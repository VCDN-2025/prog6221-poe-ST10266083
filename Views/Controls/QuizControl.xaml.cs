using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Services;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Views.Controls
{
    public partial class QuizControl : UserControl
    {
        private QuizService? Quizes;
        private LogService? Logs;
        private int Indexs, Scores;

        public QuizControl()
        {
            InitializeComponent();
        }

        public void Initialize(QuizService Quiz, LogService Log)
        {
            Quizes = Quiz;
            Logs = Log;
        }

        private void Start_Click(object Sender, RoutedEventArgs e)
        {
            if (Quizes == null) return;

            Indexs = 0;
            Scores = 0;
            Logs?.Write("Quiz started");
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (Quizes == null) return;

            if (Indexs >= Quizes.Questions.Count)
            {
          
                Prompt.Text = $"Quiz complete! Score {Scores}/{Quizes.Questions.Count}";

                Feedback.Text = Scores >= Quizes.Questions.Count * 0.7
                    ? "Great job!"
                    : "Keep learning to stay safe.";

                OptionsPanel.Children.Clear();
                NextBtn.IsEnabled = false;

                Logs?.Write($"Quiz finished: {Scores}/{Quizes.Questions.Count}");
                return;
            }

            // Show the next question
            var Q = Quizes.Questions[Indexs];
            Prompt.Text = $"Q{Indexs + 1}. {Q.Prompt}";
            Feedback.Text = string.Empty;
            NextBtn.IsEnabled = false;

            OptionsPanel.Children.Clear();

            for (int i = 0; i < Q.Options.Count; i++)
            {
                var RB = new RadioButton
                {
                    Content = Q.Options[i],
                    Tag = i,
                    GroupName = "opt",
                    Margin = new Thickness(0, 5, 0, 5)
                };
                RB.Checked += Option_Checked;
                OptionsPanel.Children.Add(RB);
            }

            ScoreText.Text = $"Score: {Scores}/{Quizes.Questions.Count}";
        }

        private void Option_Checked(object Sender, RoutedEventArgs e)
        {
            if (Quizes == null) return;

            var Picked = (int)((RadioButton)Sender).Tag!;
            var Q = Quizes.Questions[Indexs];
            bool correct = Picked == Q.Index;

            // Show feedback and log
            Feedback.Text = correct
                ? "Correct! " + Q.Explanation
                : "Wrong. " + Q.Explanation;

            if (correct)
            {
                Scores++;
                Logs?.Write($"Quiz Q{Indexs + 1} correct");
            }
            else
            {
                Logs?.Write($"Quiz Q{Indexs + 1} wrong");
            }

            NextBtn.IsEnabled = true;
        }

        private void Next_Click(object Sender, RoutedEventArgs e)
        {
            Indexs++;
            ShowQuestion();
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