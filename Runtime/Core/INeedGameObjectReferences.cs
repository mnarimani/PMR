namespace PMR
{
    public interface INeedGameObjectReferences<in T>
    {
        void Init(T go);
    }
}