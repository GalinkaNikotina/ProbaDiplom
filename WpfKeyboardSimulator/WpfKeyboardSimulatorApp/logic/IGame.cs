using System;
using System.Collections.Generic;
using System.Text;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IGame
    {
        void IncreaseTimerOneSecond();
        void RemoveCurrentText();
        void UpdateCurrentText(List<String> newText);
        bool InitText();

        bool ChangeGameLevel(Level level);
        void CalculateResult(int res);
        
        StringBuilder ExtractUserInput();
        void UpdateUserInput(StringBuilder newInput);
        void IncrementError();
        void StopGame();
    }
}