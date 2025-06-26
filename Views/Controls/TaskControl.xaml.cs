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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var Title = TitleBox.Text.Trim();
            var Description = DescBox.Text.Trim();
            var KeepDate = DatePicker.SelectedDate;

            bool ShowsError = false;

            TitleError.Visibility = string.IsNullOrEmpty(Title) ? Visibility.Visible : Visibility.Collapsed;
            DescError.Visibility = string.IsNullOrEmpty(Description) ? Visibility.Visible : Visibility.Collapsed;
            DateError.Visibility = KeepDate == null ? Visibility.Visible : Visibility.Collapsed;

            ShowsError = string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description) || ShowsError == null;

            if (ShowsError) return;

            var Task = new TaskItem(Title, Description, KeepDate);

            Tasks.Add(Task);
            Logs?.Write($"Added task: {Title}");

            if (KeepDate.HasValue)

                Logs?.Write($"Reminder set for '{Title}' on {KeepDate:yyyy-MM-dd}");

            TitleBox.Clear();
            DescBox.Clear();
            DatePicker.SelectedDate = null;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            var TaskItem = (TaskItem)((Button)sender).Tag!;

            TaskItem.IsCompleted = true;
            TaskList.Items.Refresh();
            Logs?.Write($"Completed task: {TaskItem.Title}");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var Task = (TaskItem)((Button)sender).Tag!;
            Tasks.Remove(Task);
            Logs?.Write($"Deleted task: {Task.Title}");
        }
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