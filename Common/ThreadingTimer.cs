using System;
using System.Threading;

namespace HRApp.Common
{
    internal class ThreadingTimer : ITimer, IDisposable
    {
        private Timer? _timer;

        public void Start(TimeSpan dueTime, TimeSpan period, object state)
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }

            _timer = new Timer(TimerCallback, state, dueTime, period);
        }

        public void Start(TimeSpan dueTime, TimeSpan period)
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }

            _timer = new Timer(TimerCallback, null, dueTime, period);
        }

        public void Dispose()
        {
            if(_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }

        private void TimerCallback(object? state)
        {
            Tick?.Invoke(this, new TimerEventArgs(state));
        }

        public void Stop()
        {
            if(_timer == null)
            {
                return;
            }

            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public event TimerEventHandler? Tick;
    }
}
