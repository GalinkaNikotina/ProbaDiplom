using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfKeyboardSimulatorApp.model;

namespace WpfKeyboardSimulatorApp
{
 
    public partial class MainWindow : Window
    {

        private void Finish(bool isFinis)
        {
            Timer.Stop();

            StringBuilder msg = new StringBuilder(150);

            if (isFinis)
            {
                msg.Append("Вы завершили тренинг!\r\r");
            }
            else
            {
                msg.Append("Вы не завершили тренинг!\r\r");
            }

            CalcSymbols();

            msg.Append($"Выбран уровень : {GameVariables.LevelOfDifficulty}\r Результат:\r Скорость набора: {GameVariables.Speed} симв/мин\r");
            msg.Append($"Ошибки: {GameVariables.LevelOfDifficulty}\r");

            if (Text.Length == 0)
            {
                msg.Append($"Правильность набора 0 %");
            }
            else if (GameVariables.ErrorCount == 0 && Text.Length != 0)
            {
                msg.Append($"Правильность набора {TextBlockCheck.Text.Length * 100 / Text.Length} %");
            }
            else
            {
                msg.Append($"Правильность набора {(TextBlockCheck.Text.Length - GameVariables.ErrorCount) * 100 / Text.Length} %");
            }


            MessageBox.Show(msg.ToString(), this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}