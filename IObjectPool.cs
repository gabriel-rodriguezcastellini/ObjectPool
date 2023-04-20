namespace ObjectPool;

public interface IObjectPool<T> where T: class
{
    /// <summary>
    /// Return the object from the pool.
    /// </summary>
    /// <returns>T</returns>
    T Get();

    /// <summary>
    /// Return the pass obj to the pool and makes it available.
    /// </summary>
    /// <param name="obj">T</param>
    void Return(T obj);
}
