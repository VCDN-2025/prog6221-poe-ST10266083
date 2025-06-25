using System.Windows;
using CyberSecurityChatBotGUI.Helpers;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views
{
    public partial class MainWindow : Window
    {
        private bool Initailized;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Home‐screen buttons:
        private void OnHome_Chatbot(object sender, RoutedEventArgs e) => ShowTabsAndSelect(0);
        private void OnHome_Tasks(object sender, RoutedEventArgs e) => ShowTabsAndSelect(1);
        private void OnHome_Quiz(object sender, RoutedEventArgs e) => ShowTabsAndSelect(2);
        private void OnHome_Log(object sender, RoutedEventArgs e) => ShowTabsAndSelect(3);

        public void SwitchToTasks() => ShowTabsAndSelect(1);
        public void SwitchToQuiz() => ShowTabsAndSelect(2);  

        private void ShowTabsAndSelect(int TabIndex)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            MainTab.SelectedIndex = TabIndex;

            if (Initailized) return;

            Initailized = true;

            var Log = new LogService();
            var NLP = new NlpService();
            var Quiz = new QuizService();
            var Reminder = new ReminderService(TaskCtrl.Tasks);
            var Bot = new ChatBotProcessor(NLP, Log, TaskCtrl.Tasks);

            NlpCtrl.Initialize(
                Bot,
                Log,
                AudioFile: "Assets/greeting.wav",
                AsciiArt: Art.AsciiArt()
            );
            TaskCtrl.Initialize(Reminder, Log);
            QuizCtrl.Initialize(Quiz, Log);
            LogCtrl.Initialize(Log);
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