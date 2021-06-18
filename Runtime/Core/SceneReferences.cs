using UnityEngine;

namespace PMR
{
    [DefaultExecutionOrder(-9999)]
    public class SceneReferences<TConcrete, TProjectRefs> : InSceneReferences
        where TProjectRefs : ProjectReferences<TProjectRefs>
        where TConcrete : class
    {
        protected override void DeclareTypes()
        {
            ProjectReferences<TProjectRefs>.GetInstance().CurrentScene = this;
            ProjectReferences<TProjectRefs>.GetInstance().DeclareTypes();
            ProjectReferences<TProjectRefs>.GetInstance().CurrentScene = null;

            DeclareSelfAs<IPocoRegistry>();
            DeclareSelfAs<IReferences>();
            DeclareSelfAs<TConcrete>();
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
}