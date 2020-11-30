using System;

namespace AutoVersionsDB.Helpers
{
    //https://social.msdn.microsoft.com/forums/en-us/824b42e7-07f9-4a67-bd04-6b296d4fdc11/net-framework-hidden-disposables?forum=msdnmagazine
    //https://docs.microsoft.com/en-us/archive/msdn-magazine/2016/november/net-framework-hidden-disposables
    public sealed class DisposableWrapper<T> : IDisposable where T : class
    {
        public T Instance { get; private set; }

        public DisposableWrapper(T objectToWrap)
        {
            Instance = objectToWrap;
        }

        private bool disposedValue = false; // To detect redundant calls

        public void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    var disposableObject = Instance as IDisposable;
                    if (disposableObject != null)
                    {
                        disposableObject.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }

    public static class DisposableWrapperExtensions
    {
        public static DisposableWrapper<T> AsDisposable<T>(this T objectToWrap) where T : class
        {
            return new DisposableWrapper<T>(objectToWrap);
        }
    }
}
