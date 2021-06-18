using System;
using System.Collections.Generic;
using UnityEngine;

namespace PMR
{
    internal class ProjectTypeDeclaration<TProjImpl,TConcrete> : TypeDeclaration<INeedProjectReferences<TConcrete>> 
        where TProjImpl : ProjectReferences<TProjImpl> where TConcrete : class
    {
        public override void CollectAll(GameObject self)
        {
            List<INeedProjectReferences<TConcrete>> injectables = SingletonList<INeedProjectReferences<TConcrete>>.List;

            List<GameObject> rootObjects = SingletonList<GameObject>.List;
            rootObjects.Clear();
            self.scene.GetRootGameObjects(rootObjects);

            List<INeedProjectReferences<TConcrete>> tempList = SceneRootComponentTempList<INeedProjectReferences<TConcrete>>.List;

            foreach (GameObject r in rootObjects)
            {
                r.GetComponentsInChildren(true, tempList);
                injectables.AddRange(tempList);
            }
        }

        public override void Call<T>(T self)
        {
            List<INeedProjectReferences<TConcrete>> list = SingletonList<INeedProjectReferences<TConcrete>>.List;
            foreach (INeedProjectReferences<TConcrete> i in list)
            {
                try
                {
                    i.Init(ProjectReferences<TProjImpl>.GetInstance() as TConcrete);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}