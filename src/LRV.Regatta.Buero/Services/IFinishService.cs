using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public interface IFinishService
    {
        void AddFinishObject(FinishObject @object);
        void DeleteAllFinishObject();
        void DeleteFinishObject(FinishObject item);
        IList<FinishObject> GetAllFinishObject();
    }
}
