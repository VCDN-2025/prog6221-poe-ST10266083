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
            LogGrid.ItemsSource = Logs.Entries;
        }
    }
}
