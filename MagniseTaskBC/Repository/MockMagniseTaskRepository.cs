namespace MagniseTaskBC
{
    public class MockMagniseTaskRepository : IMagniseTaskRepository
    {
        private readonly MockMagniseTaskDataStore _context = new MockMagniseTaskDataStore();
        //public TestMagniseTaskRepository(TestMagniseTaskDataStore context) => _context = context;

        public async Task SaveChangesAsync()
        {
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return _context.Get<T>(typeof(T).FullName).AsQueryable();
        }

        public IQueryable<T> NoTrack<T>() where T : class
        {
            return _context.Get<T>(typeof(T).FullName).AsQueryable();
        }

        public void AddObject<T>(T aEntity) where T : class
        {
            _context.Add(typeof(T).FullName, aEntity);
        }
    }

    public class MockMagniseTaskDataStore
    {
        private readonly Dictionary<string, List<object>> dataStore = new Dictionary<string, List<object>>();

        public IEnumerable<T> Get<T>(string aKey)
        {
            if (!dataStore.ContainsKey(aKey))
            {
                return new List<T>();
            }

            var data = dataStore[aKey];
            
            return data.Cast<T>();
        }

        public void Add<T>(string aKey, T aData)
        {
            if (!dataStore.ContainsKey(aKey))
            {
                dataStore.Add(aKey, new List<object>());
            }

            dataStore[aKey].Add(aData);
        }
    }
}
