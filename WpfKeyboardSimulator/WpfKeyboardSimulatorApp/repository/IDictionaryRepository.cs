using System;
using System.Collections.Generic;
using WpfKeyboardSimulatorApp.model;

namespace WpfKeyboardSimulatorApp.repository
{
    public interface IDictionaryRepository
    {
        List<String> FindByLevel(Level level);
    }

    class DictionaryRepository : IDictionaryRepository
    {
        public List<string> FindByLevel(Level level)
        {
            throw new NotImplementedException();
        }
    }
}