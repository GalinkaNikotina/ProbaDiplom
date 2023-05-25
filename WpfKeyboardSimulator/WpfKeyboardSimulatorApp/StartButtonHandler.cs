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

namespace WpfKeyboardSimulatorApp
{
   
    public partial class MainWindow : Window
    { 

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            Text.Clear();
            TextBlockCheck.Text = "";
            IsStop = false;
            SpeedLabel.Content = "Скорость 0 симв/мин";
            ErorCountLabel.Content = "Ошибки : 0";
            ErrorCount = 0;
            Timer.Start();
            ButtonStop.IsEnabled = true;
            ButtonStart.IsEnabled = false;
            Random random = new Random();
            int num = random.Next(0, Texts[levelDifficulty].Count);
            TextForRead = Texts[levelDifficulty][num];
            TextBlockRead.Text = TextForRead;
        }
    }
}
