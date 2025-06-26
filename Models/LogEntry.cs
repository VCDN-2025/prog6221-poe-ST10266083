using System;

namespace CyberSecurityChatBotGUI.Models
{
    public class LogEntry
    {
        public string Timestamp { get; set; }

        public string Message { get; set; }

        public LogEntry(string Messages)
        {
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Message = Messages;
        }

        public override string ToString() => $"{Timestamp} — {Message}";
    }
}
/**************************************
       * Reference list  
       * Title : Help me with some of my code
       * Author: ChatGPT
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://chatgpt.com/c/6831c044-7f6c-8008-848a-25aa7e1f1cee
**************************************/
