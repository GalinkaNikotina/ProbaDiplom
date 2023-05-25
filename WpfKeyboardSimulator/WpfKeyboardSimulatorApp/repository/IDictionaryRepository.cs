using System;
using System.Collections.Generic;
using System.Text;
using WpfKeyboardSimulatorApp.model;

namespace WpfKeyboardSimulatorApp.repository
{
    public interface IDictionaryRepository
    {
        Dictionary<Level, List<string>> FindByLevel(Level level);


    }
}
