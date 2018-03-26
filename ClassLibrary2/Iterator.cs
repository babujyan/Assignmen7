using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MyLINQ
{
    abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerator<TSource>
    {
        internal int state;
        internal TSource current;

        public Iterator()
        {
            
        }

        public TSource Current
        {
            get { return current; }
        }

        public abstract Iterator<TSource> Clone();

        //public virtual void Dispose()
        //{
            
        //    current = default(TSource);
        //    state = -1;
        //}

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }

        public abstract bool MoveNext();

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            if (state == 0)
            {
                state = 1;
                return this;
            }

            Iterator<TSource> duplicate = Clone();
            duplicate.state = 1;
            return duplicate;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    current = default(TSource);
                    state = -1;

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

    class WhereIterator<TSource> : Iterator<TSource>
    {
        IEnumerable<TSource> source;
        Func<TSource, bool> predicate;
        IEnumerator<TSource> enumerator;

        public WhereIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        public override Iterator<TSource> Clone()
        {
            return new WhereIterator<TSource>(source, predicate);
        }

        private bool disposedValue = false;

        protected override void Dispose(bool disposing)
        {
            
        
            if (!disposedValue)
            {
                if (disposing)
                {
                    current = default(TSource);
                    state = -1;

                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        

            if (enumerator is IDisposable) ((IDisposable)enumerator).Dispose();
            enumerator = null;
            base.Dispose();
        }

        public override bool MoveNext()
        {
            switch (state)
            {
                case 1:
                    enumerator = source.GetEnumerator();
                    state = 2;
                    goto case 2;
                case 2:
                    while (enumerator.MoveNext())
                    {
                        TSource item = enumerator.Current;
                        if (predicate(item))
                        {
                            current = item;
                            return true;
                        }
                    }
                    Dispose();
                    break;
            }
            return false;
        }
    }


    class SelectIterator<TSource, TResult> : Iterator<TResult>
    {
        IEnumerable<TSource> source;
        Func<TSource, TResult> selector;
        IEnumerator<TSource> enumerator;

        public SelectIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        public override Iterator<TResult> Clone()
        {
            return new SelectIterator<TSource, TResult>(source, selector);
        }

        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (enumerator is IDisposable) ((IDisposable)enumerator).Dispose();
                    enumerator = null;
                    base.Dispose();

                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public override bool MoveNext()
        {
            switch (state)
            {
                case 1:
                    enumerator = source.GetEnumerator();
                    state = 2;
                    goto case 2;
                case 2:
                    while (enumerator.MoveNext())
                    {
                        TSource item = enumerator.Current;
                        current = selector(item);
                        return true;
                        
                    }
                    Dispose();
                    break;
            }
            return false;
        }
    }
}
