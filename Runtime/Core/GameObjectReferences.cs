using UnityEngine;

namespace PMR
{
    [DefaultExecutionOrder(-999)]
    public abstract class GameObjectReferences : InSceneReferences, IRequire<SceneReferences>
    {
        private SceneReferences _scene;

        public void Init(SceneReferences dep)
        {
            _scene = dep;
        }

        protected override void DeclareTypes()
        {
            DeclareSelfAs<IPocoRegistry>();
            DeclareSelfAs<IReferences>();
            DeclareSelfAs<GameObjectReferences>();
        }

        protected void DeclareSelfAs<T>() where T : class
        {
            AddType(new GameObjectTypeDeclaration<T>());
        }

        private protected override void InitializeInstantiatedPrefab(GameObject obj)
        {
            _scene.Initialize(obj);
            Initialize(obj);
        }

        private protected override void InitializeInstantiatedObject<T>(T obj)
        {
            _scene.Initialize(obj);
            Initialize(obj);
        }
    }
    
    [DefaultExecutionOrder(-999)]
    public abstract class GameObjectReferences<TConcrete> : GameObjectReferences
        where TConcrete : class
    {
        protected override void DeclareTypes()
        {
            base.DeclareTypes();
            DeclareSelfAs<GameObjectReferences<TConcrete>>();
            DeclareSelfAs<TConcrete>();
        }
    }
}