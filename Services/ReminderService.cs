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

        public ReminderService(ObservableCollection<TaskItem> Tasks)
        {
            Task = Tasks;
            Timers = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            Timers.Tick += (_, __) => CheckReminders();
            Timers.Start();
        }

        private void CheckReminders()
        {
            var Now = DateTime.Now;

            foreach (var T in Task)
            {
                if (!T.IsCompleted

                    && T.ReminderDate.HasValue
                    && T.ReminderDate.Value.Date == Now.Date
                    && T.ReminderDate.Value.Hour == Now.Hour
                    && T.ReminderDate.Value.Minute == Now.Minute)

                {
                    ReminderTriggered?.Invoke(T);
                }
            }
        }
    }
}
/**************************************
       * Reference list  
       * Title : Member access operators and expressions - the dot, indexer, and invocation operators.
       * Author: microsft.learn
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators
**************************************/