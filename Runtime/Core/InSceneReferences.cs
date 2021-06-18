using System;
using System.Collections.Generic;
using System.Diagnostics;
using PMR.Signals;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PMR
{
    public abstract class InSceneReferences : MonoBehaviour, IReferences, ISignalProvider, IPocoRegistry
    {
        private List<ITypeDeclaration> _typeDeclarations = new List<ITypeDeclaration>(4);

        private SignalBus _signalBus;
        private bool _isCalling;
        
        private List<ITickable> _tickables;
        private List<IFixedTickable> _fixedTickables;
        private List<IDisposable> _disposables;
        private Queue<IAwakable> _pendingInitCalls;

        public SignalBus SignalBus => (_signalBus ??= new SignalBus());

        protected virtual void Awake()
        {
            PrepareComponents();

            DeclareTypes();

            RegisterPocoTypes();

            foreach (ITypeDeclaration t in _typeDeclarations)
            {
                t.CollectAll(gameObject);
            }

            _isCalling = true;
            foreach (ITypeDeclaration t in _typeDeclarations)
            {
                t.Call(this);
            }

            _isCalling = false;
        }

        private void OnEnable()
        {
            if (_pendingInitCalls != null)
            {
                while (_pendingInitCalls.Count > 0)
                {
                    _pendingInitCalls.Dequeue().Awake();
                }
            }
        }

        private void OnDestroy()
        {
            if (_disposables != null)
            {
                foreach (IDisposable d in _disposables)
                {
                    try
                    {
                        d.Dispose();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }

            // Allow for poco files to be garbage collected
            _tickables = null;
            _fixedTickables = null;
            _disposables = null;
            _pendingInitCalls = null;
        }

        private void Update()
        {
            if (_tickables == null) return;

            foreach (ITickable t in _tickables)
            {
                try
                {
                    t.Tick(Time.deltaTime);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_fixedTickables == null) return;

            foreach (IFixedTickable t in _fixedTickables)
            {
                try
                {
                    t.FixedTick(Time.fixedDeltaTime);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public T RegisterPocoType<T>() where T : new()
        {
            var obj = new T();
            RegisterPocoType(obj);
            return obj;
        }

        public void RegisterPocoType<T>(T obj)
        {
            WarnValueType<T>();

            if (obj is ITickable tickable)
            {
                _tickables ??= new List<ITickable>(1);
                _tickables.Add(tickable);
            }

            if (obj is IFixedTickable fixedTickable)
            {
                _fixedTickables ??= new List<IFixedTickable>(1);
                _fixedTickables.Add(fixedTickable);
            }

            if (obj is IAwakable initializable)
            {
                if (isActiveAndEnabled)
                {
                    initializable.Awake();
                }
                else
                {
                    _pendingInitCalls ??= new Queue<IAwakable>(1);
                    _pendingInitCalls.Enqueue(initializable);
                }
            }

            if (obj is IDisposable disposable)
            {
                _disposables ??= new List<IDisposable>(1);
                _disposables.Add(disposable);
            }

            InitializeInstantiatedObject(obj);
        }

        public void RemovePocoType<T>(T obj)
        {
            WarnValueType<T>();

            if (obj is IFixedTickable ft)
            {
                _fixedTickables?.Remove(ft);
            }

            if (obj is ITickable t)
            {
                _tickables?.Remove(t);
            }

            if (obj is IDisposable d)
            {
                _disposables?.Remove(d);
            }
        }

        public void Initialize(GameObject obj)
        {
            if (_isCalling)
                throw new PoorManException("Cannot initialize an object while another initialization is being called.");
            
            foreach (ITypeDeclaration t in _typeDeclarations)
            {
                t.Collect(obj);
            }

            _isCalling = true;
            foreach (ITypeDeclaration t in _typeDeclarations)
            {
                t.Call(this);
            }
            _isCalling = false;
        }

        public void Initialize<T>(T obj)
        {
            if (_isCalling)
                throw new PoorManException("Cannot initialize an object while another initialization is being called.");
            
            if (obj is GameObject go)
            {
                Initialize(go);
                return;
            }

            foreach (ITypeDeclaration t in _typeDeclarations)
            {
                t.Collect(obj);
            }

            _isCalling = true;
            foreach (ITypeDeclaration t in _typeDeclarations)
            {
                t.Call(this);
            }

            _isCalling = false;
        }

        public T InstantiatePrefab<T>(T prefab) where T : Component
        {
            GameObject prefabGO = prefab.gameObject;

            bool prefabWasActive = prefabGO.activeSelf;
            prefabGO.SetActive(false);

            T clone = Instantiate(prefab);
            InitializeInstantiatedPrefab(clone.gameObject);

            clone.gameObject.SetActive(prefabWasActive);
            prefabGO.SetActive(prefabWasActive);
            return clone;
        }

        public GameObject InstantiatePrefab(GameObject prefab)
        {
            bool prefabWasActive = prefab.activeSelf;
            prefab.SetActive(false);

            GameObject clone = Instantiate(prefab);
            InitializeInstantiatedPrefab(clone);

            clone.SetActive(prefabWasActive);
            prefab.SetActive(prefabWasActive);
            return clone;
        }

        public T CreateObject<T>() where T : new()
        {
            var n = new T();
            InitializeInstantiatedObject(n);
            return n;
        }

        internal void AddType(ITypeDeclaration type)
        {
            _typeDeclarations.Add(type);
        }

        [Conditional("UNITY_EDITOR")]
        private static void WarnValueType<T>()
        {
            if (typeof(T).IsValueType)
            {
                Debug.LogWarning($"[PMR] Type `{typeof(T).Name}` is value type. Using it as POCO in SceneRefs/GameObjectRefs will cause boxing allocations. Consider using a class instead.");
            }
        }
        
        protected virtual void PrepareComponents()
        {
        }

        protected virtual void RegisterPocoTypes()
        {
        }

        protected abstract void DeclareTypes();

        private protected abstract void InitializeInstantiatedPrefab(GameObject obj);

        private protected abstract void InitializeInstantiatedObject<T>(T obj);
    }
}