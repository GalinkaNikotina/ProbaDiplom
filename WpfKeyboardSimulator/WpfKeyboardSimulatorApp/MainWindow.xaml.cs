using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfKeyboardSimulatorApp.model;

namespace WpfKeyboardSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Symbol { get; set; }
        public StringBuilder Text { get; set; } = new StringBuilder(100);
        public Dictionary<Level, List<string>> _dictionary { get; set; }
        public Button tmp { get; set; }
        public Button tmp2 { get; set; }
        public DispatcherTimer TimerButton { get; set; }


        private Game _game;
        private UserInput _userInput;

        public MainWindow()
        {
            InitializeComponent();

            _game = new Game(Timer_Tick);
            _userInput = new UserInput();
            _game.InitText();

            KeyBigSmall(false); //? 
            TurnOffTabIndex(); // ? 

            TimerButton = new DispatcherTimer();
            TimerButton.Interval = new TimeSpan(5000000);
            TimerButton.Tick += TimerButtonUp_Tick;
            TimerButton.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            _game.IncreaseTimerOneSecond();
            CalcSymbols();
        }

        private void CalcSymbols()
        {
            int res = TextBlockCheck.Text.Length;
            _game.CalculateResult(res);
            SpeedLabel.Content = _game.ShowCurrentSpeed();
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

        private void ChangeGameDifficulty(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _game.ChangeGameLevel((Level)SliderDifficult.Value);
            DificultLabel.Content = $"Уровень : {_game.DifficultyLevel}";
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder userInput = _game.ExtractUserInput();
            KeyUpButton();
            KeyPress(e.Key.ToString());

            switch (e.Key)
            {
                case Key.LeftShift:
                {
                    Symbol = e.IsToggled;
                    SymbolOnOff();
                    return;
                }
                case Key.CapsLock:
                {
                    KeyBigSmall(e.IsToggled);
                    _userInput.IsToggled = e.IsToggled;
                    return;
                }
            }

            if (!ButtonStart.IsEnabled)
            {
                if (e.Key == Key.Enter ||
                    e.Key == Key.LWin ||
                    e.Key == Key.LeftAlt || e.Key == Key.RightAlt ||
                    e.Key == Key.RWin ||
                    e.Key == Key.RightShift || e.Key == Key.LeftShift ||
                    e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl)
                {
                    return;
                }

                if (e.Key == Key.Back)
                {
                    if (TextBlockCheck.Text.Length > 0)
                    {
                        StringBuilder oldUserInput = userInput;
                        StringBuilder removeOneSymbolOldInput = oldUserInput.Remove(oldUserInput.Length - 1, 1);
                        TextBlockCheck.Text = removeOneSymbolOldInput.ToString();
                    }
                }
                else
                {
                    if (userInput.Length < TextBlockRead.Text.Length)
                    {
                        if (e.Key == Key.Space)
                        {
                            userInput.Append(" ");
                        }
                        else if (e.Key.ToString() == "Tab")
                        {
                            userInput.Append("\t");
                        }
                        else
                        {
                            foreach (Grid item in BaseGrid.Children)
                            {
                                foreach (var element in item.Children)
                                {
                                    if (element is Button button)
                                    {
                                        if (button.Name.Equals(e.Key.ToString()))
                                        {
                                            userInput.Append(button.Content);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        int index = userInput.Length - 1;
                        if (index >= 0 && userInput.Length <= TextBlockRead.Text.Length)
                        {
                            if (_game.CaseSensitive)
                            {
                                if (userInput[index].Equals(TextBlockRead.Text[index]))
                                {
                                    FormattingText(userInput.Length - 1, userInput[index].ToString(), false);
                                }
                                else
                                {
                                    FormattingText(userInput.Length - 1, userInput[index].ToString(), true);
                                    _game.IncrementError();
                                }
                            }
                            else
                            {
                                if (userInput[index].ToString().ToLower()
                                    .Equals(TextBlockRead.Text[index].ToString().ToLower()))
                                {
                                    FormattingText(userInput.Length - 1, userInput[index].ToString(), false);
                                }
                                else
                                {
                                    FormattingText(Text.Length - 1, userInput[index].ToString(), true);
                                    _game.IncrementError();
                                }
                            }
                        }
                    }
                    else
                    {
                        _game.StopGame();
                        ButtonStop_Click(sender, e);
                    }
                }

                ErorCountLabel.Content = "False : " + _game.ErrorCount;
            }
        }

        private void FormattingText(int index, string _char, bool error)
        {
            Span result = new Span();
            Span baseText = new Span();
            Span tmp = new Span();

            var startText = new Span(new Run(TextBlockRead.Text.ToString().Substring(0, index)))
            {
                Background = Brushes.Green,
                Foreground = Brushes.White
            };

            string endOfText = TextBlockRead.Text.ToString()
                .Substring(index + 1, TextBlockRead.Text.Length - index - 1);

            var bChar = new Span(new Run(TextBlockRead.Text[index].ToString()))
            {
                Background = Brushes.Green,
                Foreground = Brushes.White
            };

            if (!error)
            {
                tmp = new Span(new Run(_char))
                {
                    Background = Brushes.Green,
                    Foreground = Brushes.White
                };
            }
            else
            {
                tmp = new Span(new Run(_char))
                {
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold
                };
            }

            baseText.Inlines.Add(startText);
            baseText.Inlines.Add(bChar);
            baseText.Inlines.Add(endOfText);

            result.Inlines.Add(tmp);

            TextBlockRead.Inlines.Clear();
            TextBlockRead.Inlines.Add(baseText);
            TextBlockCheck.Inlines.Add(result);
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

            SliderDifficult.IsTabStop = false;
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

        public void KeyBigSmall(bool isSmall)
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

        private void OnButtonStartClick(object sender, RoutedEventArgs e)
        {
            TextBlockCheck.Text = "";
            SpeedLabel.Content = "Скорость 0 симв/мин";
            ErorCountLabel.Content = "Ошибки : 0";
            ButtonStop.IsEnabled = true;
            ButtonStart.IsEnabled = false;
            string newTextGoal = _game.GameRestart();
            TextBlockRead.Text = newTextGoal;
        }

        private void ChangeCaseSensitive(object sender, RoutedEventArgs e)
        {
            _game.ChangeCaseSensitive((bool)CaseSensitive.IsChecked);
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            String resultMessage =
                _game.StopGameAndShowFinishGameMessage(CalcSymbols, TextBlockCheck.Text.Length);
            MessageBox.Show(resultMessage, this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            ButtonStop.IsEnabled = false;
            ButtonStart.IsEnabled = true;
        }
    }
}