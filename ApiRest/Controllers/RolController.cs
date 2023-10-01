
using ApiRest.Auth;
using bussineslayer;
using entityesLayer;
using entityesLayer.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace ApiRest.Controllers
{
    [Route("api/rol/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolController : ControllerBase
    {

        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public RolController(JwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************
        private rolBL RolBL = new rolBL();

        [HttpGet]
        public async Task<IEnumerable<rol>> Get()
        {
            return await RolBL.obatinallAsync();
        }

        [HttpGet("{id}")]
        public async Task<rol> Get(int id)
        {
            rol rol = new rol();
            rol.idrol = id;
            return await RolBL.obtainbyidAsync(rol);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] rol rol)
        {
            try
            {
                await RolBL.createAsync(rol);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<rol>> Buscar([FromBody] Rol prol)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strrol = JsonSerializer.Serialize(prol);
            rol rol = JsonSerializer.Deserialize<rol>(strrol, option);
            return await RolBL.searchAsync(rol);

        }

        [HttpPut("{idrol}")]
        public async Task<ActionResult> Put(int idrol, [FromBody] rol rol)
        {

            if (rol.idrol == idrol)
            {
                Console.WriteLine(rol.rolname);
                await RolBL.modifyAsync(rol);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            rol rol = new rol();
            rol.idrol = id;
            await RolBL.deleteAsync(rol);
            return Ok();


        }

       
    }
}
