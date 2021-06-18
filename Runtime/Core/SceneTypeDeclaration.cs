using System;
using System.Collections.Generic;
using UnityEngine;

namespace PMR
{
    internal class SceneTypeDeclaration<TConcrete> : TypeDeclaration<INeedSceneReferences<TConcrete>> where TConcrete : class
    {
        public override void CollectAll(GameObject self)
        {
            List<INeedSceneReferences<TConcrete>> injectables = SingletonList<INeedSceneReferences<TConcrete>>.List;

            List<GameObject> rootObjects = SingletonList<GameObject>.List;
            rootObjects.Clear();
            self.scene.GetRootGameObjects(rootObjects);

            List<INeedSceneReferences<TConcrete>> tempList = SceneRootComponentTempList<INeedSceneReferences<TConcrete>>.List;

            foreach (GameObject r in rootObjects)
            {
                r.GetComponentsInChildren(true, tempList);
                injectables.AddRange(tempList);
            }
        }

        public override void Call<T>(T self)
        {
            List<INeedSceneReferences<TConcrete>> list = SingletonList<INeedSceneReferences<TConcrete>>.List;
            foreach (INeedSceneReferences<TConcrete> i in list)
            {
                try
                {
                    i.Init(self as TConcrete);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}