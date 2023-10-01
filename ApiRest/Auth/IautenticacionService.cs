using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entityesLayer;
namespace ApiRest.Auth
{
    public interface IautenticacionService
    {
        string Authenticate(registeredUsers pUsuario);
    }
}
