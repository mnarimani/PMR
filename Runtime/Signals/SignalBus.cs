using System;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace PMR.Signals
{
    public class SignalBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _subs = new Dictionary<Type, List<Action<object>>>(1);

        public SignalHandle Subscribe<T>(Action<T> callback)
        {
            WarnValueType<T>();
            
            // ReSharper disable once ConvertToLocalFunction
            Action<object> action = v => callback((T) v);
            
            return SubscribeInternal<T>(action);
        }
        
        public SignalHandle Subscribe<T>(Action callback)
        {
            WarnValueType<T>();
            
            // ReSharper disable once ConvertToLocalFunction
            Action<object> action = v => callback();
            
            return SubscribeInternal<T>(action);
        }

        private SignalHandle SubscribeInternal<T>(Action<object> callback)
        {
            WarnValueType<T>();
            
            if (!_subs.TryGetValue(typeof(T), out List<Action<object>> list))
            {
                list = new List<Action<object>>(1);
                _subs.Add(typeof(T), list);
            }

            list.Add(callback);

            return new SignalHandle(() => list.Remove(callback));
        }

        public bool Fire<T>(T obj)
        {
            WarnValueType<T>();
            
            if (!_subs.TryGetValue(typeof(T), out List<Action<object>> list) || list.Count == 0)
            {
                return false;
            }
            
            for (var i = list.Count - 1; i >= 0; i--)
            {
                list[i].Invoke(obj);
            }

            return true;
        }

        public bool Fire<T>()
        {
            return Fire<T>(default);
        }
        
        
        [Conditional("UNITY_EDITOR")]
        private static void WarnValueType<T>()
        {
            if (typeof(T).IsValueType)
            {
                Debug.LogWarning($"[PMR] Type `{typeof(T).Name}` is value type. Using it as signal will cause boxing allocations. Consider using a class instead.");
            }
        }
    }
}