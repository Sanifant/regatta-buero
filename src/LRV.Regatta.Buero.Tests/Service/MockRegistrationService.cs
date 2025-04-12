using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRV.Regatta.Buero.Tests.Service
{
    internal class MockRegistrationService : IRegistrationService
    {
        public void AddRegistration(RegistrationObject registration)
        {
            throw new NotImplementedException();
        }

        public List<RegistrationObject> GetRegistrations()
        {
            var returnValue = new List<RegistrationObject>();


            for (int i = 0; i < 15; i++)
            {
                returnValue.Add(new RegistrationObject()
                {
                    Type = i % 2 == 0 ? RegistrationType.Registration : RegistrationType.LateRegistration,
                    Race = $"Race {i}",
                    StartNo = $"Race {i}",
                    Team = $"Race {i}",
                    ChairMan = $"Race {i}"
                });
            }

            return returnValue;
        }
    }
}
