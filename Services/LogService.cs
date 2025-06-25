using System.Collections.ObjectModel;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Services
{
    public class LogService
    {
        public ObservableCollection<LogEntry> Entries { get; } = new();
        public void Write(string action) => Entries.Add(new LogEntry(action));
    }
}
/**************************************
       * Reference list  
       * Title : Help with some of my code
       * Author: ChatGPT
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://chatgpt.com/c/685c5f68-679c-8008-ba45-c7d2533a1106
**************************************/