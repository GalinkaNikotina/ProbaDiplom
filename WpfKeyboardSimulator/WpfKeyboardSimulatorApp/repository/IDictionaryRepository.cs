using System;
using System.Collections.Generic;
using System.Text;

namespace WpfKeyboardSimulatorApp.repository
{
    public interface IDictionaryRepository
    {
        Dictionary<Level, List<string>> FindByLevel(Level level);


    }
}
