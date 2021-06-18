namespace PMR
{
    public interface INeedSceneReferences<in T>
    {
        void Init(T scene);
    }
}