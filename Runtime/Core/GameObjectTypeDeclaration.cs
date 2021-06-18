using System;
using UnityEngine;

namespace PMR
{
    internal class GameObjectTypeDeclaration<TConcrete> : TypeDeclaration<INeedGameObjectReferences<TConcrete>> where TConcrete : class
    {
        public override void CollectAll(GameObject self)
        {
            self.GetComponentsInChildren(true, SingletonList<INeedGameObjectReferences<TConcrete>>.List);
        }

        public override void Call<T>(T self)
        {
            foreach (INeedGameObjectReferences<TConcrete> r in SingletonList<INeedGameObjectReferences<TConcrete>>.List)
            {
                try
                {
                    r.Init(self as TConcrete);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}