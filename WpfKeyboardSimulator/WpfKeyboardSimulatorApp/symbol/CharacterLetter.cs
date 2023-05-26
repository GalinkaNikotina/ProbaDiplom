using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfKeyboardSimulatorApp.symbol
{
    public sealed partial class MainWindow: Window
    {
        public void KeyBigSmall(bool isSmall)
        {
            foreach (Grid item in BaseGrid.Children)
            {
                foreach (var element in item.Children)
                {
                    if (element is Button button)
                    {
                        if (button.Name != Key.CapsLock.ToString() && button.Name != System.Windows.Input.Key.Return.ToString() &&
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
