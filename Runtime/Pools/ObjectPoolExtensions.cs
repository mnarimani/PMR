using PMR.Factory;
using UnityEngine;

namespace PMR.Pools
{
    public static class ObjectPoolExtensions
    {
        public static MonoObjectPool<T> CreateObjectPool<T>(this T prefab, IReferences references, bool createParent = true) 
            where T : Component, IPoolable<T>
        {
            return new MonoObjectPool<T>(new PrefabFactory<T>(references, prefab), createParent);
        }
    }
}