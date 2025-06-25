using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views.Controls
{
    public partial class QuizControl : UserControl
    {
        private QuizService? _quiz;
        private LogService? _log;
        private int _idx, _score;

        public QuizControl()
        {
            InitializeComponent();
        }

        public void Initialize(QuizService quiz, LogService log)
        {
            _quiz = quiz;
            _log = log;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _idx = 0;
            _score = 0;
            _log?.Write("Quiz started");
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (_idx >= _quiz!.Questions.Count)
            {
                Prompt.Text = $"Quiz complete! Score {_score}/{_quiz.Questions.Count}";
                Feedback.Text = _score >= _quiz.Questions.Count * 0.7
                    ? "Great job!"
                    : "Keep learning to stay safe.";
                OptionsPanel.Children.Clear();
                NextBtn.IsEnabled = false;
                _log?.Write($"Quiz finished: {_score}/{_quiz.Questions.Count}");
                return;
            }

            var q = _quiz.Questions[_idx];
            Prompt.Text = $"Q{_idx + 1}. {q.Prompt}";
            Feedback.Text = "";
            NextBtn.IsEnabled = false;
            OptionsPanel.Children.Clear();

            for (int i = 0; i < q.Options.Count; i++)
            {
                var rb = new RadioButton
                {
                    Content = q.Options[i],
                    Tag = i,
                    GroupName = "opt",
                    Margin = new Thickness(0, 5, 0, 5)
                };
                rb.Checked += Option_Checked;
                OptionsPanel.Children.Add(rb);
            }

            ScoreText.Text = $"Score: {_score}/{_quiz.Questions.Count}";
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            var picked = (int)((RadioButton)sender).Tag!;
            var q = _quiz!.Questions[_idx];
            bool ok = picked == q.CorrectIndex;

            Feedback.Text = ok
                ? "Correct! " + q.Explanation
                : "Wrong. " + q.Explanation;
            if (ok) { _score++; _log?.Write($"Quiz Q{_idx + 1} correct"); }
            else { _log?.Write($"Quiz Q{_idx + 1} wrong"); }

            NextBtn.IsEnabled = true;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _idx++;
            ShowQuestion();
        }
    }
}
