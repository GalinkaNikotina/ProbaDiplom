using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfKeyboardSimulatorApp.model;

namespace WpfKeyboardSimulatorApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int ErrorCount { get; set; }

        private void TimerButtonUp_Tick(object sender, EventArgs e)
        {
            KeyUpButton();
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

        private void OnButtonStartClick(object sender, RoutedEventArgs e)
        {
            //Text.Clear();
            TextBlockCheck.Text = " ";
            SpeedLabel.Content = "Скорость 0 симв/мин";
            ErorCountLabel.Content = "Ошибок : 0";
            ErrorCount = 0;
            Timer.Start();
            ButtonStop.IsEnabled = true;
            ButtonStart.IsEnabled = false;

            string newTextGoal = _game.GameRestart();
            TextBlockRead.Text = newTextGoal;
        }

        private void ChangeGameDifficulty(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _game.ChangeGameLevel((Level)SliderDifficult.Value);
            DificultLabel.Content = $"Уровень : {_game.DifficultyLevel}";
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            StringBuilder userInput = _game.ExtractUserInput();
            KeyUpButton();
            KeyPress(e.Key.ToString());
            switch (e.Key)
            {
                case Key.LeftCtrl:
                {
                    if (_userInput.LetterCase == LetterCase.Upper)
                    {
                        _userInput.SetLowerCase();
                    }
                    else
                    {
                        _userInput.SetUpperCase();
                    }

                    SymbolOnOff();
                    return;
                }
                case Key.CapsLock:
                {
                    KeyBigSmall(e.IsToggled);
                    if (_userInput.LetterCase == LetterCase.Upper)
                    {
                        _userInput.SetLowerCase();
                    }
                    else
                    {
                        _userInput.SetUpperCase();
                    }

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
                        StringBuilder removeOneSymbolOldInput = userInput.Remove(userInput.Length - 1, 1);
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
                                            if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                                            {
                                                userInput.Append(button.Content.ToString().ToUpper());
                                            }
                                            else
                                            {
                                                userInput.Append(button.Content);
                                            }

                                            SpeedLabel.Content = _game.GetSpeed();
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
                                    FormattingText(_game.ExtractUserInput().Length - 1, userInput[index].ToString(),
                                        true);
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

                ErorCountLabel.Content = "Ошибок : " + _game.ErrorCount;
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
    }
}