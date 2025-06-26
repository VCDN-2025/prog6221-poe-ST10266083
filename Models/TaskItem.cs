using System;

namespace CyberSecurityChatBotGUI.Models
{
    public class TaskItem
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? ReminderDate { get; set; }

        public bool IsCompleted { get; set; }

        public TaskItem(string title, string description, DateTime? reminderDate = null)
        {
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            IsCompleted = false;
        }

        public override string ToString()

        {
            string Rstring = ReminderDate.HasValue
                ? ReminderDate.Value.ToString("yyyy-MM-dd")
                : "No Reminder";

            string Status = IsCompleted ? "✓" : "✗";
            return $"{Title} | {Description} | {Rstring} | Completed: {Status}";

        }

        /**************************************
       * Reference list  
       * Title : String interpolation using $
       * Author: microsoft.learn
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated
**************************************/

        internal void Add(TaskItem Task)
        {
            throw new NotImplementedException();
        }
    }
}
