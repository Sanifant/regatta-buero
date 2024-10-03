using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public class FileStorageService : IStorageService
    {
        private List<FinishObject> _finishObjects;

        public FileStorageService()
        {
            _finishObjects = new List<FinishObject>();
        }

        public void Add(FinishObject @object)
        {
            _finishObjects.Add(@object);
        }

        public IList<FinishObject> GetAll()
        {
            return _finishObjects;
        }
    }
}
