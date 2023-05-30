using System;

namespace WpfKeyboardSimulatorApp.model
{
    /// <summary>
    /// пользовательский вывод
    /// </summary>
    public interface IUserOutput
    {
        String StopGameAndShowFinishGameMessage(Action action, int textBlockLength);
        String ShowCurrentSpeed();
    }
}