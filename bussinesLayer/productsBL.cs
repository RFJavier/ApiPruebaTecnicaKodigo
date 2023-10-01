using dataAccesLayer;
using entityesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bussinesLayer
{
    public class productsBL
    {
        public async Task<int> CreateAsync(products pProducts)
        {
            return await productsDAL.CreateAsync(pProducts);
        }

        public async Task<int> ModifyAsync(products pProducts)
        {
            return await productsDAL.ModifyAsync(pProducts);
        }

        public async Task<int> DeleteAsync(products pProducts)
        {
            return await productsDAL.DeleteAsync(pProducts);
        }

        public async Task<products> ObtainByIdAsync(products pProducts)
        {
            return await productsDAL.ObtainByIdAsync(pProducts);
        }

        public async Task<List<products>> ObtainAllAsync()
        {
            return await productsDAL.ObtainAllAsync();
        }

        public async Task<List<products>> SearchAsync(products pProducts)
        {
            return await productsDAL.SearchAsync(pProducts);
        }
        public async Task<List<products>> SearchIncludeCategpryAsync(products pProducts)
        {
            return await productsDAL.SearchIncludeCategoryAsync(pProducts);
        }

    }
}
