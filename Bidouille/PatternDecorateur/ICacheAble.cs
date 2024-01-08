
namespace Bidouille.PatternDecorateur;

public interface ICacheAble
{
    public object Cache { get; set; }

    public void AddDataCache(object data);
    public void RemoveDataCache(object data); 
    public object GetDataCache(object data);
}

public abstract class BaseCacheDecorator : ICacheAble
{
    public BaseCacheDecorator(ICacheAble cacheAble)
    {
        _cacheAble = cacheAble;
    }
        
    private readonly ICacheAble _cacheAble;

    public object Cache { get; set; }

    public virtual void AddDataCache(object data)
    {
        _cacheAble.AddDataCache(data);
    }

    public virtual void RemoveDataCache(object data)
    {
        _cacheAble.RemoveDataCache(data);
    }

    public virtual object GetDataCache(object data)
    {
        return _cacheAble.GetDataCache(data); 
    }
}

public class CacheDecorator : BaseCacheDecorator
{
    public CacheDecorator(ICacheAble cacheAble) : base(cacheAble)
    {
    }

    public override void AddDataCache(object data)
    {

        base.AddDataCache(data);
    }
}