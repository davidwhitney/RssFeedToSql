using System.Collections.Generic;
using System.Linq;

namespace RssFeedToSql.SqlGeneration
{
    public class InMemoryDatabaseOf<TEntityType> where TEntityType : IHaveAnId
    {
        private int _lastId;
        private readonly Dictionary<int, TEntityType> _inMemoryStore;

        public InMemoryDatabaseOf()
        {
            _inMemoryStore = new Dictionary<int, TEntityType>();
        }

        public int CreateOrRetrieveId(TEntityType record)
        {
            if (_inMemoryStore.ContainsValue(record))
            {
                return _inMemoryStore.Single(x => Equals(x.Value, record)).Key;
            }

            _lastId = _lastId + 1;
            record.Id = _lastId;
            _inMemoryStore.Add(_lastId, record);
            return _lastId;
        }

        public IEnumerable<TEntityType> Items
        {
            get { return _inMemoryStore.Values; }
        } 
    }
}