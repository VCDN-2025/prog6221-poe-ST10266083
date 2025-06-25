using System;

namespace CyberSecurityChatBotGUI.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }

        public string Action { get; set; }

        public LogEntry(string Actions)
        {
            Timestamp = DateTime.Now;
            Action = Actions;
        }

        public override string ToString()
        {
            return $"{Timestamp:yyyy-MM-dd HH:mm:ss} – {Action}";
        }
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