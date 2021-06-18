using PMR.Pools;
using UnityEngine;

namespace PMR.Factory
{
    public class PrefabFactory<T> : IFactory<T> where T : Component
    {
        private readonly IReferences _creator;
        private readonly T _prefab;

        public PrefabFactory(IReferences creator, T prefab)
        {
            _prefab = prefab;
            _creator = creator;
        }

        public T Create()
        {
            return _creator.InstantiatePrefab(_prefab);
        }
    }
}