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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public enum Level
    {
        Beginner, Elementary, PreIntermediate, Intermediate, Advanced, Fluent
    }

    public partial class MainWindow : Window
    {
        public bool Symbol { get; set; }
        public bool IsStop { get; set; }
        public int Speed { get; set; }
        public int TimeSecond { get; set; }
        public int ErrorCount { get; set; }
        public bool? IsCaseSensative { get; set; }
        public string TextForRead { get; set; }
        public bool IsToggled { get; set; } = false;
        public StringBuilder Text { get; set; } = new StringBuilder(100);
        public List<string> MyProperty { get; set; }
        public Level levelDifficulty { get; set; }
        public Dictionary<Level, List<string>> Texts { get; set; }
        public Button tmp { get; set; }
        public Button tmp2 { get; set; }
        public DispatcherTimer Timer { get; set; }
        public DispatcherTimer TimerButton { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Texts = new Dictionary<Level, List<string>>();

            levelDifficulty = Level.Beginner;
            Texts[levelDifficulty] = new List<string>();
            Texts[levelDifficulty].Add("Картечь хватила в самую середину толпы.");
            Texts[levelDifficulty].Add("Сегодня идет сильный дождь!");
            Texts[levelDifficulty].Add("Уймись, или худо будет.");
            Texts[levelDifficulty].Add("Солнечные чудесные города.");
            Texts[levelDifficulty].Add("Однажды Петя возвращался из детского сада.");


            levelDifficulty = Level.Elementary;
            Texts[levelDifficulty] = new List<string>();
            Texts[levelDifficulty].Add("В сенях пах­ло све­жи­ми ябло­ка­ми и висе­ли вол­чьи и лисьи шкуры.");
            Texts[levelDifficulty].Add("Ни души не попа­лось на пути, ни огня за став­ня­ми.");
            Texts[levelDifficulty].Add("Казнить так казнить, жаловать так жаловать");
            Texts[levelDifficulty].Add("То ль былое вспо­ми­на­ет­ся, то ль небыв­шее зовёт?");
            Texts[levelDifficulty].Add("Тропинка обо­гну­ла куст ореш­ни­ка, и лес сра­зу раз­дал­ся в сто­ро­ны.");
            Texts[levelDifficulty].Add("Голова еще боле­ла, созна­ние же было ясное, отчёт­ли­вое.");
            levelDifficulty = Level.PreIntermediate;
            Texts[levelDifficulty] = new List<string>();
            Texts[levelDifficulty].Add("Жизнь дается один раз, и хочется прожить ее бодро, осмысленно, красиво.");
            Texts[levelDifficulty].Add("Он охотно давал их читать, никогда не требуя их назад; зато никогда не возвращал хозяину книги, им занятой.");
            Texts[levelDifficulty].Add("Канонада ста­ла сла­бее, одна­ко трес­кот­ня ружей сза­ди и спра­ва слы­ша­лась все чаще и чаще.");
            Texts[levelDifficulty].Add("То степь откры­ва­лась далё­кая и мол­ча­ли­вая, то низ­кие, подер­ну­тые кро­вью тучи, " +
                "а то и люди, и паро­вик, и моло­тил­ка разом тону­ли в чер­ном мра­ке.");
            Texts[levelDifficulty].Add("Время стояло самое благоприятное, то есть было темно, слегка морозно и совершенно тихо.");
            Texts[levelDifficulty].Add("До этой поры он не жил, а лишь существовал, правда очень недурно, но все же возлагая все надежды на будущее.");

            levelDifficulty = Level.Intermediate;
            Texts[levelDifficulty] = new List<string>();
            Texts[levelDifficulty].Add("Светлые, прозрачно набегающие морщины моют золотой песок, да чуть приметно " +
                "шевелятся тёмные листья задумчиво свесившихся над обрывом с размытыми весеннею водою корнями деревьев.");
            Texts[levelDifficulty].Add("Милостивое писание ваше я получил, в котором изволишь гневаться на меня, раба вашего, что-де стыдно мне не исполнять господских приказаний; а я, не старый пес, а верный ваш слуга, господских " +
                "приказаний слушаюсь и усердно вам всегда служил и дожил до седых волос");
            Texts[levelDifficulty].Add("Я хотел уже выйти из дому, как дверь моя отворилась и ко мне явился капрал с донесением, " +
                "что наши казаки ночью выступили из крепости, взяв насильно с собою Юлая, и что около крепости разъезжают неведомые люди.");
            Texts[levelDifficulty].Add("Туман лежал белой колыхающейся гладью у его ног, но над ним сияло голубое небо, " +
                "и шептались душистые зеленые ветви, а золотые лучи солнца звенели ликующим торжеством победы.");
            Texts[levelDifficulty].Add("Ней, шедший последним (потому что, несмотря на несчастное их положение или именно вследствие его, им хотелось побить тот пол, который ушиб их, он занялся взрыванием никому не мешавших стен Смоленска), — " +
                "шедший последним, Ней, с своим десятитысячным корпусом, прибежал в Оршу к " +
                "Наполеону только с тысячью человеками, побросав и всех людей, и все пушки и ночью, украдучись, пробравшись лесом через Днепр.");


            levelDifficulty = Level.Advanced;
            Texts[levelDifficulty] = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string text = RandomText(50);
                Texts[levelDifficulty].Add(text);
            }


            levelDifficulty = Level.Fluent;
            Texts[levelDifficulty] = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string text = RandomText(50);
                Texts[levelDifficulty].Add(text);
            }

            ErrorCount = 0;
            levelDifficulty = 0;
            ButtonStop.IsEnabled = false;
            IsStop = false;
            IsCaseSensative = true;

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

        private void TimerButtonUp_Tick(object sender, EventArgs e)
        {
            KeyUpButton();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSecond += 1;
            CalcSymbols();
        }
        private void CalcSymbols()
        {
            float min = 0;

            int res = TextBlockCheck.Text.Length;

            if (TimeSecond / 1000 > 60)
            {

                min = (float)(TimeSecond / 1000f) / 60f;
                res = (int)(res / min);
            }
            Speed = res;
            SpeedLabel.Content = "Скорость " + res.ToString() + "симв/мин";
        }
        public string RandomText(int count)
        {
            Random random = new Random();
            string randText = "1234567890йцукенгшщзхъэждлорпавыфячсмитьбю.,./ЯЧСМИТЬБЮ.ЙЦУКЕНГШЩЗХЪЭЖДЛОРПАВЫФ";
            StringBuilder stringBuilder = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                if (i % 4 == 0 || i % 6 == 0)  stringBuilder.Append(" ");
                
                stringBuilder.Append(randText[random.Next(0, randText.Length)]);
            }
            string newText = stringBuilder.ToString();
            return newText;
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

            msg.Append($"Выбран уровень : {levelDifficulty}\r Результат:\r Скорость набора: {Speed} симв/мин\r");
            msg.Append($"Ошибки: {ErrorCount}\r");

            if (Text.Length == 0)
            {
                msg.Append($"Правильность набора 0 %");
            }
            else if (ErrorCount == 0 && Text.Length != 0)
            {
                msg.Append($"Правильность набора { TextBlockCheck.Text.Length * 100 / Text.Length } %");
            }
            else
            {
                msg.Append($"Правильность набора { (TextBlockCheck.Text.Length - ErrorCount) * 100 / Text.Length } %");
            }


            MessageBox.Show(msg.ToString(), this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SliderDificult_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            levelDifficulty = (Level)SliderDificult.Value;
            DificultLabel.Content = "Уровень : " + levelDifficulty.ToString();
        }
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            KeyUpButton();
            KeyPress(e.Key.ToString());
            

            if (e.Key == Key.LeftShift)
            {
                Symbol = e.IsToggled;
                SymbolOnOff();
                return;
            }            

            if (e.Key == Key.CapsLock)
            {
                KeyBigSmall(e.IsToggled);
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
                            if (IsCaseSensative.Value)
                            {
                                if (Text[index].Equals(TextBlockRead.Text[index]))
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), false);

                                }
                                else
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), true);
                                    ErrorCount++;
                                }
                            }
                            else
                            {
                                if (Text[index].ToString().ToLower().Equals(TextBlockRead.Text[index].ToString().ToLower()))
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), false);
                                }
                                else
                                {
                                    FormatingText(Text.Length - 1, Text[index].ToString(), true);
                                    ErrorCount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        IsStop = true;
                        ButtonStop_Click(sender, e);
                    }
                }
                ErorCountLabel.Content = "False : " + ErrorCount;
            }
        }
        private void FormatingText(int index, string _char, bool error)
        {
            Span result = new Span();
            Span baseText = new Span();
            Span tmp = new Span();

            var startText = new Span(new Run(TextBlockRead.Text.ToString().Substring(0, index)))
            {
                Background = Brushes.Green,
                Foreground = Brushes.White
            };

            string endOfText = TextBlockRead.Text.ToString().Substring(index + 1, TextBlockRead.Text.Length - index - 1);

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
            SliderDificult.IsTabStop = false;
            CaseSensative.IsTabStop = false;
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
        private void CaseSensative_Click(object sender, RoutedEventArgs e)
        {
            IsCaseSensative = CaseSensative.IsChecked;
        }
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            Finish(IsStop);
            ButtonStop.IsEnabled = false;
            ButtonStart.IsEnabled = true;
            Timer.Stop();
        }
    }
}
