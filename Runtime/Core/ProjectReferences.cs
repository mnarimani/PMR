using PMR.Signals;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;

#endif

namespace PMR
{
    public abstract class ProjectReferences<T> : ScriptableObject, ISignalProvider where T : ProjectReferences<T>
    {
        internal InSceneReferences CurrentScene;
        private SignalBus _signalBus;
        private static T _instance;

        public SignalBus SignalBus => (_signalBus ??= new SignalBus());

        protected internal virtual void DeclareTypes()
        {
            DeclareSelfAs<T>();
        }

        protected void DeclareSelfAs<TType>() where TType : ProjectReferences<TType>
        {
            CurrentScene.AddType(new ProjectTypeDeclaration<T, TType>());
        }

        internal static T GetInstance()
        {
            const string fileName = "ProjectReferences";

            if (_instance == null)
                _instance = Resources.Load<T>(fileName);
                
            if (_instance == null)
            {
                _instance = CreateInstance<T>();

#if UNITY_EDITOR
                if (!Directory.Exists(Application.dataPath + "/Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");
                AssetDatabase.CreateAsset(_instance, "Assets/Resources/" + fileName + ".asset");
                AssetDatabase.SaveAssets();
#endif
            }

            return _instance;
        }
    }
}