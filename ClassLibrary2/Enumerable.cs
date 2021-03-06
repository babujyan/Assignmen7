﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLINQ
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> ExtensionWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) throw new  Exception("Source argument is Null");
            if (predicate == null) throw new Exception("Predicate argument is Null");
                        
            return new WhereEnumerable<TSource>(source, predicate);
        }

        public static IEnumerable<TResult> ExtensionSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new Exception("Source argument is Null");
            if (selector == null) throw new Exception("Selector argument is Null");

            return new SelectEnumerable<TSource,TResult>(source,selector);
        } 

        public static IOrderedEnumerable<TSource> ExtensionOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable
        {
            if (source == null) throw new Exception("Source argument is Null");
            if (keySelector == null) throw new Exception("KeySelector argument is Null");

            return new OrderedEnumerable<TSource, TKey>(source,keySelector, true);
        }

        public static List<TSource> ExtensionToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new Exception("Source argument is Null");

            return CoppyToList<TSource>.ToList(source);
        }

        public static Dictionary<TKey, TSource> ExtensionToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null) throw new Exception("Source argument is Null");
            if (keySelector == null) throw new Exception("KeySelector argument is Null");

            return CoppyToDictinoary<TKey, TSource>.ToDictinoary(source, keySelector);
        }

        public static IEnumerable<IGrouping<TKey, TSource>> ExtensionGroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey: IComparable
        {
            if (source == null) throw new Exception("Source argument is Null");
            if (keySelector == null) throw new Exception("KeySelector argument is Null");

            return new Grouping<TKey, TSource>(source,keySelector);
        }
    }
}
