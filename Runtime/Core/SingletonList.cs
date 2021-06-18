using System.Collections.Generic;

namespace PMR
{
    internal static class SingletonList<T>
    {
        private static List<T> _list;
        public static List<T> List => (_list ??= new List<T>(10));
    }
}