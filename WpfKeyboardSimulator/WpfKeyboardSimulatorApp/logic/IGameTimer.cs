using System;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IGameTimer
    {
        void InitTimer(Action<object,EventArgs> timerTick);
        void StopTimer();
    }
}