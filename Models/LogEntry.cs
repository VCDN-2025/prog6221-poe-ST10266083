using System;

namespace CyberSecurityChatBotGUI.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }

        public LogEntry(string action)
        {
            Timestamp = DateTime.Now;
            Action = action;
        }

        public override string ToString()
        {
            return $"{Timestamp:yyyy-MM-dd HH:mm:ss} – {Action}";
        }
    }
}
