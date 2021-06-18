namespace PMR
{
    public interface INeedProjectReferences<in T>
    {
        void Init(T project);
    }
}