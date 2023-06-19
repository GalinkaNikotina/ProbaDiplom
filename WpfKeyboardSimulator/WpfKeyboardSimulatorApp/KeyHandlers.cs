using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
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
        //Этот метод представляет собой обработчик событий, который запускается при установке или снятии флажка.
        //Метод вызывает метод ChangeCaseSensitive объекта _game, передавая логическое значение, которое указывает, установлен или не установлен флажок
        private void ChangeCaseSensitive(object sender, RoutedEventArgs e)
        {
            _game.ChangeCaseSensitive((bool)CaseSensitive.IsChecked);
        }
        //Этот метод является обработчиком событий, который запускается при срабатывании таймера.Метод вызывает метод KeyUpButton.
        //Этот метод представляет собой обработчик событий, который запускается при нажатии кнопки. Метод вызывает
        //метод StopGameAndShowFinishGameMessage объекта _game, передавая два аргумента: CalcSymbols и длину текста в объекте TextBlockCheck.
        //Затем метод отображает окно сообщения с сообщением о результате, возвращаемым объектом _game. Наконец, метод отключает
        //кнопку ButtonStop и включает кнопку ButtonStart.
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            String resultMessage =
                _game.StopGameAndShowFinishGameMessage(CalcSymbols, TextBlockCheck.Text.Length);
            MessageBox.Show(resultMessage, this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            ButtonStop.IsEnabled = false;
            ButtonStart.IsEnabled = true;
        }
        //Этот метод представляет собой обработчик событий, который запускается при нажатии кнопки.
        private void OnButtonStartClick(object sender, RoutedEventArgs e)
        {
            
            TextBlockCheck.Text = " ";
            SpeedLabel.Content = "Скорость 0 знач/мин";
            ErorCountLabel.Content = "Ошибок : 0";
            ErrorCount = 0;
            Timer.Start();
            ButtonStop.IsEnabled = true;
            ButtonStart.IsEnabled = false;

            string newTextGoal = _game.GameRestart();
            TextBlockRead.Text = newTextGoal;
        }
        //Этот метод представляет собой обработчик событий, который запускается при изменении значения ползунка.
        private void ChangeGameDifficulty(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _game.ChangeGameLevel((Level)SliderDifficult.Value);
            DificultLabel.Content = $"Уровень : {_game.DifficultyLevel}";
        }
        //Этот метод вызывается при нажатии клавиши. Метод перебирает дочерние
        //элементы объекта Grid и проверяет, являются ли какие-либо из них
        //объектами Button со свойством Name, соответствующим аргументу keyName. 
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
        //Этот метод перебирает дочерние элементы объекта Grid и устанавливает
        //для свойства Margin любых объектов Button значение нового объекта Thickness со значением 0.
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
        //Этот метод представляет собой обработчик событий, который запускается при нажатии клавиши. 
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
        //этот фрагмент кода определяет методы, которые обрабатывают нажатия клавиш и Caps Lock
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