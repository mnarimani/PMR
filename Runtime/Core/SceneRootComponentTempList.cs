using System.Collections.Generic;

namespace PMR
{
    /// <summary>
    /// A class to hold temporary components that were fetched in Scene Ref. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal static class SceneRootComponentTempList<T>
    {
        private static List<T> _list;
        public static List<T> List => (_list ??= new List<T>(10));
    }
}