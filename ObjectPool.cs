namespace ObjectPool
{
    public class ObjectPoolImplementation : IObjectPool<PoolAbleItem>
    {
        private static readonly Dictionary<string, PoolAbleItem> _poolAbleItems = new();
        private static readonly Dictionary<string, PoolAbleItem> _poolAbleItemsB = new();
        private readonly object _lock = new();
        readonly bool __lockWasTaken = false;

        public PoolAbleItem Get()
        {
            try
            {
                var poolableItem = new PoolAbleItem(Guid.NewGuid().ToString());

                lock (_lock)
                {
                    _poolAbleItems.Add(poolableItem.Id, poolableItem);
                }
                
                return poolableItem;
            }
            finally
            {
                if (__lockWasTaken)
                {
                    Monitor.Exit(_lock);
                }
            }            
        }

        public void Return(PoolAbleItem obj)
        {
            try
            {
                lock (_lock)
                {
                    var item = _poolAbleItems.TryGetValue(obj.Id, out PoolAbleItem? poolAbleItem);

                    if (poolAbleItem != null)
                    {
                        _poolAbleItemsB.TryAdd(obj.Id, poolAbleItem);
                    }
                    else
                    {
                        Console.WriteLine($"The item key {obj.Id} doesn't exist");
                        throw new Exception($"The item key {obj.Id} doesn't exist");
                    }
                }                               
            }
            catch (Exception e)
            {
                Console.WriteLine($"The item key {obj.Id} doesn't exist, exception: {e.Message}");
            }
            finally
            {
                if (__lockWasTaken)
                {
                    Monitor.Exit(_lock);
                }
            }
        }
    }
}
