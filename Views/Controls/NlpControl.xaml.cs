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
        private ChatBotProcessor? Bots;
        private LogService? Logs;
        private MainWindow Hosts => (MainWindow)Window.GetWindow(this)!;
        private bool GiveName = false;

        public NlpControl()
        {
            InitializeComponent();
        }

        public void Initialize(ChatBotProcessor Bot, LogService Log, string AudioFile, string AsciiArt)
        {
            Bots = Bot;
            Logs = Log;

            try
            {
                var player = new SoundPlayer(AudioFile);
                player.Play();
            }

            catch {}

            AsciiBlock.Text = AsciiArt;

            foreach (var line in Bot.GetGreeting())
                Append("Bot: " + line);
        }

        private void Send_Click(object Sender, RoutedEventArgs e)
        {
            var TXT = ChatInput.Text.Trim();

            if (string.IsNullOrEmpty(TXT)) return;

            Append("You: " + TXT);
            ChatInput.Clear();

            IEnumerable<string> responses;

            if (!GiveName)
            {
                responses = Bots!.StoreNameAndGetMenu(TXT);
                GiveName = true;
            }
            else
            {
                responses = Bots!.Process(TXT);
            }

            foreach (var Line in responses)
            {
                if (Line == "EXIT")
                {
                    Application.Current.Shutdown();
                    return;
                }
                else if (Line == "OPEN_TASKS")
                {
                    Hosts.SwitchToTasks();
                }
                else if (Line == "OPEN_QUIZ")
                {
                    Hosts.SwitchToQuiz();       
                }
                else
                {
                    Append("Bot: " +Line);
                }
            }
        }

        private void Append(string Message)
        {
            ChatOutput.Items.Add(new TextBlock
            {
                Text = Message,
                TextWrapping = TextWrapping.Wrap
            });
            ChatScroll.ScrollToEnd();
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