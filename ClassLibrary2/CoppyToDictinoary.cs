using System;
using System.Collections.Generic;

namespace MyLINQ
{
    internal static class CoppyToDictinoary<TKey, TSource>
    {
        public static Dictionary<TKey, TSource> ToDictinoary(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {            
            Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>();

            foreach (TSource i in source)
            {
                dictionary.Add(keySelector(i), i);
            }

            return dictionary;
        }
    }
}
