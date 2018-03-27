using System.Collections.Generic;


namespace MyLINQ
{
    internal static class CoppyToList<TSource>
    {
        public static List<TSource> ToList(IEnumerable<TSource> source)
        {
            List<TSource> list = new List<TSource>();

            foreach (TSource i in source)
            {
                list.Add(i);
            }

            return list;
        }
    }
}
