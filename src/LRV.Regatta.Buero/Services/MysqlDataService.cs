using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlDataService : IRegistrationService, IFinishService
    {
        private readonly DatabaseContext databaseContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MysqlDataService(DatabaseContext context)
        {
            this.databaseContext = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void Add(FinishObject @object)
        {
            this.databaseContext.FinishObjects.Add(@object);
            this.databaseContext.SaveChanges();
        }


        public void AddRegistration(RegistrationObject registration)
        {
            this.databaseContext.Add(registration);
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
