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
            catch { }

            AsciiBlock.Text = AsciiArt;

            foreach (var line in Bot.GetGreeting())

                Append("Bot: " + line);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
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

            bool showedError = false;

            foreach (var line in responses)
            {
                if (line == "EXIT")
                {
                    Application.Current.Shutdown();
                    return;
                }
                else if (line == "OPEN_TASKS")
                {
                    Hosts.SwitchToTasks();
                }
                else if (line == "OPEN_QUIZ")
                {
                    Hosts.SwitchToQuiz();
                }
                else if (line.StartsWith("Sorry")) 
                {
                    BotError.Text = line;
                    BotError.Visibility = Visibility.Visible;
                    showedError = true;
                }
                else
                {
                    Append("Bot: " + line);
                }
            }

            if (!showedError)
                BotError.Visibility = Visibility.Collapsed;
        }

        /**************************************
       * Reference list  
       * Title : Exception-handling statements - throw, try-catch, try-finally, and try-catch-finally
       * Author: learn microsoft
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements
        **************************************/

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
       * Title : Help me with some of my code
       * Author: ChatGPT
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://chatgpt.com/c/6831c044-7f6c-8008-848a-25aa7e1f1cee
**************************************/