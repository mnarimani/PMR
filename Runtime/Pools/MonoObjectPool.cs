using PMR.Factory;
using UnityEngine;

namespace PMR.Pools
{
    public class MonoObjectPool<T> : ObjectPool<T> where T : Component, IPoolable<T>
    {
        private readonly bool _createParent;
        private Transform _parent;

        public MonoObjectPool(IFactory<T> creator, bool createParent) : base(creator)
        {
            _createParent = createParent;
        }

        protected override T OnPoppedFromPool(T obj)
        {
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }

        protected override T OnPushedToPool(T obj)
        {
            if (_createParent && _parent == null)
                _parent = new GameObject(typeof(T).Name + " Pool").transform;

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent);
            return obj;
        }
    }
}