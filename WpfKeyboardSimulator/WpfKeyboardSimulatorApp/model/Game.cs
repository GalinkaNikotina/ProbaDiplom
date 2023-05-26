using System;
using System.Text;

namespace WpfKeyboardSimulatorApp.model
{
    /// <summary>
    /// Класс с основной логикой методы взаимодействия вынесены в интерфейс IGame
    /// </summary>
    public class Game : IGame, IUserOutput
    {
        private const String RepositoryErrorMessage =
            "Не удалось загрузить словарь данных для выбранного уровня сложности";

        public Level DifficultyLevel { get; private set; }
        private GameStatus _gameStatus;
        public int Speed { get; set; }
        public int ErrorCount { get; private set; }
        public bool CaseSensitive { get; set; }
        public int TimeSecond { get; set; }
        public GameText GameText { get; set; }
        private GameTimer _timer;

        public Game(Action<object, EventArgs> timerTick)
        {
            DifficultyLevel = Level.Beginner;
            _gameStatus = GameStatus.Started;
            Speed = 0;
            ErrorCount = 0;
            CaseSensitive = true;

            GameText = new GameText();
            _timer = new GameTimer();
            _timer.InitTimer(timerTick);
        }

        public void IncreaseTimerOneSecond()
        {
            TimeSecond++;
        }


        public void RemoveCurrentText()
        {
            GameText.ClearCurrentDictionary();
        }

        public void UpdateCurrentDictionary(Level level)
        {
            GameText.LoadDictionaryForLevel(level);
        }

        public bool InitText()
        {
            try
            {
                GameText.Init(DifficultyLevel);
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
                UpdateCurrentDictionary(level);
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
            return GameText.UserInput;
        }

        public void UpdateUserInput(StringBuilder newInput)
        {
            GameText.UpdateUserInput(newInput);
        }

        public void IncrementError()
        {
            ErrorCount++;
        }

        public void StopGame()
        {
            _gameStatus = GameStatus.Stopped;
        }

        public string GameRestart()
        {
            GameText.UpdateUserInput(new StringBuilder());
            _gameStatus = GameStatus.Started;
            ErrorCount = 0;
            _timer.StartTimer();
            GameText.Restart(DifficultyLevel);
            return GameText.GoalText;
        }

        public void ChangeCaseSensitive(bool isSensitive)
        {
            CaseSensitive = isSensitive;
        }

        public string StopGameAndShowFinishGameMessage(Action action, int textBlockLength)
        {
            _timer.StopTimer();
            StringBuilder resultMessage = new StringBuilder(150);
            resultMessage.Append(_gameStatus == GameStatus.Stopped
                ? "Вы завершили тренинг!\r\r"
                : "Вы не завершили тренинг!\r\r");

            action.Invoke();


            resultMessage.Append(
                $"Выбран уровень : {DifficultyLevel}\r Результат:\r Скорость набора: {Speed} симв/мин\r");
            resultMessage.Append($"Ошибки: {ErrorCount}\r");

            if (GameText.UserInput.Length == 0)
            {
                resultMessage.Append($"Правильность набора 0 %");
            }
            else if (ErrorCount == 0 && GameText.UserInput.Length != 0)
            {
                resultMessage.Append($"Правильность набора {textBlockLength * 100 / GameText.UserInput.Length} %");
            }
            else
            {
                resultMessage.Append(
                    $"Правильность набора {(textBlockLength - ErrorCount) * 100 / GameText.UserInput.Length} %");
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