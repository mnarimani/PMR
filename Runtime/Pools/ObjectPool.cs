using System.Collections.Generic;
using PMR.Factory;

namespace PMR.Pools
{
    public class ObjectPool<T> : IFactory<T>, IObjectPool<T> where T : IPoolable<T>
    {
        private readonly List<T> _pool = new List<T>();
        private IFactory<T> _creator;

        public ObjectPool(IFactory<T> creator)
        {
            _creator = creator;
        }

        public void Prewarm(int count)
        {
            _pool.Capacity += count;
            for (int i = 0; i < count; i++)
            {
                _pool.Add(OnPushedToPool(_creator.Create()));
            }
        }

        public T Spawn()
        {
            T obj;
            if (_pool.Count > 0)
            {
                int index = _pool.Count - 1;
                T t = _pool[index];
                _pool.RemoveAt(index);
                obj = OnPoppedFromPool(t);
            }
            else
            {
                obj = _creator.Create();
            }
            
            obj.OnPoolSpawned(this);
            return obj;
        }

        public void Despawn(T obj)
        {
            obj.OnPoolDespawned();
            _pool.Add(OnPushedToPool(obj));
        }

        protected virtual T OnPoppedFromPool(T obj)
        {
            return obj;
        }

        protected virtual T OnPushedToPool(T obj)
        {
            return obj;
        }

        T IFactory<T>.Create()
        {
            return Spawn();
        }
    }
}