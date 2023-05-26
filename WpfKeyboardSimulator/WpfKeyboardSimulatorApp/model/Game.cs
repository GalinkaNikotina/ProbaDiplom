using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Threading;
using WpfKeyboardSimulatorApp.repository;

namespace WpfKeyboardSimulatorApp.model
{
    public class Game : IGame, IUserOutput
    {
        private const String RepositoryErrorMessage =
            "Не удалось загрузить словарь данных для выбранного уровня сложности";

        public Level DifficultyLevel { get; set; }
        private GameStatus GameStatus { get; set; }
        public int Speed { get; set; }
        public int ErrorCount { get; private set; }
        public bool CaseSensitive { get; set; }
        public int TimeSecond { get; set; }

        private GameTimer _timer;

        private List<String> _currentText;
        public GameText UserInput { get; set; }
        private IDictionaryRepository _repository;

        public Game(Action<object, EventArgs> timerTick)
        {
            DifficultyLevel = Level.Beginner;
            GameStatus = GameStatus.Started;
            Speed = 0;
            ErrorCount = 0;
            CaseSensitive = true;
            _currentText = new List<string>();
            _repository = new DictionaryRepository();

            UserInput = new GameText();
            _timer = new GameTimer();
            _timer.InitTimer(timerTick);
        }

        public void IncreaseTimerOneSecond()
        {
            this.TimeSecond++;
        }

        public void RemoveCurrentText()
        {
            this._currentText = new List<string>();
        }

        public void UpdateCurrentText(List<string> newText)
        {
            RemoveCurrentText();
            this._currentText.AddRange(newText);
        }

        public bool InitText()
        {
            try
            {
                List<string> text = _repository.FindByLevel(this.DifficultyLevel);
                _currentText.AddRange(text);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(RepositoryErrorMessage);
                return false;
            }
        }

        public bool ChangeGameLevel(Level level)
        {
            try
            {
                List<string> text = _repository.FindByLevel(level);
                UpdateCurrentText(text);
                DifficultyLevel = level;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(RepositoryErrorMessage);
                return false;
            }
        }

        public void CalculateResult(int res)
        {
            float minimum = 0f;
            int result = 0;

            if (TimeSecond / 1000 > 60)
            {
                minimum = (float)(TimeSecond / 1000f) / 60f;
                result = (int)(res / minimum);
            }

            Speed = result;
        }

        public StringBuilder ExtractUserInput()
        {
            return UserInput.Text;
        }

        public void UpdateUserInput(StringBuilder newInput)
        {
            UserInput.Text = newInput;
        }

        public void IncrementError()
        {
            ErrorCount++;
        }

        public void StopGame()
        {
            GameStatus = GameStatus.Stopped;
        }

        public string StopGameAndShowFinishGameMessage(bool isFinish, Action action, int textBlockLength)
        {
            _timer.StopTimer();
            StringBuilder resultMessage = new StringBuilder(150);
            if (isFinish)
            {
                resultMessage.Append("Вы завершили тренинг!\r\r");
            }
            else
            {
                resultMessage.Append("Вы не завершили тренинг!\r\r");
            }

            action.Invoke();


            resultMessage.Append(
                $"Выбран уровень : {DifficultyLevel}\r Результат:\r Скорость набора: {Speed} симв/мин\r");
            resultMessage.Append($"Ошибки: {ErrorCount}\r");

            if (UserInput.Text.Length == 0)
            {
                resultMessage.Append($"Правильность набора 0 %");
            }
            else if (ErrorCount == 0 && UserInput.Text.Length != 0)
            {
                resultMessage.Append($"Правильность набора {textBlockLength * 100 / UserInput.Text.Length} %");
            }
            else
            {
                resultMessage.Append(
                    $"Правильность набора {(textBlockLength - ErrorCount) * 100 / UserInput.Text.Length} %");
            }

            return resultMessage.ToString();
        }


        public string ShowCurrentSpeed()
        {
            return new StringBuilder()
                .Append("Скорость")
                .Append(Speed)
                .Append("симв/мин")
                .ToString();
        }
    }
}