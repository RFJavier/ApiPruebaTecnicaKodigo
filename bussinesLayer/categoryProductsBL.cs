using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dataAccesLayer;
using entityesLayer;

namespace bussinesLayer
{
    public class categoryProductsBL
    {
        public async Task<int> CreateAsync(productCategory pCategory)
        {
            return await productcategoryDAL.CreateAsync(pCategory);
        }

        public async Task<int> ModifyAsync(productCategory pCategory)
        {
            return await productcategoryDAL.ModifyAsync(pCategory);
        }

        public async Task<int> DeleteAsync(productCategory pCategory)
        {
            return await productcategoryDAL.DeleteAsync(pCategory);
        }

        public async Task<productCategory> ObtainByIdAsync(productCategory pCategory)
        {
            return await productcategoryDAL.ObtainByIdAsync(pCategory);
        }

        public async Task<List<productCategory>> ObtainAllAsync()
        {
            return await productcategoryDAL.ObtainAllAsync();
        }

        public async Task<List<productCategory>> SearchAsync(productCategory pCategory)
        {
            return await productcategoryDAL.SearchAsync(pCategory);
        }
    }
}
