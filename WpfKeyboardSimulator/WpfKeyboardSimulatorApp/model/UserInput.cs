namespace WpfKeyboardSimulatorApp.model
{
    /// <summary>
    /// Класс с настройками пользовательского ввода.
    /// LetterCase - верхний нижний регистр
    /// </summary>
    public class UserInput : IUserInput
    {
        public LetterCase LetterCase { get; private set; }

        public UserInput()
        {
            LetterCase = LetterCase.Lower;
        }


        public void SetUpperCase()
        {
            LetterCase = LetterCase.Upper;
        }

        public void SetLowerCase()
        {
            LetterCase = LetterCase.Lower;
        }
    }
}