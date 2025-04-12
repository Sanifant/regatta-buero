using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRV.Regatta.Buero.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Tests.Service;

namespace LRV.Regatta.Buero.Controllers.Tests
{
    [TestClass()]
    public class RegistrationControllerTests
    {
        private IRegistrationService _registrationService;


        [TestInitialize]
        public void TestInitialize()
        {
            _registrationService = new MockRegistrationService();
        }

        [TestMethod()]
        public void Test_That_Get()
        {
            var controller = new RegistrationController(_registrationService);

            var returnValue = controller.Get();


            Assert.AreEqual(15, returnValue.Count);
        }

        [TestMethod()]
        public void Test_That_AddRegistration()
        {
            Assert.IsTrue(true);
        }
    }
}