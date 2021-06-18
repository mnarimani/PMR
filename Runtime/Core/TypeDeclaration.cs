using UnityEngine;

namespace PMR
{
    internal abstract class TypeDeclaration<TInterface> : ITypeDeclaration
    {
        public abstract void CollectAll(GameObject self);

        public void Collect(GameObject target)
        {
            target.GetComponentsInChildren(true, SingletonList<TInterface>.List);
        }

        public void Collect<T>(T target)
        {
            SingletonList<TInterface>.List.Clear();
            if (target is TInterface req)
                SingletonList<TInterface>.List.Add(req);
        }

        public abstract void Call<T>(T self);
    }
}