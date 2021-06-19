using System;
using UnityEngine;

namespace PMR
{
    internal class GameObjectTypeDeclaration<TConcrete> : TypeDeclaration<TConcrete> where TConcrete : class
    {
        public override void CollectAll(GameObject self)
        {
            self.GetComponentsInChildren(true, SingletonList<IRequire<TConcrete>>.List);
        }

        public override void Call<T>(T self)
        {
            foreach (IRequire<TConcrete> r in SingletonList<IRequire<TConcrete>>.List)
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