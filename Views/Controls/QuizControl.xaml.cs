using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
                Feedback.Foreground = Brushes.Black;

                OptionsPanel.Children.Clear();
                NextBtn.IsEnabled = false;

                Logs?.Write($"Quiz finished: {Scores}/{Quizes.Questions.Count}");
                return;
            }
            /**************************************
             * Reference list  
             * Title : Member access operators and expressions - the dot, indexer, and invocation operators.
             * Author: microsft.learn
             * Date 2025/06/24
             * Code version N/A
             * Available at : https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators
            **************************************/

            // Show the next question
            var Q = Quizes.Questions[Indexs];

            Prompt.Text = $"Q{Indexs + 1}. {Q.Prompt}";
            Feedback.Text = string.Empty;
            Feedback.Foreground = Brushes.Black;
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

            // Set feedback and color
            Feedback.Text = correct
                ? "Correct! " + Q.Explanation
                : "Wrong. " + Q.Explanation;

            Feedback.Foreground = correct ? Brushes.Green : Brushes.Red;

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
       * Title : Help me with some of my code
       * Author: ChatGPT
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://chatgpt.com/c/6831c044-7f6c-8008-848a-25aa7e1f1cee
**************************************/