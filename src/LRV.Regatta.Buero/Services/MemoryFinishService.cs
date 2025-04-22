using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public class MemoryFinishService : IFinishService
    {
        private List<FinishObject> _finishObjects;

        public MemoryFinishService()
        {
            _finishObjects = new List<FinishObject>();
        }

        public void AddFinishObject(FinishObject @object)
        {
            _finishObjects.Add(@object);
        }

        public void DeleteAllFinishObject()
        {
            _finishObjects.Clear();
        }

        public void DeleteFinishObject(FinishObject item)
        {
            _finishObjects.Remove(item);
        }

        public IList<FinishObject> GetAllFinishObject()
        {
            return _finishObjects;
        }
    }
}
