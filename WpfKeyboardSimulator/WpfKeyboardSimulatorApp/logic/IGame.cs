using System.Text;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IGame
    {
        void IncreaseTimerOneSecond();
        /// <summary>
        /// удаление текста текущего
        /// </summary>
        void RemoveCurrentText();
        bool InitText();
        /// <summary>
        /// изменение уровня
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool ChangeGameLevel(Level level);
        
        void CalculateResult(int res);
        
        StringBuilder ExtractUserInput();
        /// <summary>
        /// обновление ввода
        /// </summary>
        /// <param name="newInput"></param>
        void UpdateUserInput(StringBuilder newInput);
        void IncrementError();
        void StopGame();
        string GameRestart();

        void ChangeCaseSensitive(bool isSensitive);
    }
}