using System;
using System.Collections.Generic;

namespace CyberSecurityChatBotGUI.Services
{
    public class NlpService
    {
        private readonly Dictionary<string, List<string>> _topicResponses;

        public NlpService()
        {
            _topicResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new List<string>
                {
                    "Use at least 12 characters mixing uppercase, lowercase, numbers, and symbols.",
                    "Never reuse passwords; use a password manager for unique credentials.",
                    "Rotate your passwords every 3 months to limit exposure risk.",
                    "Enable two-factor authentication (2FA) wherever possible."
                },
                ["scam"] = new List<string>
                {
                    "Scammers love urgency—take a breath and verify requests directly.",
                    "Hover over links before clicking; if suspicious, type the URL yourself.",
                    "Don't open attachments from unknown senders—they may carry malware.",
                    "Check for poor spelling or odd phrasing—common red flags."
                },
                ["privacy"] = new List<string>
                {
                    "Review app permissions regularly to ensure minimal data sharing.",
                    "Use a VPN on public Wi-Fi to encrypt your internet traffic.",
                    "Lock down social media settings so only trusted contacts see you.",
                    "Clear cache and cookies often to remove trackers."
                },
                ["phishing"] = new List<string>
                {
                    "Watch for misspelled sender addresses in emails that impersonate companies.",
                    "Never click links or attachments in unsolicited emails; report them.",
                    "Install anti-phishing browser extensions to flag malicious sites.",
                    "Confirm HTTPS and a padlock icon before entering credentials."
                }
            };
        }

        public IEnumerable<string> GetTips(string topic)
            => _topicResponses.TryGetValue(topic, out var t) ? t : Array.Empty<string>();

        public string PickRandomTip(string topic)
        {
            var tips = new List<string>(GetTips(topic));
            if (tips.Count == 0) return "Sorry, I don’t have any tips on that right now.";
            return tips[new Random().Next(tips.Count)];
        }

        public bool ContainsTopic(string text, out string found)
        {
            foreach (var key in _topicResponses.Keys)
                if (text.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    found = key;
                    return true;
                }
            found = null!;
            return false;
        }
    }
}
