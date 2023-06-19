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
        //конструктор, который принимает делегат действия в качестве параметра.
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
        //Метод IncreaseTimerOneSecond увеличивает свойство TimeSecond на единицу.
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
        //Метод UpdateCurrentDictionary обновляет текущий словарь экземпляра GameText на основе параметра level.
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
        //Метод ChangeGameLevel обновляет текущий словарь экземпляра GameText и свойство DifficultyLevel на основе параметра level.
        public bool ChangeGameLevel(Level level)
        {
            UpdateCurrentDictionary(level);
            DifficultyLevel = level;
            return true;
        }
        //Метод CalculateResult вычисляет скорость пользователя на основе параметра result и времени, затраченного на выполнение задачи.
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
        //Этот метод вычисляет скорость набора текста
        public int GetSpeed()
        {
            int rightLettersCount = GameText.UserInput.Length - ErrorCount; //содержит количество правильно введенных символов. Она вычисляется как разность между длиной введенного пользователем текста и количеством ошибок.
            float currentTimeInMinutes = (TimeSecond / 1000f) / 60f; //содержит время, затраченное на набор текста в минутах. Она вычисляется как отношение времени в миллисекундах, затраченного на набор текста, к 60.
            int result = 0;
                result = (int)(rightLettersCount / currentTimeInMinutes);
            Console.WriteLine("Current time: " + currentTimeInMinutes + "current right letters count:" + rightLettersCount);
            Speed = result;
            return result;
        }
        //Метод ExtractUserInput возвращает свойство userInput экземпляра GameText в виде объекта StringBuilder.
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
        //Метод IncrementError увеличивает свойство errorCount экземпляра игры на единицу.

        public void IncrementError()
        {
            ErrorCount++;
        }
        //Метод StopGame устанавливает для свойства _gameStatus экземпляра игры значение GameStatus.
        public void StopGame()
        {
            _gameStatus = GameStatus.Stopped;
        }
        //Метод GameRestart перезапускает игру, обновляя свойство userInput
        //экземпляра GameText до пустого объекта StringBuilder,
        //устанавливая для свойства _gameStatus экземпляра игры значение GameStatus
        public string GameRestart()
        {
            GameText.UpdateUserInput(new StringBuilder());
            _gameStatus = GameStatus.Started;
            ErrorCount = 0;
            _timer.StartTimer();
            GameText.Restart(DifficultyLevel);
            return GameText.GoalText;
        }

        //Метод ChangeCaseSensitive принимает логический параметр isSensitive и обновляет свойство CaseSensitive экземпляра игры до значения параметра.

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
                $"Выбран уровень : {DifficultyLevel}\r Результат:\rСкорость набора: {Speed} знач/мин\r");
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

        //Он возвращает строку, которая представляет текущую скорость пользователя.
        public string ShowCurrentSpeed()
        {
            return new StringBuilder()
                .Append("Скорость")
                .Append(Speed)
                .Append("знач/мин")
                .ToString();
        }
    }
}