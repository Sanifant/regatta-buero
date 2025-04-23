using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlDataService : IRegistrationService, IFinishService, ILogService
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


        public void AddRegistration(RegistrationObject registration)
        {
            this.databaseContext.Add(registration);
            this.databaseContext.SaveChanges();
        }

        public List<RegistrationObject> GetRegistrations()
        {
            return this.databaseContext.RegistrationObjects.ToList();
        }

        public void DeleteRegistration(RegistrationObject registration)
        {
            this.databaseContext.Remove(registration);
            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void AddFinishObject(FinishObject @object)
        {
            this.databaseContext.FinishObjects.Add(@object);
            this.databaseContext.SaveChanges();
        }

        public IList<FinishObject> GetAllFinishObject()
        {
            return this.databaseContext.FinishObjects.ToList();
        }

        public void DeleteAllFinishObject()
        {
            this.databaseContext.FinishObjects.RemoveRange(this.databaseContext.FinishObjects);
            this.databaseContext.SaveChanges();
        }

        public void DeleteFinishObject(FinishObject @object)
        {
            this.databaseContext.FinishObjects.Remove(@object);
            this.databaseContext.SaveChanges();
        }

        public void AddLog(LogObject log)
        {
            this.databaseContext.LogObjects.Add(log);
            this.databaseContext.SaveChanges();
        }

        public List<LogObject> GetLogs()
        {
            return this.databaseContext.LogObjects.ToList();
        }
    }
}
