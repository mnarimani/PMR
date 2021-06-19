using UnityEngine;

namespace PMR
{
    [DefaultExecutionOrder(-9999)]
    public class SceneReferences : InSceneReferences
    {
        protected override void DeclareTypes()
        {
            ProjectReferences.GetInstance().CurrentScene = this;
            ProjectReferences.GetInstance().DeclareTypes();
            ProjectReferences.GetInstance().CurrentScene = null;

            DeclareSelfAs<SceneReferences>();
            DeclareSelfAs<IPocoRegistry>();
            DeclareSelfAs<IReferences>();
        }

        protected void DeclareSelfAs<T>() where T : class
        {
            AddType(new SceneTypeDeclaration<T>());
        }

        private protected override void InitializeInstantiatedPrefab(GameObject obj)
        {
            Initialize(obj);
        }

        private protected override void InitializeInstantiatedObject<T>(T obj)
        {
            Initialize(obj);
        }
    }
    
    [DefaultExecutionOrder(-9999)]
    public class SceneReferences<TConcrete> : SceneReferences where TConcrete : class
    {
        protected override void DeclareTypes()
        {
            base.DeclareTypes();
            DeclareSelfAs<SceneReferences<TConcrete>>();
            DeclareSelfAs<TConcrete>();
        }

        private protected override void InitializeInstantiatedPrefab(GameObject obj)
        {
            Initialize(obj);
        }

        private protected override void InitializeInstantiatedObject<T>(T obj)
        {
            Initialize(obj);
        }
    }
}