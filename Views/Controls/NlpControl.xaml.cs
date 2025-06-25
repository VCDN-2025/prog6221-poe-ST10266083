using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Helpers;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views.Controls
{
    public partial class NlpControl : UserControl
    {
        private ChatBotProcessor? _bot;
        private LogService? _log;
        private MainWindow _host => (MainWindow)Window.GetWindow(this)!;
        private bool _askedName = false;

        public NlpControl()
        {
            InitializeComponent();
        }

        public void Initialize(ChatBotProcessor bot, LogService log, string audioFile, string asciiArt)
        {
            _bot = bot;
            _log = log;

            try
            {
                var player = new SoundPlayer(audioFile);
                player.Play();
            }
            catch { /* ignore missing file */ }

            AsciiBlock.Text = asciiArt;

            foreach (var line in _bot.GetGreeting())
                Append("Bot: " + line);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var txt = ChatInput.Text.Trim();
            if (string.IsNullOrEmpty(txt)) return;

            Append("You: " + txt);
            ChatInput.Clear();

            IEnumerable<string> responses;
            if (!_askedName)
            {
                responses = _bot!.StoreNameAndGetMenu(txt);
                _askedName = true;
            }
            else
            {
                responses = _bot!.Process(txt);
            }

            foreach (var line in responses)
            {
                if (line == "__EXIT__")
                {
                    Application.Current.Shutdown();
                    return;
                }
                else if (line == "__OPEN_TASKS__")
                {
                    _host.SwitchToTasks();
                }
                else if (line == "__OPEN_QUIZ__")
                {
                    _host.SwitchToQuiz();       // ← new
                }
                else
                {
                    Append("Bot: " + line);
                }
            }
        }

        private void Append(string message)
        {
            ChatOutput.Items.Add(new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap
            });
            ChatScroll.ScrollToEnd();
        }
    }
}
