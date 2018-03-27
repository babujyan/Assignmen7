using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MyLINQ
{
    abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerator<TSource>
    { 
        internal TSource current;
        internal IEnumerator<TSource> enumerator;

        public Iterator() { }

        public TSource Current
        {
            get { return current; }
        }

        //public virtual void Dispose()
        //{
            
        //    current = default(TSource);
        //    state = -1;
        //}

        void IEnumerator.Reset()
        {
            this.enumerator.Reset();
        }

        public abstract bool MoveNext();

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public abstract IEnumerator<TSource> GetEnumerator();
       

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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
        // ~Iterator() {
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

    class WhereEnumerable<TSource> : Iterator<TSource>
    {
        IEnumerable<TSource> source;
        Func<TSource, bool> predicate;
        

        public WhereEnumerable(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override IEnumerator<TSource> GetEnumerator()
        {
            return new WhereIterator(this.source, this.predicate);
        }

        public override bool MoveNext()
        {
            return false;
        }

        internal class WhereIterator : IEnumerator<TSource>
        {

            private IEnumerator<TSource> whereEnumerator;


            Func<TSource, bool> predicate;

            public TSource Current
            {
                get
                {
                    return this.whereEnumerator.Current;
                }
            }
            object IEnumerator.Current
            {
                get
                {
                    return this.whereEnumerator.Current;
                }
            }

            public WhereIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
            {
                this.whereEnumerator = source.GetEnumerator();
                this.predicate = predicate;
            }

            public bool MoveNext()
            {
                 return this.whereEnumerator.MoveNext();
            }

            public void Reset()
            {
                this.whereEnumerator.MoveNext();
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
            // ~WhereIterator() {
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


    class SelectEnumerable<TSource, TResult> : Iterator<TResult>
    {
        IEnumerable<TSource> source;
        Func<TSource, TResult> selector;
        

        public SelectEnumerable(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override IEnumerator<TResult> GetEnumerator()
        {
            return new SelectIterator(this.source, this.selector);
        }

        public override bool MoveNext()
        {
            return false;
        }

        internal class SelectIterator : IEnumerator<TResult>
        {
            IEnumerator<TSource> selectEnumerator;
            private Func<TSource, TResult> selector;

            public TResult Current
            {
                get
                {
                    return this.selector(this.selectEnumerator.Current);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.selector(this.selectEnumerator.Current);
                }
            }


            public SelectIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
            {
                this.selectEnumerator = source.GetEnumerator();
                this.selector = selector;
            }

            public bool MoveNext()
            {
                return this.selectEnumerator.MoveNext();
            }

            public void Reset()
            {
                this.selectEnumerator.Reset();
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
            // ~SelectIterator() {
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
}