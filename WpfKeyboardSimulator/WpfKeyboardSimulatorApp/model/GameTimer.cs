using System;
using System.Windows.Threading;

namespace WpfKeyboardSimulatorApp.model
{
    public class GameTimer : IGameTimer
    {
        private DispatcherTimer _timer;

        public GameTimer()
        {
            _timer = new DispatcherTimer();
        }

        public void InitTimer(Action<object,EventArgs> timerTick)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timer.Tick += timerTick.Invoke;
        }

        public void StopTimer()
        {
            this._timer.Stop();
        }
    }
}