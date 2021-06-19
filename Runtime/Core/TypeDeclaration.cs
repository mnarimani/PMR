using UnityEngine;

namespace PMR
{
    internal abstract class TypeDeclaration<TInterface> : ITypeDeclaration where TInterface : class
    {
        public abstract void CollectAll(GameObject self);

        public void Collect(GameObject target)
        {
            target.GetComponentsInChildren(true, SingletonList<IRequire<TInterface>>.List);
        }

        public void Collect<T>(T target)
        {
            UsageWarnings.WarnValueType<T>($"[PMR] `{typeof(T).Name}` is value type. Initializing it will cause boxing allocations. Consider using a class instead.");
            
            SingletonList<IRequire<TInterface>>.List.Clear();
            if (target is IRequire<TInterface> req)
                SingletonList<IRequire<TInterface>>.List.Add(req);
        }

        public abstract void Call<T>(T self);
    }
}