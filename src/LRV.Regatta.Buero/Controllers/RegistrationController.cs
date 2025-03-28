using LRV.Regatta.Buero.Atributes;
using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        [HttpGet]
        public IList<string> Get()
        {
            List<string> returnValue = new ();

            for (int i = 0; i < 50; i++)
            {
                returnValue.Add($"String_{i}");
            }
            
            return returnValue;
        }
    }
}