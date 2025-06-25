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
            string Rstriing = ReminderDate.HasValue
                ? ReminderDate.Value.ToString("yyyy-MM-dd")
                : "No Reminder";
            string status = IsCompleted ? "✓" : "✗";
            return $"{Title} | {Description} | {Rstriing} | Completed: {status}";

        }

        internal void Add(TaskItem Task)
        {
            throw new NotImplementedException();
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