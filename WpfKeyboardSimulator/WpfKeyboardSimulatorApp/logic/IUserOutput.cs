using System;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IUserOutput
    {
        String StopGameAndShowFinishGameMessage(bool isFinish, Action action, int textBlockLength);
        String ShowCurrentSpeed();
    }
}