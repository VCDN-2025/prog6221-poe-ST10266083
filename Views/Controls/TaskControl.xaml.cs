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
        private ReminderService? _reminder;
        private LogService? _log;

        public TaskControl()
        {
            InitializeComponent();
            TaskList.ItemsSource = Tasks;
        }

        public void Initialize(ReminderService reminder, LogService log)
        {
            _reminder = reminder;
            _log = log;
            _reminder.ReminderTriggered += t =>
                MessageBox.Show($"Reminder: {t.Title}", "Reminder");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // validation: title required
            var title = TitleBox.Text.Trim();
            if (string.IsNullOrEmpty(title))
            {
                TitleError.Visibility = Visibility.Visible;
                return;
            }
            TitleError.Visibility = Visibility.Collapsed;

            var description = DescBox.Text.Trim();
            var reminderDate = DatePicker.SelectedDate;

            var task = new TaskItem(title, description, reminderDate);
            Tasks.Add(task);
            _log?.Write($"Added task: {title}");

            if (reminderDate.HasValue)
                _log?.Write($"Reminder set for '{title}' on {reminderDate:yyyy-MM-dd}");

            // reset fields
            TitleBox.Clear();
            DescBox.Clear();
            DatePicker.SelectedDate = null;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            var task = (TaskItem)((Button)sender).Tag!;
            task.IsCompleted = true;
            TaskList.Items.Refresh();
            _log?.Write($"Completed task: {task.Title}");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var task = (TaskItem)((Button)sender).Tag!;
            Tasks.Remove(task);
            _log?.Write($"Deleted task: {task.Title}");
        }
    }
}
