using System.Collections.Generic;
using System.Linq;

namespace RssFeedToSql.SqlGeneration
{
    public class InMemoryDatabaseOf<TEntityType>
    {
        private int _writerAutoId;
        private readonly Dictionary<int, TEntityType> _writers;

        public InMemoryDatabaseOf()
        {
            _writerAutoId = 1;
            _writers = new Dictionary<int, TEntityType>();
        }

        public int CreateOrRetrieveId(TEntityType potentiallyNewWriter)
        {
            if (_writers.ContainsValue(potentiallyNewWriter))
            {
                return _writers.Single(x => Equals(x.Value, potentiallyNewWriter)).Key;
            }

            _writerAutoId = _writerAutoId + 1;
            _writers.Add(_writerAutoId, potentiallyNewWriter);
            return _writerAutoId;
        }
    }
}