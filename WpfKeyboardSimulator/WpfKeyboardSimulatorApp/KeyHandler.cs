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
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            KeyUpButton();
            KeyPress(e.Key.ToString());


            if (e.Key == Key.LeftShift)
            {
                SymbolOnOff(e.IsToggled);
                return;
            }

            if (e.Key == Key.CapsLock)
            {
                SetRegister(e.IsToggled);
                IsToggled = e.IsToggled;
                return;
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
                else if (e.Key == Key.Back)
                {
                    if (TextBlockCheck.Text.Length > 0)
                    {
                        TextBlockCheck.Text = TextBlockCheck.Text.Substring(0, TextBlockCheck.Text.Length - 1);

                        var text = Text.ToString().Substring(0, (Text.Length - 1));
                        Text.Clear();
                        Text.Append(text);
                    }
                }
                else
                {
                    if (Text.Length < TextBlockRead.Text.Length)
                    {
                        if (e.Key == Key.Space)
                        {
                            Text.Append(" ");
                        }
                        else if (e.Key.ToString() == "Tab")
                        {
                            Text.Append("\t");
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
                                            Text.Append(button.Content);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        int index = Text.Length - 1;
                        if (index >= 0 && Text.Length <= TextBlockRead.Text.Length)
                        {
                            if (GameVariables.IsCaseSensitive)
                            {
                                if (Text[index].Equals(TextBlockRead.Text[index]))
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), false);
                                }
                                else
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), true);
                                    GameVariables.ErrorCount++;
                                }
                            }
                            else
                            {
                                if (Text[index].ToString().ToLower()
                                    .Equals(TextBlockRead.Text[index].ToString().ToLower()))
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), false);
                                }
                                else
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), true);
                                    GameVariables.ErrorCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        GameVariables.IsStop = true;
                        ButtonStop_Click(sender, e);
                    }
                }

                ErorCountLabel.Content = "False : " + GameVariables.ErrorCount;
            }
        }
    }
}