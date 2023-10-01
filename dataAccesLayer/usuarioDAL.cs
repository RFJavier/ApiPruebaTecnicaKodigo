using entityesLayer;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace dataAccesLayer
{
    public class usuarioDAL
    {
        private static void EncriptarMD5(registeredUsers pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.userpassword));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUsuario.userpassword = strEncriptar;
            }
        }

        private static async Task<bool> ExisteLogin(registeredUsers pUsuario, dbContext pDbContext)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContext.registeredusers.FirstOrDefaultAsync(s => s.nickname == pUsuario.nickname && s.id != pUsuario.id);
            if (loginUsuarioExiste != null && loginUsuarioExiste.id > 0 && loginUsuarioExiste.nickname == pUsuario.nickname)
                result = true;
            return result;
        }

        public static async Task<int> createAsync(registeredUsers pUsuario)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                bool existeLogin = await ExisteLogin(pUsuario, bdContexto);
                if (existeLogin == false)
                {
                    pUsuario.registerdate = DateTime.Now;
                    EncriptarMD5(pUsuario);
                    bdContexto.Add(pUsuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        public static async Task<int> modifyAsync(registeredUsers pUsuario)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {


                var usuario = await bdContexto.registeredusers.FirstOrDefaultAsync(s => s.id == pUsuario.id);
                usuario.idrol = usuario.idrol;
                usuario.username = pUsuario.username;
                usuario.nickname = pUsuario.nickname;
                usuario.email = pUsuario.email;
                usuario.isactive = pUsuario.isactive;
                bdContexto.Update(usuario);

                result = await bdContexto.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> deleteAsync(registeredUsers pUsuario)
        {
            int result = 0;
            using (var bdContexto = new dbContext())
            {
                var usuario = await bdContexto.registeredusers.FirstOrDefaultAsync(s => s.id == pUsuario.id);
                bdContexto.registeredusers.Remove(usuario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<registeredUsers> obtainbyidAsync(registeredUsers pUsuario)
        {
            var usuario = new registeredUsers();
            using (var bdContexto = new dbContext())
            {
                usuario = await bdContexto.registeredusers.FirstOrDefaultAsync(s => s.id == pUsuario.id);
            }
            return usuario;
        }

        public static async Task<List<registeredUsers>> obtainallAsync()
        {
            var usuarios = new List<registeredUsers>();
            using (var bdContexto = new dbContext())
            {
                usuarios = await bdContexto.registeredusers.ToListAsync();
            }
            return usuarios;
        }

        internal static IQueryable<registeredUsers> QuerySelect(IQueryable<registeredUsers> pQuery, registeredUsers pUsuario)
        {
            if (pUsuario.id > 0)
                pQuery = pQuery.Where(s => s.id == pUsuario.id);
            if (pUsuario.idrol > 0)
                pQuery = pQuery.Where(s => s.idrol == pUsuario.idrol);
            if (!string.IsNullOrWhiteSpace(pUsuario.username))
                pQuery = pQuery.Where(s => s.username.Contains(pUsuario.username));
            if (!string.IsNullOrWhiteSpace(pUsuario.nickname))
                pQuery = pQuery.Where(s => s.nickname.Contains(pUsuario.nickname));
            if (pUsuario.isactive > 0)
                pQuery = pQuery.Where(s => s.isactive == pUsuario.isactive);
            if (pUsuario.registerdate.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.registerdate.Year, pUsuario.registerdate.Month, pUsuario.registerdate.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.registerdate >= fechaInicial && s.registerdate <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.id).AsQueryable();
            if (pUsuario.Top_Aux > 0)
                pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<registeredUsers>> searchAsync(registeredUsers pUsuario)
        {
            var Usuarios = new List<registeredUsers>();
            using (var bdContexto = new dbContext())
            {
                var select = bdContexto.registeredusers.AsQueryable();
                select = QuerySelect(select, pUsuario);
                Usuarios = await select.ToListAsync();
            }
            return Usuarios;
        }

        public static async Task<List<registeredUsers>> BuscarIncluirRolesAsync(registeredUsers pUsuario)
        {
            var usuarios = new List<registeredUsers>();
            using (var bdContexto = new dbContext())
            {
                var select = bdContexto.registeredusers.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }

        public static async Task<registeredUsers> LoginAsync(registeredUsers pUsuario)
        {
            var usuario = new registeredUsers();
            using (var bdContexto = new dbContext())
            {
                EncriptarMD5(pUsuario);
                usuario = await bdContexto.registeredusers.FirstOrDefaultAsync(s => s.nickname == pUsuario.nickname &&
                s.userpassword == pUsuario.userpassword && s.isactive == (int)Estatus_Usuario.ACTIVO);
            }
            return usuario;
        }

        public static async Task<int> changepasswordAsync(registeredUsers pUsuario, string pPasswordAnt)
        {
            int result = 0;
            var usuarioPassAnt = new registeredUsers { userpassword = pPasswordAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var bdContexto = new dbContext())
            {
                var usuario = await bdContexto.registeredusers.FirstOrDefaultAsync(s => s.id == pUsuario.id);
                if (usuarioPassAnt.userpassword == usuario.userpassword)
                {
                    EncriptarMD5(pUsuario);
                    usuario.userpassword = pUsuario.userpassword;
                    bdContexto.Update(usuario);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("El password actual es incorrecto");
            }
            return result;
        }
    }
}
