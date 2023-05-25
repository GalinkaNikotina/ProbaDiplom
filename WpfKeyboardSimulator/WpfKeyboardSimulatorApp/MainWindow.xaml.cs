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
using WpfKeyboardSimulatorApp.repository;
using WpfKeyboardSimulatorApp.symbol;

namespace WpfKeyboardSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDictionaryRepository _dictionaryRepository;
        public GameVariables GameVariables { get; private set; }
        public bool Symbol { get; set; }


        public string TextForRead { get; set; }
        public bool IsToggled { get; set; } = false;
        public StringBuilder Text { get; set; } = new StringBuilder(100);
        public Dictionary<Level, List<string>> Dictionary { get; set; }

        public DispatcherTimer Timer { get; set; }
        public DispatcherTimer TimerButton { get; set; }
        public Button tmp { get; set; }
        public Button tmp2 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.GameVariables = new GameVariables();
            _dictionaryRepository = new DictionaryRepository();

            Dictionary = new Dictionary<Level, List<string>>();
            Dictionary = _dictionaryRepository.FindByLevel(GameVariables.LevelOfDifficulty);

            ButtonStop.IsEnabled = false;
            GameVariables.IsStop = false;
            GameVariables.IsCaseSensitive = true;

            SetRegister(false);
            TurnOffTabIndex();

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer.Tick += Timer_Tick;

            TimerButton = new DispatcherTimer();
            TimerButton.Interval = new TimeSpan(5000000);
            TimerButton.Tick += TimerButtonUp_Tick;
            TimerButton.Start();
        }

        private void TimerButtonUp_Tick(object sender, EventArgs e)
        {
            KeyUpButton();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GameVariables.TimeSecond += 1;
            CalcSymbols();
        }

        private void CalcSymbols()
        {
            float min = 0;

            int res = TextBlockCheck.Text.Length;

            if (GameVariables.TimeSecond / 1000 > 60)
            {
                min = (float)(GameVariables.TimeSecond / 1000f) / 60f;
                res = (int)(res / min);
            }

            GameVariables.Speed = res;
            SpeedLabel.Content = "Скорость " + res.ToString() + "симв/мин";
        }

        public void SymbolOnOff()
        {
            if (Symbol)
            {
                Oem3.Content = "~";
                D1.Content = "!";
                D2.Content = "@";
                D3.Content = "#";
                D4.Content = "$";
                D5.Content = "%";
                D6.Content = "^";
                D7.Content = "&";
                D8.Content = "*";
                D9.Content = "(";
                D0.Content = ")";
                OemMinus.Content = "_";
                OemPlus.Content = "+";
            }
            else
            {
                Oem3.Content = ",";
                D1.Content = "1";
                D2.Content = "2";
                D3.Content = "3";
                D4.Content = "4";
                D5.Content = "5";
                D6.Content = "6";
                D7.Content = "7";
                D8.Content = "8";
                D9.Content = "9";
                D0.Content = "0";
                OemMinus.Content = "-";
                OemPlus.Content = "=";
            }
        }




        public void TurnOffTabIndex()
        {
            foreach (Grid item in BaseGrid.Children)
            {
                foreach (var element in item.Children)
                {
                    if (element is Button button)
                    {
                        button.IsTabStop = false;
                    }
                }
            }

            SliderDificult.IsTabStop = false;
            CaseSensitive.IsTabStop = false;
        }

        public void KeyUpButton()
        {
            foreach (Grid item in BaseGrid.Children)
            {
                foreach (var element in item.Children)
                {
                    if (element is Button button)
                    {
                        button.Margin = new Thickness(0);
                    }
                }
            }
        }

        public void KeyPress(string keyName)
        {
            foreach (Grid item in BaseGrid.Children)
            {
                foreach (var element in item.Children)
                {
                    if (element is Button button)
                    {
                        if (keyName.Equals(button.Name))
                        {
                            button.Margin = new Thickness(4);
                            tmp = button;
                            return;
                        }
                        else if (keyName.Equals("System") && button.Name.Equals("System1"))
                        {
                            button.Margin = new Thickness(4);
                            tmp2 = button;
                        }
                        else if (keyName.Equals("System") && button.Name.Equals("System2"))
                        {
                            button.Margin = new Thickness(4);
                            tmp = button;
                            return;
                        }
                    }
                }
            }
        }

        public void SetRegister(bool isSmall)
        {
            foreach (Grid item in BaseGrid.Children)
            {
                foreach (var element in item.Children)
                {
                    if (element is Button button)
                    {
                        if (button.Name != Key.CapsLock.ToString() && button.Name != Key.Return.ToString() &&
                            button.Name != Key.LWin.ToString() && button.Name != Key.Back.ToString() &&
                            button.Name.ToString() != "System1" && button.Name.ToString() != "System2" &&
                            button.Name != Key.RWin.ToString() && button.Name != Key.Tab.ToString() &&
                            button.Name != Key.RightShift.ToString() && button.Name != Key.LeftShift.ToString() &&
                            button.Name != Key.RightCtrl.ToString() && button.Name != Key.LeftCtrl.ToString() &&
                            button.Name.ToString() != "ButtonStart" && button.Name.ToString() != "ButtonStop" &&
                            button.Name != Key.Space.ToString())
                        {
                            if (isSmall)
                            {
                                button.Content = button.Content.ToString().ToUpper();
                            }
                            else
                            {
                                button.Content = button.Content.ToString().ToLower();
                            }
                        }
                    }
                }
            }
        }

        private void CaseSensative_Click(object sender, RoutedEventArgs e)
        {
            GameVariables.IsCaseSensitive = CaseSensitive.IsChecked;
        }
    }
}