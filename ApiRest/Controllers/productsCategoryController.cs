using ApiRest.Auth;
using bussineslayer;
using bussinesLayer;
using dataAccesLayer;
using entityesLayer;
using entityesLayer.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiRest.Controllers
{
    [Route("api/productcategory/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class productsCategoryController : Controller
    {
        private categoryProductsBL categoryProductsBL = new categoryProductsBL();

        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public productsCategoryController(JwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************

        [HttpGet]
        public async Task<IEnumerable<productCategory>> Get()
        {
            return await categoryProductsBL.ObtainAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<productCategory> Get(int id)
        {
            productCategory category = new productCategory();
            category.idcategory = id;
            return await categoryProductsBL.ObtainByIdAsync(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] productCategory category)
        {
            try
            {
                await categoryProductsBL.CreateAsync(category);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<productCategory>> search([FromBody] ProductCategory pcategory)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string struser = JsonSerializer.Serialize(pcategory);
            productCategory category = JsonSerializer.Deserialize<productCategory>(struser, option);
            return await categoryProductsBL.SearchAsync(category);

        }

        [HttpPut("{idcategory}")]
        public async Task<ActionResult> Put(int idcategory, [FromBody] productCategory pcategory)
        {
            
            if (pcategory.idcategory == idcategory)

                await categoryProductsBL.ModifyAsync(pcategory);
                return Ok();
  
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                productCategory productCategory = new productCategory();
                productCategory.idcategory = id;
                await categoryProductsBL.DeleteAsync(productCategory);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



    }
}
