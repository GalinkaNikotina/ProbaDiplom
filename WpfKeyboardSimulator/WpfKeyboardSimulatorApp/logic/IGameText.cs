using System.Text;

namespace WpfKeyboardSimulatorApp.model
{
    public interface IGameText
    {
        void Init(Level level);
        void Restart(Level level);
        void ClearCurrentDictionary();
        void LoadDictionaryForLevel(Level level);
        void UpdateUserInput(StringBuilder input);
    }
}