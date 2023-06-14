using System.Text;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IGameText
    {
        void Init(Level level);
        void Restart(Level level);
        /// <summary>
        /// очистка словаря
        /// </summary>
        void ClearCurrentDictionary();
        /// <summary>
        /// предложения из словаря выбранного уровня
        /// </summary>
        /// <param name="level"></param>
        void LoadDictionaryForLevel(Level level);
        void UpdateUserInput(StringBuilder input);
    }
}