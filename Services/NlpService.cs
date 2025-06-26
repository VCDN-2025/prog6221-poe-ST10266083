using System;
using System.Collections.Generic;

namespace CyberSecurityChatBotGUI.Services
{
    public class NlpService
    {
        private readonly Dictionary<string, List<string>> Responses;

        public NlpService()
        {
            Responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new List<string>
                {
                    "Use at least 12 characters mixing uppercase, lowercase, numbers and symbols.",
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

        /**************************************
       * Reference list  
       * Title : Dictionary with list of strings as value
       * Author: stackoverflow
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://stackoverflow.com/questions/17887407/dictionary-with-list-of-strings-as-value
        **************************************/


        public IEnumerable<string> GetTips(string Topic)
            => Responses.TryGetValue(Topic, out var t) ? t : Array.Empty<string>();

        public string PickRandomTip(string Topic)
        {
            var Tips = new List<string>(GetTips(Topic));

            if (Tips.Count == 0) return "Sorry, I don’t have any tips on that right now.";

            return Tips[new Random().Next(Tips.Count)];
        }

        public bool ContainsTopic(string Text, out string Found)
        {
            foreach (var key in Responses.Keys)

                if (Text.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Found = key;
                    return true;
                }

            Found = null!;
            return false;
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