using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace MyLINQ
{
    class OrderedEnumerable<TSource, TKey> : IOrderedEnumerable<TSource>, IEnumerator<TSource> where TKey : IComparable //, IEnumerator<TSource>
    {
        IEnumerable<TSource> source;
        Func<TSource, TKey> keySelector;
        IEnumerator<KeyValuePair<TKey, TSource>> enumerator;
        bool descending;

        public TSource Current
        {
            get
            {
                return this.enumerator.Current.Value;
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool descending)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.descending = descending;
        }


        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey1>(Func<TSource, TKey1> keySelector, IComparer<TKey1> comparer, bool descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            List<KeyValuePair<TKey, TSource>> list = new List<KeyValuePair<TKey, TSource>>();
            foreach (var item in this.source)
            {
                list.Add(new KeyValuePair<TKey, TSource>(this.keySelector(item), item));
            }
            Sort(list);
            this.enumerator = list.GetEnumerator();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    
        public static List<KeyValuePair<TKey, TSource>> Sort(List<KeyValuePair<TKey,TSource>> list)
        {
            Quick(list, 0, list.Count() - 1);

            
            return list;
        }

        private static void Quick(List<KeyValuePair<TKey, TSource>> array, int left,int right)
        {
            if (left < right)
            {
                int p = Partition(array, left, right);
                Quick(array, left, p - 1);
                Quick(array, p+1, right);
            }
        }

        /// <summary>
        /// Partition for Quick sort.
        /// </summary>
        /// <param name="array"> Array whic is going to be sorted. </param>
        /// <param name="low"> Lowest index for sorting. </param>
        /// <param name="high"> Highest index for sorting. </param>
        /// <returns>Index</returns>
        private static int Partition(List<KeyValuePair<TKey, TSource>> array, int low, int high)
        {
            KeyValuePair<TKey, TSource> pivot = array[high];
            int index = low;
            KeyValuePair<TKey, TSource> temp;


            for (int i = low; i < high; i++)
            {
                if (array[i].Key.CompareTo(pivot.Key) <= 0)
                {
                    temp = array[i];
                    array[i] = array[index];
                    array[index] = temp;
                    index++;
                }
            }

            temp = array[high];
            array[high] = array[index];
            array[index] = temp;

            return index;
        }

        public bool MoveNext()
        {
            return this.enumerator.MoveNext();
        }

        public void Reset()
        {
            this.enumerator.MoveNext();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~OrderedEnumerable() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}