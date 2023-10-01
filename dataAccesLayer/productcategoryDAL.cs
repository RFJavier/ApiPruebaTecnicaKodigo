using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entityesLayer;
using Microsoft.EntityFrameworkCore;

namespace dataAccesLayer
{
    public class productcategoryDAL
    {
        public static async Task<int> CreateAsync(productCategory pCategory)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                bdContexto.Add(pCategory);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModifyAsync(productCategory pCategory)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                var category = await bdContexto.productcategory.FirstOrDefaultAsync(s => s.idcategory == pCategory.idcategory);
                category.categoryname = pCategory.categoryname;
                category.statuscategory = pCategory.statuscategory;
                bdContexto.Update(category);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> DeleteAsync(productCategory pCategory)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                var category = await bdContexto.productcategory.FirstOrDefaultAsync(s => s.idcategory == pCategory.idcategory);
                bdContexto.productcategory.Remove(category);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<productCategory> ObtainByIdAsync(productCategory pCategory)
        {
            var category = new productCategory();
            using (var bdContexto = new dbContext())
            {
                category = await bdContexto.productcategory.FirstOrDefaultAsync(s => s.idcategory == pCategory.idcategory);
            }
            return category;
        }

        public static async Task<List<productCategory>> ObtainAllAsync()
        {
            var categories = new List<productCategory>();
            using (var bdContexto = new dbContext())
            {
                categories = await bdContexto.productcategory.ToListAsync();
            }
            return categories;
        }

        internal static IQueryable<productCategory> QuerySelect(IQueryable<productCategory> pQuery, productCategory pcategory)
        {
            if (pcategory.idcategory > 0)
                pQuery = pQuery.Where(s => s.idcategory == pcategory.idcategory);
            if (!string.IsNullOrWhiteSpace(pcategory.categoryname))
                pQuery = pQuery.Where(s => s.categoryname.Contains(pcategory.categoryname));
            if (pcategory.statuscategory > 0)
                pQuery = pQuery.Where(s => s.statuscategory == pcategory.statuscategory);

            pQuery = pQuery.OrderByDescending(s => s.idcategory).AsQueryable();
            return pQuery;
        }

        public static async Task<List<productCategory>> SearchAsync(productCategory pCategory)
        {
            var categories = new List<productCategory>();
            using (var bdContexto = new dbContext())
            {
                var select = bdContexto.productcategory.AsQueryable();
                select = QuerySelect(select, pCategory);
                categories = await select.ToListAsync();
            }
            return categories;
        }

    }
}

