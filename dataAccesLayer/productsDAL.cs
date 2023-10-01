using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entityesLayer;
using Microsoft.EntityFrameworkCore;

namespace dataAccesLayer
{
    public class productsDAL
    {
        private static async Task<bool> ExisteProduct(products pProduct, dbContext pDbContext)
        {
            bool result = false;
            var productExiste = await pDbContext.products.FirstOrDefaultAsync(s => s.productname == pProduct.productname && s.idproduct != pProduct.idproduct);
            if (productExiste != null && productExiste.idproduct > 0 && productExiste.productname == pProduct.productname)
                result = true;
            return result;
        }

        public static async Task<int> CreateAsync(products pProduct)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                bool existeproduct = await ExisteProduct(pProduct, bdContexto);
                if (existeproduct == false)
                {
                    bdContexto.Add(pProduct);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Este producto ya existe");
            }
            return result;
        }

        public static async Task<int> ModifyAsync(products pProduct)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {


                var product = await bdContexto.products.FirstOrDefaultAsync(s => s.idproduct == pProduct.idproduct);
                product.idcategory = pProduct.idcategory;
                product.productname = pProduct.productname;
                product.code = pProduct.code;
                product.quantity = pProduct.quantity;
                product.price = pProduct.price;
                product.descriptions = pProduct.descriptions ;
                product.statusproduct = pProduct.statusproduct;               
                bdContexto.Update(product);

                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> DeleteAsync(products pProduct)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                var product = await bdContexto.products.FirstOrDefaultAsync(s => s.idproduct == pProduct.idproduct);
                bdContexto.products.Remove(product);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<products> ObtainByIdAsync(products pProduct)
        {
            var product = new products();
            using (var bdContexto = new dbContext())
            {
                product = await bdContexto.products.FirstOrDefaultAsync(s => s.idproduct == pProduct.idproduct);
            }
            
            return product;
        }

        public static async Task<List<products>> ObtainAllAsync()
        {
            
            var products = new List<products>();
            using (var bdContexto = new dbContext())
            {  
                products = await bdContexto.products.ToListAsync();
            }
            return products;
        }

        internal static IQueryable<products> QuerySelect(IQueryable<products> pQuery, products pProduct)
        {
            if (pProduct.idcategory > 0)
                pQuery = pQuery.Where(s => s.idcategory == pProduct.idcategory);

            if (!string.IsNullOrWhiteSpace(pProduct.productname))
                pQuery = pQuery.Where(s => s.productname.Contains(pProduct.productname));

            if (!string.IsNullOrWhiteSpace(pProduct.code))
                pQuery = pQuery.Where(s => s.code.Contains(pProduct.code));
            if (pProduct.statusproduct > 0)
                pQuery = pQuery.Where(s => s.statusproduct == pProduct.statusproduct);

            if (pProduct.idcategory > 0)
                pQuery = pQuery.Where(s => s.ProductCategories.categoryname == pProduct.ProductCategories.categoryname);
            return pQuery;
        }

        public static async Task<List<products>> SearchAsync(products pProduct)
        {
            var products = new List<products>();
            using (var bdContexto = new dbContext())
            {
                var select = bdContexto.products.AsQueryable();
                select = QuerySelect(select, pProduct);
                products = await select.ToListAsync();
            }
            return products;
        }

        public static async Task<List<products>> SearchIncludeCategoryAsync(products pUsuario)
        {
            var usuarios = new List<products>();
            using (var bdContexto = new dbContext())
            {
                var select = bdContexto.products.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.ProductCategories).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }
    }
}
