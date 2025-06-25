using System;
using System.Collections.Generic;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Services
{
    public class QuizService
    {
        private readonly List<QuizQuestion> _questions = new();
        public IReadOnlyList<QuizQuestion> Questions => _questions;

        public QuizService()
        {
            // 1) MCQ
            _questions.Add(new QuizQuestion(
                "What is the strongest form of user authentication?",
                new List<string> { "Single password", "Security questions", "Password + Two-Factor Authentication", "Fingerprint scan only" },
                2,
                "Two-Factor Authentication (2FA) pairs something you know with something you have, making it much stronger."
            ));

            // 2) TRUE / FALSE
            _questions.Add(new QuizQuestion(
                "True or False: You should never open attachments from unknown senders.",
                new List<string> { "True", "False" },
                0,
                "True—unknown attachments often carry malware."
            ));

            // 3) MCQ
            _questions.Add(new QuizQuestion(
                "Which of these is a sign of a phishing email?",
                new List<string>
                {
                    "An email from a known address asking you to login via an unfamiliar link",
                    "An email newsletter you subscribed to",
                    "A billing statement from your bank's official domain",
                    "A calendar invite from a coworker you recognize"
                },
                0,
                "Phishers often send urgent messages with suspicious links from spoofed addresses."
            ));

            // 4) TRUE / FALSE
            _questions.Add(new QuizQuestion(
                "True or False: Using the same password for multiple accounts is safe if it's long enough.",
                new List<string> { "True", "False" },
                1,
                "False—unique passwords per account prevent a single breach from compromising all."
            ));

            // 5) MCQ
            _questions.Add(new QuizQuestion(
                "What does VPN stand for?",
                new List<string> { "Virtual Private Network", "Verified Public Network", "Virtual Public Node", "Verified Private Node" },
                0,
                "VPN stands for Virtual Private Network, encrypting your traffic over public Wi-Fi."
            ));

            // 6) MCQ
            _questions.Add(new QuizQuestion(
                "Which practice helps protect your privacy on social media?",
                new List<string>
                {
                    "Accepting all friend requests",
                    "Posting only text (no images)",
                    "Leaving your profile public",
                    "Locking down your privacy settings to trusted contacts"
                },
                3,
                "By restricting who can see your posts to trusted contacts, you limit data exposure."
            ));

            // 7) TRUE / FALSE
            _questions.Add(new QuizQuestion(
                "True or False: You should update your software regularly to patch vulnerabilities.",
                new List<string> { "True", "False" },
                0,
                "True—updates often include security patches that close vulnerabilities attackers exploit."
            ));

            // 8) MCQ
            _questions.Add(new QuizQuestion(
                "What should you do before clicking on a link in an email?",
                new List<string>
                {
                    "Hover over it to inspect the URL",
                    "Click immediately to see where it goes",
                    "Forward the email to your contacts",
                    "Delete the entire email without reading"
                },
                0,
                "Hovering over the link shows the real destination—if it’s suspicious, don’t click."
            ));

            // 9) TRUE / FALSE
            _questions.Add(new QuizQuestion(
                "True or False: HTTPS means the connection is encrypted.",
                new List<string> { "True", "False" },
                0,
                "True—HTTPS indicates encryption via TLS/SSL, protecting data in transit."
            ));

            // 10) MCQ
            _questions.Add(new QuizQuestion(
                "Which of these should you use to manage many strong, unique passwords?",
                new List<string>
                {
                    "A sticky-note on your monitor",
                    "Reusing one password everywhere",
                    "A reputable password manager",
                    "Your browser’s autocomplete only"
                },
                2,
                "A password manager generates, stores, and auto-fills unique passwords securely for you."
            ));

            // Shuffle questions so each quiz run is different
            var rnd = new Random();
            for (int i = 0; i < _questions.Count; i++)
            {
                int j = rnd.Next(i, _questions.Count);
                (_questions[i], _questions[j]) = (_questions[j], _questions[i]);
            }
        }
    }
}
