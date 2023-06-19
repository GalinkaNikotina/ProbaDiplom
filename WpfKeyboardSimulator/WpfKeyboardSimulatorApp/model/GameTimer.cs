using System;
using System.Windows.Threading;

namespace WpfKeyboardSimulatorApp.model
{
    public class GameTimer : IGameTimer
    {
        private DispatcherTimer _timer;

        //обработчик события, добавленный методом Init Timer

        public GameTimer()
        {
            _timer = new DispatcherTimer();
        }

        //метод, называемый Init Timer, который принимает делегат действия,
        //называемый timer Tick, в качестве параметра. Метод устанавливает
        //интервал _timer равным 1 миллисекунде, используя объект TimeSpan,
        //и добавляет обработчик события к событию Tick _timer, используя метод Invoke делегата timerTick.
        public void InitTimer(Action<object,EventArgs> timerTick)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timer.Tick += timerTick.Invoke;
        }

        public void StopTimer()
        {
            this._timer.Stop();
        }

        public void StartTimer()
        {
            _timer.Start();
        }
    }
}