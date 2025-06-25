using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CyberSecurityChatBotGUI.Services;
using CyberSecurityChatBotGUI.Models;

namespace CyberSecurityChatBotGUI.Helpers
{
    public class ChatBotProcessor
    {
        private readonly NlpService _nlp;
        private readonly LogService _log;
        private readonly ObservableCollection<TaskItem> _tasks;
        private readonly Dictionary<string, string> _data = new(StringComparer.OrdinalIgnoreCase);
        private string _finalTopic = string.Empty;

        // For chat‐driven task/reminder flow
        private string _pendingTaskTitle = null!;
        private bool _awaitingReminder = false;

        public ChatBotProcessor(NlpService nlp, LogService log, ObservableCollection<TaskItem> tasks)
        {
            _nlp = nlp;
            _log = log;
            _tasks = tasks;
        }

        // 1) Initial name prompt
        public List<string> GetGreeting()
        {
            _log.Write("Displayed name prompt");
            return new List<string> { "Please enter your name:" };
        }

        // 2) After name, show menu
        public List<string> StoreNameAndGetMenu(string rawName)
        {
            var lines = new List<string>();
            if (string.IsNullOrWhiteSpace(rawName))
            {
                lines.Add("Name cannot be empty. Please enter your name:");
                return lines;
            }

            var name = rawName.Trim();
            _data["Name"] = name;
            _log.Write($"Stored user name: {name}");

            lines.Add($"Nice to meet you, {name}! How can I assist you today?");
            lines.Add(new string('═', 60));
            lines.Add("Here are some things you can ask me:");
            lines.Add(" • How are you?");
            lines.Add(" • What’s your purpose?");
            lines.Add(" • What can I ask you about?");
            lines.Add(" • Tell me about password safety");
            lines.Add(" • Give me a phishing tip");
            lines.Add(" • Any scam emails I should watch for?");
            lines.Add(" • How do I protect my privacy?");
            lines.Add(" • I'm interested in [topic]");
            lines.Add(" • What is my preference?");
            lines.Add(" • Tell me more");
            lines.Add(" • Add task [title] (e.g. “Add task review privacy settings”)");
            lines.Add(" • Remind me to [action] in [N] days (e.g. “Remind me to update my password in 5 days”)");
            lines.Add(" • Show tasks / View my tasks");
            lines.Add(" • Start quiz");
            lines.Add(" • Show activity log / What have you done for me?");
            lines.Add(" • exit (or quit)");
            lines.Add(new string('═', 60));

            return lines;
        }

