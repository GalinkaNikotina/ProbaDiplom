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
    /// Класс логики взаимодействия WPF формы 
    /// </summary>
    public partial class MainWindow : Window
    {
        public Button tmp { get; set; }
        public Button tmp2 { get; set; }
       
        public DispatcherTimer TimerButton { get; set; }
        public DispatcherTimer Timer { get; set; }
        public int Speed { get; set; }
        private Game _game;
        private UserInput _userInput;
        public int TimeSecond { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            _game = new Game(Timer_Tick);
            _userInput = new UserInput();
            _game.InitText();

            KeyBigSmall(false);
            TurnOffTabIndex();

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            Timer.Tick += Timer_Tick;

            TimerButton = new DispatcherTimer();
            TimerButton.Interval = new TimeSpan(5000000);
            TimerButton.Tick += TimerButtonUp_Tick;
            TimerButton.Start();
        }

        /// <summary>
        /// обработчик секунд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            _game.IncreaseTimerOneSecond();
            TimeSecond += 1;
            CalcSymbols();
        }

        private void CalcSymbols()
        {
            SpeedLabel.Content = "Скорость " + _game.GetSpeed() + " симв / мин";
        }
        //int res = TextBlockCheck.Text.Length;
        //_game.CalculateResult(res);
        //SpeedLabel.Content = _game.ShowCurrentSpeed();
    


        public void SymbolOnOff()
        {
            if (_userInput.LetterCase == LetterCase.Upper)
            {
                Oem3.Content = ",";
                D1.Content = "!";
                D2.Content = "?";
                D3.Content = "@";
                D4.Content = "#";
                D5.Content = "$";
                D6.Content = "%";
                D7.Content = "^";
                D8.Content = "&";
                D9.Content = "*";
                D0.Content = "(";
                OemMinus.Content = ")";
                OemPlus.Content = "+";
            }
            else if (_userInput.LetterCase == LetterCase.Lower)
            {
                Oem3.Content = "ё";
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
    }
}