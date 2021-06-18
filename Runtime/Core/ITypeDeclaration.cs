using UnityEngine;

namespace PMR
{
    internal interface ITypeDeclaration
    {
        void CollectAll(GameObject self);
        void Collect(GameObject target);
        void Collect<T>(T target);
        void Call<T>(T self);
    }
}