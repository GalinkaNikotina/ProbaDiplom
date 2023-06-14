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
        ///регистр    
        public bool CaseSensitive { get; set; } 
        public int TimeSecond { get; set; }
        /// Класс отвечающий за взаимодействие с текстом  
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

        /// <summary>
        /// чистит текст
        /// </summary>
        public void RemoveCurrentText()
        {
            GameText.ClearCurrentDictionary();
        }

        public void UpdateCurrentDictionary(Level level)
        {
            GameText.LoadDictionaryForLevel(level);
        }
        /// <summary>
        /// метод инциализирующий текст который пользователь должен ввести
        /// </summary>
        /// <returns></returns>
        public bool InitText()
        {
            GameText.Init(DifficultyLevel);
            return true;
        }

        public bool ChangeGameLevel(Level level)
        {
            UpdateCurrentDictionary(level);
            DifficultyLevel = level;
            return true;
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
        public int GetSpeed()
        {
            int rightLettersCount = GameText.UserInput.Length - ErrorCount;
            float currentTimeInMinutes = (TimeSecond / 1000f) / 60f;
            int result = 0;
                result = (int)(rightLettersCount / currentTimeInMinutes);
            Console.WriteLine("Current time: " + currentTimeInMinutes + "current right letters count:" + rightLettersCount);
            Speed = result;
            return result;
        }

        public StringBuilder ExtractUserInput()
        {
            return GameText.UserInput;
        }
        /// <summary>
        /// обновление ввода
        /// </summary>
        /// <param name="newInput"></param>
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
        /// <summary>
        /// остановка тренинга и вызов сообщения
        /// </summary>
        /// <param name="action"></param>
        /// <param name="textBlockLength"></param>
        /// <returns></returns>
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