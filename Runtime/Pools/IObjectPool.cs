namespace PMR.Pools
{
    public interface IObjectPool<T>
    {
        T Spawn();
        void Despawn(T obj);
    }
}