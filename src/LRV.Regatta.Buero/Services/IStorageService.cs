using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public interface IStorageService
    {
        void Add(FinishObject @object);
        IList<FinishObject> GetAll();
    }
}