        // 3) Main processor
        public IEnumerable<string> Process(string input)
        {
            _log.Write($"User: {input}");
            if (string.IsNullOrWhiteSpace(input))
                return new[] { "I didn't catch that. Could you please say something?" };

            var trimmed = input.Trim();

            // --- TASK 1: One‐shot reminder ---
            var oneShot = Regex.Match(trimmed,
                @"\bremind me to (.+?) in (\d+) days?\b",
                RegexOptions.IgnoreCase);
            if (oneShot.Success)
            {
                var title = oneShot.Groups[1].Value.Trim();
                var days = int.Parse(oneShot.Groups[2].Value);
                var task = new TaskItem(title, description: "", reminderDate: DateTime.Now.AddDays(days));
                _tasks.Add(task);
                _log.Write($"Added & scheduled task via chat: {title} in {days} days");
                return new[]
                {
                    $"Got it—I'll remind you to '{title}' in {days} days on {task.ReminderDate:yyyy-MM-dd}."
                };
            }

            // --- TASK 1: Chat‐driven "Add task ..." ---
            var addMatch = Regex.Match(trimmed,
                @"\b(?:i want to )?(?:add|create) (?:a )?task (.+)$",
                RegexOptions.IgnoreCase);
            if (addMatch.Success)
            {
                _pendingTaskTitle = addMatch.Groups[1].Value.Trim();
                _tasks.Add(new TaskItem(_pendingTaskTitle, description: ""));
                _log.Write($"Added task via chat: {_pendingTaskTitle}");
                _awaitingReminder = true;
                return new[]
                {
                    $"Task '{_pendingTaskTitle}' added. Would you like to set a reminder? (e.g. \"Yes, in 3 days\" or \"No\")"
                };
            }

            // --- TASK 1: Reminder follow‐up ---
            if (_awaitingReminder &&
                (trimmed.StartsWith("yes", StringComparison.OrdinalIgnoreCase) ||
                 trimmed.StartsWith("no", StringComparison.OrdinalIgnoreCase)))
            {
                _awaitingReminder = false;
                if (trimmed.StartsWith("no", StringComparison.OrdinalIgnoreCase))
                {
                    _log.Write($"No reminder for '{_pendingTaskTitle}'");
                    return new[] { $"Okay—no reminder set for '{_pendingTaskTitle}'." };
                }

                var rem = Regex.Match(trimmed, @"in (\d+) days?", RegexOptions.IgnoreCase);
                if (rem.Success && int.TryParse(rem.Groups[1].Value, out var days))
                {
                    var task = _tasks.Last(t => t.Title == _pendingTaskTitle);
                    task.ReminderDate = DateTime.Now.AddDays(days);
                    _log.Write($"Reminder set for '{_pendingTaskTitle}' in {days} days");
                    return new[]
                    {
                        $"Got it—I'll remind you about '{_pendingTaskTitle}' on {task.ReminderDate:yyyy-MM-dd}."
                    };
                }

                return new[] { "Sorry, I didn't catch that timeframe. Please say \"Yes, in 3 days\" or \"No.\"" };
            }

            // --- TASK 1: Tab‐switch chat command ---
            if (Regex.IsMatch(trimmed, @"\b(?:show|view)\s+(?:my\s+)?tasks\b", RegexOptions.IgnoreCase))
            {
                _log.Write("Switching to Tasks via chat");
                return new[] { "__OPEN_TASKS__" };
            }

            // --- TASK 2: Start quiz shortcut ---
            if (trimmed.Equals("start quiz", StringComparison.OrdinalIgnoreCase))
            {
                _log.Write("Switching to Quiz via chat");
                return new[] { "__OPEN_QUIZ__" };
            }

            // --- EXIT ---
            if (trimmed.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return new[] { "__EXIT__" };

            // --- Part 1 & 2: chit-chat / tips / sentiment / NLP / preferences ---

            // How are you?
            if (trimmed.Contains("how are you", StringComparison.OrdinalIgnoreCase))
            {
                _log.Write("Answered 'how are you'");
                return new[] { "I'm doing well—thanks for asking! What can I help you with?" };
            }

            // Purpose
            if (trimmed.Contains("purpose", StringComparison.OrdinalIgnoreCase))
            {
                _log.Write("Explained purpose");
                return new[] { "I'm here to give you practical cybersecurity tips and answer your questions." };
            }

            // What can I ask
            if (trimmed.Contains("what can i ask", StringComparison.OrdinalIgnoreCase))
            {
                _log.Write("Explained available questions");
                return new[]
                {
                    "You can ask about passwords, scams, privacy, phishing, tasks, or start the quiz."
                };
            }

            // Activity log
            if (trimmed.Contains("what have you done for me", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Contains("show activity log", StringComparison.OrdinalIgnoreCase))
                return SummarizeLog(10, "Activity Log (last 10 actions):");

            // Explicit interest: "I am interested in X"
            var prefMatch = Regex.Match(trimmed,
                @"\b(?:i'?m|i am) interested in (.+)$",
                RegexOptions.IgnoreCase);
            if (prefMatch.Success)
            {
                var topic = prefMatch.Groups[1].Value.Trim();
                _data["preference"] = topic;
                _log.Write($"Stored preference: {topic}");

                // if we have tips for it
                var tips = _nlp.GetTips(topic);
                bool known = tips.Any();
                return new[]
                {
                    known
                        ? $"Got it! I'll remember your interest in {topic}."
                        : $"Understood—I'll keep an eye out for tips on {topic}."
                };
            }

            // Recall preference
            if (trimmed.Contains("what is my preference", StringComparison.OrdinalIgnoreCase))
            {
                if (_data.TryGetValue("preference", out var pref) && _nlp.GetTips(pref).Any())
                {
                    var tip = _nlp.PickRandomTip(pref);
                    _log.Write($"Tip for preference {pref}: {tip}");
                    return new[]
                    {
                        $"Since you're interested in {pref}, here's a tip:",
                        new string('═', 60),
                        tip,
                        new string('═', 60)
                    };
                }
                else
                {
                    return new[] { "You haven't told me your preference yet." };
                }
            }

            // Empathy + topic
            if ((trimmed.Contains("worried", StringComparison.OrdinalIgnoreCase) ||
                 trimmed.Contains("anxious", StringComparison.OrdinalIgnoreCase) ||
                 trimmed.Contains("frustrated", StringComparison.OrdinalIgnoreCase)) &&
                _nlp.ContainsTopic(trimmed, out var emoTopic))
            {
                _log.Write("Empathy intro");
                var t2 = _nlp.PickRandomTip(emoTopic);
                return new[]
                {
                    "I understand—it can be stressful. Here’s what I recommend:",
                    new string('═', 60),
                    t2,
                    new string('═', 60)
                };
            }

            // Keyword-based tip
            if (_nlp.ContainsTopic(trimmed, out var topicKey))
            {
                _finalTopic = topicKey;
                var tip = _nlp.PickRandomTip(topicKey);
                _log.Write($"Tip on {topicKey}: {tip}");
                return new[]
                {
                    new string('═', 60),
                    tip,
                    new string('═', 60)
                };
            }

            // Tell me more
            if (trimmed.Contains("tell me more", StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrEmpty(_finalTopic))
            {
                var moreTip = _nlp.PickRandomTip(_finalTopic);
                _log.Write($"More tip on {_finalTopic}: {moreTip}");
                return new[]
                {
                    $"Sure—here’s more on {_finalTopic}:",
                    new string('═', 60),
                    moreTip,
                    new string('═', 60)
                };
            }

            // Fallback
            _log.Write($"Unknown input: {input}");
            return new[]
            {
                "Sorry, I didn't get that. Try asking about password, scam, privacy, phishing, tasks, or quiz."
            };
        }

        private List<string> SummarizeLog(int count, string header)
        {
            var list = new List<string> { header };
            var entries = _log.Entries;
            int start = Math.Max(0, entries.Count - count);
            for (int i = start; i < entries.Count; i++)
                list.Add($"{i - start + 1}. {entries[i]}");
            return list;
        }
    }
}
