using System;
using System.Collections.Generic;
using System.Text;
using WpfKeyboardSimulatorApp.repository;

namespace WpfKeyboardSimulatorApp.model
{   
    /// <summary>
    /// Класс отвечающий за взаимодействие с текстом
    /// UserInput - текст который вводит пользователь
    /// GoalText  - рандомно выбираемый из словаря текст который пользователь должен ввести
    /// IDictionaryRepository - класс доступа доступа к словарю
    /// _dictionary - список с загруженными для конкретного уровня сложности предложениями. Возможно стоит убрать ? Нужна ли логика, что после успешного ввода, пользователю дается следующее предложение из словаря
    /// </summary>
    public class GameText : IGameText
    {
        public StringBuilder UserInput { get; private set; }
        private List<String> _dictionary;
        public String GoalText { get; private set; }
        private IDictionaryRepository _repository;

        public GameText()
        {
            UserInput = new StringBuilder(100);
            _repository = new DictionaryRepository();
        }
        /// <summary>
        /// выбирает из словаря рандомное предложение и устанавливает его целью 
        /// </summary>
        /// <param name="level"></param>
        public void Init(Level level)
        {
            List<string> textForCurrentLevel = _repository.FindByLevel(level);
            var random = new Random();
            int goalTextIndex = random.Next(0, textForCurrentLevel.Count);
            GoalText = textForCurrentLevel[goalTextIndex];
        }

        public void Restart(Level level)
        {
            Init(level);
        }

        public void ClearCurrentDictionary()
        {
            _dictionary = new List<string>();
        }

        public void LoadDictionaryForLevel(Level level)
        {
            var newDictionary = _repository.FindByLevel(level);
            _dictionary = newDictionary;
        }

        public void UpdateUserInput(StringBuilder input)
        {
            UserInput = input;
        }
    }
}