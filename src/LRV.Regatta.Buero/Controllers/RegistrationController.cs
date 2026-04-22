using LRV.Regatta.Buero.Attributes;
using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Controllers
{
    /// <summary>
    /// RegistrationController class that handles HTTP requests related to registration data in the regatta management system. This controller includes endpoints for retrieving all registration objects and adding new registration objects. The controller uses the IRegistrationService to manage registration data and includes an API key attribute for securing the endpoints.
    /// </summary>
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService dataService;

        /// <summary>
        /// Constructor for the RegistrationController class, initializing the IRegistrationService dependency. This constructor sets up the necessary service for managing registration data in the regatta management system, allowing the controller to handle registration-related HTTP requests effectively.
        /// </summary>
        /// <param name="data">The IRegistrationService instance for managing registration data.</param>
        public RegistrationController(IRegistrationService data){
            this.dataService = data;
        }

        /// <summary>
        /// Processes the GET HTTP Verb, retrieving all registration objects from the registration service. This method returns a list of all RegistrationObject instances stored in the registration service, allowing clients to access the complete registration data for analysis and management purposes.
        /// </summary>
        /// <returns>A list of all RegistrationObject instances.</returns>
        [HttpGet]
        public IList<RegistrationObject> Get()
        {
            List<RegistrationObject> returnValue = this.dataService.GetRegistrations();
            
            return returnValue;
        }

        /// <summary>
        /// Processes the PUT HTTP Verb, adding a new registration object to the registration service. This method accepts a RegistrationObject instance from the request body and adds it to the registration service, allowing clients to submit new registration data for storage and management in the regatta management system.
        /// </summary>
        /// <param name="registration">The RegistrationObject instance to be added.</param>
        [HttpPut]
        public void AddRegistration([FromBody]RegistrationObject registration){

            this.dataService.AddRegistration(registration);
        }
    }
}