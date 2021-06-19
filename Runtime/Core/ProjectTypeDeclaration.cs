using System;
using System.Collections.Generic;
using UnityEngine;

namespace PMR
{
    internal class ProjectTypeDeclaration<TConcrete> : TypeDeclaration<TConcrete> where TConcrete : class
    {
        public override void CollectAll(GameObject self)
        {
            List<IRequire<TConcrete>> injectables = SingletonList<IRequire<TConcrete>>.List;

            List<GameObject> rootObjects = SingletonList<GameObject>.List;
            rootObjects.Clear();
            self.scene.GetRootGameObjects(rootObjects);

            List<IRequire<TConcrete>> tempList = SceneRootComponentTempList<IRequire<TConcrete>>.List;

            foreach (GameObject r in rootObjects)
            {
                r.GetComponentsInChildren(true, tempList);
                injectables.AddRange(tempList);
            }
        }

        public override void Call<T>(T self)
        {
            List<IRequire<TConcrete>> list = SingletonList<IRequire<TConcrete>>.List;
            foreach (IRequire<TConcrete> i in list)
            {
                try
                {
                    i.Init(ProjectReferences.GetInstance() as TConcrete);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}