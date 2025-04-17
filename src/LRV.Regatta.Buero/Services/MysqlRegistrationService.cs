using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public class MysqlRegistrationService : IRegistrationService
    {
        private readonly DatabaseContext databaseContext;

        public MysqlRegistrationService(DatabaseContext context)
        {
            this.databaseContext = context;
        }

        public void AddRegistration(RegistrationObject registration)
        {
            this.databaseContext.Add(registration);
            this.databaseContext.SaveChanges();
        }

        public List<RegistrationObject> GetRegistrations()
        {
            return this.databaseContext.RegistrationObjects.ToList();
        }
    }
}
