using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views.Controls
{
    public partial class LogControl : UserControl
    {
        private LogService? Logs;

        public LogControl()
        {
            InitializeComponent();
        }

        public void Initialize(LogService Log)
        {
            Logs = Log;
            Logs.Entries.CollectionChanged += (_, __) => Refresh();
            Refresh();
        }

        private void Refresh()
        {
            LogList.Items.Clear();

            int Start = System.Math.Max(0, Logs!.Entries.Count - 10);

            for (int i = Start; i < Logs.Entries.Count; i++)
            {
                LogList.Items.Add(new TextBlock
                {
                    Text = Logs.Entries[i].ToString(),
                    TextWrapping = TextWrapping.Wrap
                });
            }
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
