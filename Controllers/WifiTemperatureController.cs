using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dal;
using System.Text.Json;
using WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class WifiTemperatureController : Controller
    {
        private DataBaseManager DBContext;
        private readonly IConfiguration configuration;
        public WifiTemperatureController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            DBContext = new DataBaseManager(configuration);
        }


        [Route("Get")]
        [HttpGet]
        public IEnumerable<ArduinoData> Get()
        {
            var list = DBContext.GetData();

            return list.ToArray();
           
        }
        [Route("Post")]
        [HttpPost]
        public string PostTodoItem(ArduinoData data)
        {

            try
            {
                if (data == null)
                    return BadRequest().ToString();
                data.Log = DateTime.Now;
                 DBContext.Add(data);

                return null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new temperature data").ToString();
            }



            
        }
    }
}
