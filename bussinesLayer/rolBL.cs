
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dataAccesLayer;
using entityesLayer;

namespace bussineslayer
{
    public class rolBL
    {
        public async Task<int> createAsync(rol pRol)
        {
            return await rolDAL.createAsync(pRol);
        }

        public async Task<int> modifyAsync(rol pRol)
        {
            return await rolDAL.modifyAsync(pRol);
        }

        public async Task<int> deleteAsync(rol pRol)
        {
            return await rolDAL.deleteAsync(pRol);
        }

        public async Task<rol> obtainbyidAsync(rol pRol)
        {
            return await rolDAL.obtainbyidAsync(pRol);
        }

        public async Task<List<rol>> obatinallAsync()
        {
            return await rolDAL.obtainallAsync();
        }

        public async Task<List<rol>> searchAsync(rol pRol)
        {
            return await rolDAL.searchAsync(pRol);
        }


    }
}
