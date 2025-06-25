using System.Windows;
using CyberSecurityChatBotGUI.Helpers;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views
{
    public partial class MainWindow : Window
    {
        private bool _initialized;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Home‐screen buttons:
        private void OnHome_Chatbot(object sender, RoutedEventArgs e) => ShowTabsAndSelect(0);
        private void OnHome_Tasks(object sender, RoutedEventArgs e) => ShowTabsAndSelect(1);
        private void OnHome_Quiz(object sender, RoutedEventArgs e) => ShowTabsAndSelect(2);
        private void OnHome_Log(object sender, RoutedEventArgs e) => ShowTabsAndSelect(3);

        // Called from controls:
        public void SwitchToTasks() => ShowTabsAndSelect(1);
        public void SwitchToQuiz() => ShowTabsAndSelect(2);   // ← new

        private void ShowTabsAndSelect(int tabIndex)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            MainTab.SelectedIndex = tabIndex;

            if (_initialized) return;
            _initialized = true;

            var log = new LogService();
            var nlp = new NlpService();
            var quiz = new QuizService();
            var reminder = new ReminderService(TaskCtrl.Tasks);
            var bot = new ChatBotProcessor(nlp, log, TaskCtrl.Tasks);

            NlpCtrl.Initialize(
                bot,
                log,
                audioFile: "Assets/greeting.wav",
                asciiArt: Art.AsciiArt()
            );
            TaskCtrl.Initialize(reminder, log);
            QuizCtrl.Initialize(quiz, log);
            LogCtrl.Initialize(log);
        }
    }
}
