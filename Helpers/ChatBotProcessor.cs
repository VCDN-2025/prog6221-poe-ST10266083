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
        private NlpService Context;
        private LogService Log;
        private ObservableCollection<TaskItem> Tasks;
        private Dictionary<string, string> Data = new(StringComparer.OrdinalIgnoreCase);
        private string LastTopic = string.Empty;

        // Reminder flow
        private string TaskTitle = null!;
        private bool Reminder = false;

        public ChatBotProcessor(NlpService WordLogic, LogService Logs, ObservableCollection<TaskItem> Task)
        {
            Context = WordLogic;
            Log = Logs;
            Tasks = Task;
        }

        // Name prompt
        public List<string> GetGreeting()
        {
            Log.Write("Displayed name prompt");
            return new List<string> { "Please enter your name:" };
        }

        // Show menu
        public List<string> StoreNameAndGetMenu(string FName)
        {
            var Lines = new List<string>();

            if (string.IsNullOrWhiteSpace(FName))
            {
                Lines.Add("Name cannot be empty. Please enter your name:");
                return Lines;
            }

            var Name = FName.Trim();
            Data["Name"] = Name;
            Log.Write($"Stored user name: {Name}");
            Lines.Add($"Nice to meet you, {Name}! How can I assist you today?");
            Lines.Add(new string('═', 60));
            Lines.Add("Here are some things you can ask me:");
            Lines.Add(" • How are you?");
            Lines.Add(" • What’s your purpose?");
            Lines.Add(" • What can I ask you about?");
            Lines.Add(" • Tell me about password safety");
            Lines.Add(" • Give me a phishing tip");
            Lines.Add(" • Any scam emails I should watch for?");
            Lines.Add(" • How do I protect my privacy?");
            Lines.Add(" • I'm interested in [topic]");
            Lines.Add(" • What is my preference?");
            Lines.Add(" • Tell me more");
            Lines.Add(" • Add task [title] (e.g. “Add task review privacy settings”)");
            Lines.Add(" • Remind me to [action] in [N] days (e.g. “Remind me to update my password in 5 days”)");
            Lines.Add(" • Show tasks / View my tasks");
            Lines.Add(" • Start quiz");
            Lines.Add(" • Show activity log / What have you done for me?");
            Lines.Add(" • exit (or quit)");
            Lines.Add(new string('═', 60));

            return Lines;
        }

        // Main processor
        public IEnumerable<string> Process(string Input)
        {
            Log.Write($"User: {Input}");

            if (string.IsNullOrWhiteSpace(Input))
                return new[] { "I didn't catch that. Could you please say something else?" };

            var Trimmed = Input.Trim();

            // TASK 1: One-shot reminder
            var OneShot = Regex.Match(Trimmed,
                @"\bremind me to (.+?) in (\d+) days?\b",
                RegexOptions.IgnoreCase);

            if (OneShot.Success)
            {
                var Title = OneShot.Groups[1].Value.Trim();
                var Days = int.Parse(OneShot.Groups[2].Value);
                var newTask = new TaskItem(Title, description: "", reminderDate: DateTime.Now.AddDays(Days));

                Tasks.Add(newTask);
                Log.Write($"Added and scheduled task with chat: {Title} in {Days} days");
                return new[]
                {
                    $"Got it—I'll remind you to '{Title}' in {Days} days on {newTask.ReminderDate:yyyy-MM-dd}."
                };
            }

            // TASK 1: Chat-driven "Add task"
            var Matched = Regex.Match(Trimmed,
                @"\b(?:i want to )?(?:add|create) (?:a )?task (.+)$",
                RegexOptions.IgnoreCase);

            if (Matched.Success)
            {
                TaskTitle = Matched.Groups[1].Value.Trim();
                Tasks.Add(new TaskItem(TaskTitle, description: ""));
                Log.Write($"Added task with chat: {TaskTitle}");
                Reminder = true;
                return new[]
                {
                    $"Task '{TaskTitle}' added. Would you like to set a reminder? (e.g. \"Yes, in 3 days\" or \"No\")"
                };
            }

            // TASK 1: Reminder follow-up
            if (Reminder &&
                (Trimmed.StartsWith("yes", StringComparison.OrdinalIgnoreCase) ||
                 Trimmed.StartsWith("no", StringComparison.OrdinalIgnoreCase)))
            {
                Reminder = false;

                if (Trimmed.StartsWith("no", StringComparison.OrdinalIgnoreCase))
                {
                    Log.Write($"No reminder for '{TaskTitle}'");
                    return new[] { $"Okay, no reminder set for '{TaskTitle}'." };
                }

                var Remebers = Regex.Match(Trimmed, @"in (\d+) days?", RegexOptions.IgnoreCase);

                if (Remebers.Success && int.TryParse(Remebers.Groups[1].Value, out var days))
                {
                    var t = Tasks.Last(ti => ti.Title == TaskTitle);
                    t.ReminderDate = DateTime.Now.AddDays(days);
                    Log.Write($"Reminder set for '{TaskTitle}' in {days} days");
                    return new[]
                    {
                        $"Got it, I'll remind you about '{TaskTitle}' on {t.ReminderDate:yyyy-MM-dd}."
                    };
                }

                return new[] { "Sorry, I didn't catch that timeframe. Please say \"Yes, in 3 days\" or \"No.\"" };
            }

            // TASK 1: Tab-switch chat command
            if (Regex.IsMatch(Trimmed, @"\b(?:show|view)\s+(?:my\s+)?tasks\b", RegexOptions.IgnoreCase))
            {
                Log.Write("Switching to Tasks");
                return new[] { "OPEN_TASKS" };
            }

            // TASK 2: Start quiz shortcut
            if (Trimmed.Equals("start quiz", StringComparison.OrdinalIgnoreCase))
            {
                Log.Write("Switching to Quiz");
                return new[] { "OPEN_QUIZ" };
            }

            // EXIT
            if (Trimmed.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                Trimmed.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return new[] { "EXIT" };

            // Part 1 & 2: chit-chat / tips / sentiment / preferences

            if (Trimmed.Contains("how are you", StringComparison.OrdinalIgnoreCase))
            {
                Log.Write("Answered 'how are you'");
                return new[] { "I'm doing well—thanks for asking! What can I help you with?" };
            }
            if (Trimmed.Contains("purpose", StringComparison.OrdinalIgnoreCase))
            {
                Log.Write("Explained purpose");
                return new[] { "I'm here to give you practical cybersecurity tips and answer your questions." };
            }
            if (Trimmed.Contains("what can i ask", StringComparison.OrdinalIgnoreCase))
            {
                Log.Write("Explained available questions");
                return new[] { "You can ask about passwords, scams, privacy, phishing, tasks or start the quiz." };
            }

            // Activity log
            if (Trimmed.Contains("what have you done for me", StringComparison.OrdinalIgnoreCase) ||
                Trimmed.Contains("show activity log", StringComparison.OrdinalIgnoreCase))
                return SummarizeLog(10, "Activity Log (last 10 actions):");

            // "I am interested in"
            var BeforeMatch = Regex.Match(Trimmed,
                @"\b(?:i'?m|i am) interested in (.+)$",
                RegexOptions.IgnoreCase);

            if (BeforeMatch.Success)
            {
                var Topics = BeforeMatch.Groups[1].Value.Trim();
                Data["preference"] = Topics;
                Log.Write($"Stored preference: {Topics}");
                var Tips = Context.GetTips(Topics);
                bool Known = Tips.Any();
                return new[]
                {
                    Known
                        ? $"Got it! I'll remember your interest in {Topics}."
                        : $"Understood—I'll keep an eye out for tips on {Topics}."
                };
            }

            // Recall preference
            if (Trimmed.Contains("what is my preference", StringComparison.OrdinalIgnoreCase))
            {
                if (Data.TryGetValue("preference", out var pref) && Context.GetTips(pref).Any())
                {
                    var tip = Context.PickRandomTip(pref);
                    Log.Write($"Tip for preference {pref}: {tip}");
                    return new[]
                    {
                        $"Since you're interested in {pref}, here's a tip:",
                        new string('═', 60),
                        tip,
                        new string('═', 60)
                    };
                }
                return new[] { "You haven't told me your preference yet." };
            }

            // Empathy + topic
            if ((Trimmed.Contains("worried", StringComparison.OrdinalIgnoreCase) ||
                 Trimmed.Contains("anxious", StringComparison.OrdinalIgnoreCase) ||
                 Trimmed.Contains("frustrated", StringComparison.OrdinalIgnoreCase)) &&
                Context.ContainsTopic(Trimmed, out var emoTopic))
            {
                Log.Write("Empathy intro");
                var SecondTip = Context.PickRandomTip(emoTopic);
                return new[]
                {
                    "I understand—it can be stressful. Here’s what I recommend:",
                    new string('═', 60),
                    SecondTip,
                    new string('═', 60)
                };
            }

            // Keyword-based tip
            if (Context.ContainsTopic(Trimmed, out var Key))
            {
                LastTopic = Key;
                var Tips = Context.PickRandomTip(Key);
                Log.Write($"Tip on {Key}: {Tips}");
                return new[]
                {
                    new string('═', 60),
                    Tips,
                    new string('═', 60)
                };
            }

            // Tell me more
            if (Trimmed.Contains("tell me more", StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrEmpty(LastTopic))
            {
                var ExtraTip = Context.PickRandomTip(LastTopic);
                Log.Write($"Extra tip on {LastTopic}: {ExtraTip}");
                return new[]
                {
                    $"Sure—here’s more on {LastTopic}:",
                    new string('═', 60),
                    ExtraTip,
                    new string('═', 60)
                };
            }

            // Fallback
            Log.Write($"Unknown input: {Input}");
            return new[]
            {
                "Sorry, I didn't get that. Try asking about password, scam, privacy, phishing, tasks or quiz."
            };
        }

        private List<string> SummarizeLog(int Counts, string Heading)
        {
            var List = new List<string> { Heading };
            var Entries = Log.Entries;
            int Start = Math.Max(0, Entries.Count - Counts);
            for (int i = Start; i < Entries.Count; i++)
                List.Add($"{i - Start + 1}. {Entries[i]}");
            return List;
        }
    }
}
/**************************************
       * Reference list  
       * Title : Dictionary with list of strings as value
       * Author: stackoverflow
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://stackoverflow.com/questions/17887407/dictionary-with-list-of-strings-as-value
**************************************/

/**************************************
       * Reference list  
       * Title : Exception-handling statements - throw, try-catch, try-finally, and try-catch-finally
       * Author: learn microsoft
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements
**************************************/

/**************************************
       * Reference list  
       * Title : C# Exceptions - Delegates
       * Author: geeksforgeeks
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://www.geeksforgeeks.org/c-sharp-delegates/
**************************************/

/**************************************
       * Reference list  
       * Title : Help with all code
       * Author: ChatGPT
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://chatgpt.com/c/6831c044-7f6c-8008-848a-25aa7e1f1cee
**************************************/