using ApiRest.Auth;
using bussineslayer;
using entityesLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using ApiRest.Modelo;
using Microsoft.AspNetCore.Cors;
using entityesLayer.interfaces;

namespace ApiRest.Controllers
{

    [Route("api/")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class LoginController : Controller
    {
        private usuarioBL UsuarioBL = new usuarioBL();

        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public LoginController(JwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
     
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] Login pUsuario)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            registeredUsers usuario = JsonSerializer.Deserialize<registeredUsers>(strUsuario, option);
            // codigo para autorizar el usuario por JWT
            registeredUsers usuario_auth = await UsuarioBL.LoginAsync(usuario);
            if (usuario_auth != null && usuario_auth.id > 0 && usuario.nickname == usuario_auth.nickname)
            {
                var token = authService.Authenticate(usuario_auth);

                return Ok(token.ToString());
            }
            else
            {
                return Unauthorized();
            }
            // *********************************************
        }

        [HttpPost("Crear_Usuario")]
        public async Task<ActionResult> Post([FromBody] registeredUsers usuario)
        {
  
                await UsuarioBL.CreateAsync(usuario);
                return Ok("usuario creado");

        }
    }
}
