using UnityEngine;

namespace PMR.Factory
{
    public class NewObjectFactory<T> : IFactory<T> where T: Component
    {
        private readonly IReferences _creator;

        public NewObjectFactory(IReferences creator)
        {
            _creator = creator;
        }
        
        public T Create()
        {
            var obj = new GameObject(typeof(T).Name);
            obj.SetActive(false);
            var t = obj.AddComponent<T>();
            _creator.Initialize(t);
            obj.SetActive(true);
            return t;
        }
    }
}