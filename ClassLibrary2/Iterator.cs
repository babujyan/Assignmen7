﻿using System.Collections.Generic;
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
}