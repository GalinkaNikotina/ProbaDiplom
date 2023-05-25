namespace WpfKeyboardSimulatorApp.model
{
    public class GameVariables
    {
        public int Speed { get; set; }
        public int TimeSecond { get; set; }
        public Level LevelOfDifficulty { get; set; }
        public int ErrorCount { get; set; }
        public bool IsCaseSensitive { get; set; }
        public bool IsStop { get; set; }
        

        public GameVariables()
        {
            Speed = 0;
            TimeSecond = 0;
            LevelOfDifficulty = Level.Beginner;
            ErrorCount = 0;
            IsCaseSensitive = false;
            IsStop = false;
        }
    }
}