namespace WpfKeyboardSimulatorApp.model
{
    public class UserInput
    {
        public bool IsToggled { get; set; }
        public bool IsCaseSensitive { get; set; }
        public bool ButtonStopIsEnabled { get; set; }

        public UserInput()
        {
            IsToggled = false;
            IsCaseSensitive = true;
            ButtonStopIsEnabled = false;
        }
    }
}