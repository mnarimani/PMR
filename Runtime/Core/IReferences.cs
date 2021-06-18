using UnityEngine;

namespace PMR
{
    public interface IReferences
    {
        void Initialize(GameObject obj);
        void Initialize<T>(T obj);
        T InstantiatePrefab<T>(T prefab) where T : Component;
        GameObject InstantiatePrefab(GameObject prefab);
        T CreateObject<T>() where T : new();
    }
}