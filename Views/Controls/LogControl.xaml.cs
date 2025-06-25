using System.Windows;
using System.Windows.Controls;
using CyberSecurityChatBotGUI.Services;

namespace CyberSecurityChatBotGUI.Views.Controls
{
    public partial class LogControl : UserControl
    {
        private LogService? _log;

        public LogControl()
        {
            InitializeComponent();
        }

        public void Initialize(LogService log)
        {
            _log = log;
            _log.Entries.CollectionChanged += (_, __) => Refresh();
            Refresh();
        }

        private void Refresh()
        {
            LogList.Items.Clear();
            int start = System.Math.Max(0, _log!.Entries.Count - 10);
            for (int i = start; i < _log.Entries.Count; i++)
            {
                LogList.Items.Add(new TextBlock
                {
                    Text = _log.Entries[i].ToString(),
                    TextWrapping = TextWrapping.Wrap
                });
            }
        }
    }
}
