
using entityesLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataAccesLayer
{
    public class rolDAL 
    {
        public static async Task<int> createAsync(rol pRol)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                bdContexto.Add(pRol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> modifyAsync(rol pRol)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                var rol = await bdContexto.rol.FirstOrDefaultAsync(s => s.idrol == pRol.idrol);
                rol.rolname = pRol.rolname;
                bdContexto.Update(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> deleteAsync(rol pRol)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                var rol = await bdContexto.rol.FirstOrDefaultAsync(s => s.idrol == pRol.idrol);
                bdContexto.rol.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<rol> obtainbyidAsync(rol pRol)
        {
            var rol = new rol();
            using (var bdContexto = new dbContext())
            {
                rol = await bdContexto.rol.FirstOrDefaultAsync(s => s.idrol == pRol.idrol);
            }
            return rol;
        }

        public static async Task<List<rol>> obtainallAsync()
        {
            var roles = new List<rol>();
            using (var bdContexto = new dbContext())
            {
                roles = await bdContexto.rol.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<rol> QuerySelect(IQueryable<rol> pQuery, rol pRol)
        {
            if (pRol.idrol > 0)
                pQuery = pQuery.Where(s => s.idrol == pRol.idrol);

            if (!string.IsNullOrWhiteSpace(pRol.rolname))
                pQuery = pQuery.Where(s => s.rolname.Contains(pRol.rolname));

            pQuery = pQuery.OrderByDescending(s => s.idrol).AsQueryable();

            if (pRol.Top_Aux > 0)
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<rol>> searchAsync(rol pRol)
        {
            var roles = new List<rol>();
            using (var bdContexto = new dbContext())
            {
                var select = bdContexto.rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync( );
            }
            return roles;
        }

      

    }
}
