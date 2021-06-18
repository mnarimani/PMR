namespace PMR.Pools
{
    public interface IPoolable<T>
    {
        void OnPoolSpawned(IObjectPool<T> objectPool);
        void OnPoolDespawned();
    }
}