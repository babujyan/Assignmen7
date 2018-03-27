using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLINQ
{
    class Grouping<TKey, TSource> : IEnumerable<IGrouping<TKey, TSource>> where TKey : IComparable
    {

        private IEnumerable<TSource> source;

        internal Func<TSource, TKey> keySelector;


        public Grouping(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            this.source = source;
            this.keySelector = keySelector;
        }


        IEnumerator<IGrouping<TKey, TSource>> IEnumerable<IGrouping<TKey, TSource>>.GetEnumerator()
        {
            return new GroupingIterator(source, keySelector);
        }

        public IEnumerator GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal class GroupingIterator : IEnumerator<IGrouping<TKey, TSource>>
        {
            private Dictionary<TKey, Sourse> dictionary;

            IEnumerable<TSource> source;
            private Func<TSource, TKey> keySelector;
            private IEnumerator<KeyValuePair<TKey,Sourse>> enumerator;

            public GroupingIterator(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
            {
                this.source = source;
                this.keySelector = keySelector;
               
                this.Fill();
                this.enumerator = this.dictionary.GetEnumerator();
            }

            public IGrouping<TKey, TSource> Current
            {
                get
                {
                    return this.enumerator.Current.Value;
                }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                return this.enumerator.MoveNext();
            }

            public void Reset()
            {
                this.enumerator.Reset();
            }

            private void Fill()
            {
                this.dictionary = new Dictionary<TKey, Sourse>();
                
                foreach(var i in source)
                {
                    var key = keySelector(i);
                    if(this.dictionary.ContainsKey(key))
                    {
                        this.dictionary[key].Add(i);
                    }
                    else
                    {
                        Sourse sourse = new Sourse(keySelector);
                        sourse.Add(i);
                        this.dictionary.Add(key,sourse);
                    }
                }
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
            // ~GroupingIterator() {
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

        internal class Sourse : IGrouping<TKey, TSource>
        {
            List<TSource> source;
            internal Func<TSource, TKey> keySelector;


            public Sourse(Func<TSource, TKey> keySelector)
            {
                this.keySelector = keySelector;
                source = new List<TSource>();
            }

            internal void Add(TSource i)
            {
                source.Add(i);
            }

            public IEnumerator<TSource> GetEnumerator()
            {
                return this.source.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.source.GetEnumerator();
            }

            internal List<TSource> Value
            {
                get
                {
                    return source;
                }
            }

            public TKey Key
            {
                get
                {
                    return this.keySelector(source[0]);
                }
                
            }
        }
    }
}
