namespace PMR
{
    public interface IPocoRegistry
    {
        T RegisterPocoType<T>() where T : new();
        void RegisterPocoType<T>(T obj);
        void RemovePocoType<T>(T obj);
    }
}