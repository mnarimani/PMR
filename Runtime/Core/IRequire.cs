namespace PMR
{
    public interface IRequire<in T>
    {
        void Init(T dep);
    }
}