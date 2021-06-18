using UnityEngine;

namespace PMR.Factory
{
    public static class FactoryExtensions
    {
        public static PrefabFactory<T> CreateFactory<T>(this T prefab, IReferences references) where T : Component
        {
            return new PrefabFactory<T>(references, prefab);
        }
    }
}