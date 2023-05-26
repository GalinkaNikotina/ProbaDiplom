using System;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IUserOutput
    {
        String StopGameAndShowFinishGameMessage(Action action, int textBlockLength);
        String ShowCurrentSpeed();
    }
}