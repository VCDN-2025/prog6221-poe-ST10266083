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
            string reminderStr = ReminderDate.HasValue
                ? ReminderDate.Value.ToString("yyyy-MM-dd")
                : "No Reminder";
            string status = IsCompleted ? "✓" : "✗";
            return $"{Title} | {Description} | {reminderStr} | Completed: {status}";
        }
    }
}
