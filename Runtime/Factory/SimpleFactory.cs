namespace PMR.Factory
{
    public class SimpleFactory<T> : IFactory<T> where T : new()
    {
        private readonly IReferences _creator;

        public SimpleFactory(IReferences creator)
        {
            _creator = creator;
        }
        
        public T Create()
        {
            return _creator.CreateObject<T>();
        }
    }
}