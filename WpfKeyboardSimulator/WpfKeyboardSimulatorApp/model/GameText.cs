using System.Text;

namespace WpfKeyboardSimulatorApp.model
{
    public class GameText : IGameText
    {
        public StringBuilder Text { get; set; } = new StringBuilder(100);

        public float TextRemainingPercent { get; set; }
    }
}