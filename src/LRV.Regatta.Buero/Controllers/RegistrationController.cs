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
        private readonly IDataService dataService;

        public RegistrationController(IDataService data){
            this.dataService = data;
        }

        [HttpGet]
        public IList<RegistrationObject> Get()
        {
            List<RegistrationObject> returnValue = new List<RegistrationObject>();

            for (int i = 0; i < 50; i++)
            {
                returnValue.Add(new RegistrationObject(){
                    Type = i%2==0 ? RegistrationType.Registration : RegistrationType.LateRegistration,
                    Race = $"Race {i}",
                    StartNo = $"Race {i}",
                    Team  = $"Race {i}",
                    ChairMan = $"Race {i}"
                });
            }
            
            return returnValue;
        }

        [HttpPut]
        public void AddRegistration([FromBody]RegistrationObject registration){

            this.dataService.AddRegistration(registration);
        }
    }
}