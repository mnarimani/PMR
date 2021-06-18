using System;

namespace PMR.Signals
{
    public readonly struct SignalHandle : IDisposable
    {
        private readonly Action _unsubscribe;

        public SignalHandle(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }

        public void Unsubscribe()
        {
            Dispose();
        }
        
        public void Dispose()
        {
            _unsubscribe();
        }
    }
}