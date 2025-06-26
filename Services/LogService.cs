using System.Collections.ObjectModel;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Services
{
    public class LogService
    {
        public ObservableCollection<LogEntry> Entries { get; } = new();

        public void Write(string Action) => Entries.Add(new LogEntry(Action));
    }
}
