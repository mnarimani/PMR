using UnityEngine;

namespace PMR
{
    [DefaultExecutionOrder(-999)]
    public abstract class GameObjectReferences<TConcrete, TProjectRefs> : InSceneReferences, INeedSceneReferences<IReferences>
        where TConcrete : class
        where TProjectRefs : ProjectReferences<TProjectRefs>
    {
        private IReferences _sceneRefs;

        public void Init(IReferences scene)
        {
            _sceneRefs = scene;
        }

        protected override void DeclareTypes()
        {
            DeclareSelfAs<IPocoRegistry>();
            DeclareSelfAs<IReferences>();
            DeclareSelfAs<TConcrete>();
        }

        protected void DeclareSelfAs<T>() where T : class
        {
            AddType(new GameObjectTypeDeclaration<T>());
        }

        private protected override void InitializeInstantiatedPrefab(GameObject obj)
        {
            _sceneRefs.Initialize(obj);
            Initialize(obj);
        }

        private protected override void InitializeInstantiatedObject<T>(T obj)
        {
            _sceneRefs.Initialize(obj);
            Initialize(obj);
        }
    }
}