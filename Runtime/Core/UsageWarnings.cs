using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace PMR
{
    public static class UsageWarnings
    {
        [Conditional("UNITY_EDITOR")]
        public static void WarnValueType<T>(string message)
        {
            if (typeof(T).IsValueType)
            {
                Debug.LogWarning(message);
            }
        }
    }
}