using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Services
{
    public class ReminderService
    {
        private readonly DispatcherTimer Timers;
        private readonly ObservableCollection<TaskItem> Task;

        public event Action<TaskItem>? ReminderTriggered;

        public ReminderService(ObservableCollection<TaskItem> tasks)
        {
            Task = tasks;
            Timers = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            Timers.Tick += (_, __) => CheckReminders();
            Timers.Start();
        }

        private void CheckReminders()
        {
            var Now = DateTime.Now;

            foreach (var t in Task)
            {
                if (!t.IsCompleted
                    && t.ReminderDate.HasValue
                    && t.ReminderDate.Value.Date == Now.Date
                    && t.ReminderDate.Value.Hour == Now.Hour
                    && t.ReminderDate.Value.Minute == Now.Minute)
                {
                    ReminderTriggered?.Invoke(t);
                }
            }
        }
    }
}
/**************************************
       * Reference list  
       * Title : Help with my code
       * Author: ChatGPT
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://chatgpt.com/c/685c5f68-679c-8008-ba45-c7d2533a1106
**************************************/