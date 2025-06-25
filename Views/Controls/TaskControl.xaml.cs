using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Models;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views.Controls
{
    public partial class TaskControl : UserControl
    {
        public ObservableCollection<TaskItem> Tasks { get; } = new();
        private ReminderService? Reminders;
        private LogService? Logs;

        public TaskControl()
        {
            InitializeComponent();
            TaskList.ItemsSource = Tasks;
        }

        public void Initialize(ReminderService Reminder, LogService Log)
        {
            Reminders = Reminder;
            Logs = Log;
            Reminders.ReminderTriggered += t =>
                MessageBox.Show($"Reminder: {t.Title}", "Reminder");
        }

        private void Add_Click(object Sender, RoutedEventArgs e)
        {
            var Title = TitleBox.Text.Trim();

            if (string.IsNullOrEmpty(Title))
            {
                TitleError.Visibility = Visibility.Visible;
                return;
            }
            TitleError.Visibility = Visibility.Collapsed;

            var Description = DescBox.Text.Trim();
            var ReminderDate = DatePicker.SelectedDate;

            var Task = new TaskItem(Title, Description, ReminderDate);

            Tasks.Add(Task);
            Logs?.Write($"Added task: {Title}");

            if (ReminderDate.HasValue)
                Logs?.Write($"Reminder set for '{Title}' on {ReminderDate:yyyy-MM-dd}");

            // reset fields
            TitleBox.Clear();
            DescBox.Clear();
            DatePicker.SelectedDate = null;
        }

        private void Complete_Click(object Sender, RoutedEventArgs e)
        {
      
            var TaskItem = (TaskItem)((Button)Sender).Tag!;

          
            TaskItem.IsCompleted = true;
            TaskList.Items.Refresh();

            
            Logs?.Write($"Completed task: {TaskItem.Title}");
        }

        private void Delete_Click(object Sender, RoutedEventArgs e)
        {
            var task = (TaskItem)((Button)Sender).Tag!;
            Tasks.Remove(task);
            Logs?.Write($"Deleted task: {task.Title}");
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