namespace ObjectPool;

public class PoolAbleItem
{
    public static int Instances { get; protected set; }
    private readonly string _identifier;
    public string Id => _identifier;

    public PoolAbleItem(string identifier)
    {
        _identifier = identifier;
        Instances++;
    }

    ~PoolAbleItem()
    {
        Instances--;
    }
}
