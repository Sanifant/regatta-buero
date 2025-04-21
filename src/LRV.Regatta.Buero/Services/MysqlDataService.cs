using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public class MysqlDataService : IRegistrationService, IFinishService
    {
        private readonly DatabaseContext databaseContext;

        public MysqlDataService(DatabaseContext context)
        {
            this.databaseContext = context;
        }

        public void Add(FinishObject @object)
        {
            this.databaseContext.FinishObjects.Add(@object);
            this.databaseContext.SaveChanges();
        }

        public void AddRegistration(RegistrationObject registration)
        {
            this.databaseContext.Update(registration);
            this.databaseContext.SaveChanges();
        }

        public IList<FinishObject> GetAll()
        {
            return this.databaseContext.FinishObjects.ToList();
        }

        public List<RegistrationObject> GetRegistrations()
        {
            return this.databaseContext.RegistrationObjects.ToList();
        }
    }
}
