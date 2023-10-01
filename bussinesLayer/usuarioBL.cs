using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dataAccesLayer;
using entityesLayer;
namespace bussineslayer
{
    public class usuarioBL
    {
        public async Task<int> CreateAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.createAsync(pUsuario);
        }

        public async Task<int> ModifyAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.modifyAsync(pUsuario);
        }

        public async Task<int> DeleteAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.deleteAsync(pUsuario);
        }

        public async Task<registeredUsers> ObtainByIdAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.obtainbyidAsync(pUsuario);
        }

        public async Task<List<registeredUsers>> ObtainAllAsync()
        {
            return await usuarioDAL.obtainallAsync();
        }

        public async Task<List<registeredUsers>> SearchAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.searchAsync(pUsuario);
        }

        public async Task<registeredUsers> LoginAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.LoginAsync(pUsuario);
        }

        public async Task<int> ChangePasswordAsync(registeredUsers pUsuario, string pPasswordAnt)
        {
            return await usuarioDAL.changepasswordAsync(pUsuario, pPasswordAnt);
        }

        public async Task<List<registeredUsers>> SearchIncluirRolesAsync(registeredUsers pUsuario)
        {
            return await usuarioDAL.BuscarIncluirRolesAsync(pUsuario);
        }
    }
}
