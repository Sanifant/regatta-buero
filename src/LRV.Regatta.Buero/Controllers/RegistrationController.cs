using LRV.Regatta.Buero.Attributes;
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
        private readonly IRegistrationService dataService;

        public RegistrationController(IRegistrationService data){
            this.dataService = data;
        }

        [HttpGet]
        public IList<RegistrationObject> Get()
        {
            List<RegistrationObject> returnValue = this.dataService.GetRegistrations();
            
            return returnValue;
        }

        [HttpPut]
        public void AddRegistration([FromBody]RegistrationObject registration){

            this.dataService.AddRegistration(registration);
        }
    }
}