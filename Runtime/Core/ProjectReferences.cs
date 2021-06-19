using System;
using PMR.Signals;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;

#endif

namespace PMR
{
    public abstract class ProjectReferences : ScriptableObject, ISignalProvider
    {
        internal InSceneReferences CurrentScene;

        private protected static Func<ProjectReferences> CreateInstance;
        
        private SignalBus _signalBus;
        private static ProjectReferences _instance;

        public SignalBus SignalBus => (_signalBus ??= new SignalBus());

        protected internal virtual void DeclareTypes()
        {
            DeclareSelfAs<ProjectReferences>();
        }

        protected void DeclareSelfAs<TType>() where TType : class
        {
            CurrentScene.AddType(new ProjectTypeDeclaration<TType>());
        }

        internal static ProjectReferences GetInstance()
        {
            const string fileName = "ProjectReferences";

            if (_instance == null)
                _instance = Resources.Load<ProjectReferences>(fileName);
                
            if (_instance == null && CreateInstance != null)
            {
                _instance = CreateInstance();

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
    
    public abstract class ProjectReferences<T> : ProjectReferences where T : ProjectReferences<T>
    {
        static ProjectReferences()
        {
            CreateInstance = CreateInstance<T>;
        }

        protected internal override void DeclareTypes()
        {
            DeclareSelfAs<ProjectReferences<T>>();
            DeclareSelfAs<T>();
        }
    }
}