using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Services
{
    public class ReminderService
    {
        private readonly DispatcherTimer _timer;
        private readonly ObservableCollection<TaskItem> _tasks;

        public event Action<TaskItem>? ReminderTriggered;

        public ReminderService(ObservableCollection<TaskItem> tasks)
        {
            _tasks = tasks;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            _timer.Tick += (_, __) => CheckReminders();
            _timer.Start();
        }

        private void CheckReminders()
        {
            var now = DateTime.Now;
            foreach (var t in _tasks)
            {
                if (!t.IsCompleted
                    && t.ReminderDate.HasValue
                    && t.ReminderDate.Value.Date == now.Date
                    && t.ReminderDate.Value.Hour == now.Hour
                    && t.ReminderDate.Value.Minute == now.Minute)
                {
                    ReminderTriggered?.Invoke(t);
                }
            }
        }
    }
}
