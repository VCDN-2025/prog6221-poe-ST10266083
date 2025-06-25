using System;
using System.Collections.Generic;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Services
{
    public class QuizService
    {
        private readonly List<QuizQuestion> Question = new();
        public IReadOnlyList<QuizQuestion> Questions => Question;

        public QuizService()
        {
            // MCQ
            Question.Add(new QuizQuestion(
                "What is the strongest form of user authentication?",
                new List<string> { "Single password", "Security questions", "Password + Two-Factor Authentication", "Fingerprint scan only" },
                2,
                "Two-Factor Authentication (2FA) pairs something you know with something you have making it much stronger."
            ));

            // TRUE / FALSE
            Question.Add(new QuizQuestion(
                "True or False: You should never open attachments from unknown senders.",
                new List<string> { "True", "False" },
                0,
                "True—unknown attachments often carry malware."
            ));

            // MCQ
            Question.Add(new QuizQuestion(
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

            // TRUE / FALSE
            Question.Add(new QuizQuestion(
                "True or False: Using the same password for multiple accounts is safe if it's long enough.",
                new List<string> { "True", "False" },
                1,
                "False—unique passwords per account prevent a single breach from compromising all."
            ));

            // MCQ
            Question.Add(new QuizQuestion(
                "What does VPN stand for?",
                new List<string> { "Virtual Private Network", "Verified Public Network", "Virtual Public Node", "Verified Private Node" },
                0,
                "VPN stands for Virtual Private Network, encrypting your traffic over public Wi-Fi."
            ));

            // MCQ
            Question.Add(new QuizQuestion(
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

            // TRUE / FALSE
            Question.Add(new QuizQuestion(
                "True or False: You should update your software regularly to patch vulnerabilities.",
                new List<string> { "True", "False" },
                0,
                "True—updates often include security patches that close vulnerabilities attackers exploit."
            ));

            // MCQ
            Question.Add(new QuizQuestion(
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

            // TRUE / FALSE
            Question.Add(new QuizQuestion(
                "True or False: HTTPS means the connection is encrypted.",
                new List<string> { "True", "False" },
                0,
                "True—HTTPS indicates encryption via TLS/SSL, protecting data in transit."
            ));

            // MCQ
            Question.Add(new QuizQuestion(
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
            var RQ = new Random();

            for (int i = 0; i < Question.Count; i++)
            {
                int j = RQ.Next(i, Question.Count);
                (Question[i], Question[j]) = (Question[j], Question[i]);
            }
        }
    }
}
/**************************************
       * Reference list  
       * Title : Help with my code
       * Author: ChatGPT
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://chatgpt.com/c/685c5f68-679c-8008-ba45-c7d2533a1106
**************************************/

/**************************************
       * Reference list  
       * Title : Create a simple multiple choice quiz game in Visual Studio
       * Author: Moo ICT – Project Based Tutorials
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://www.mooict.com/c-tutorial-create-a-simple-multiple-choice-quiz-game-in-visual-studio/
**************************************/