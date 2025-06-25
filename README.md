ST10266083 Cybersecurity Awareness Chatbot (WPF)

Built with WPF/XAML, it features:

- NLP Simulation - (keyword & regex handling)  
- Task Assistant (add/delete/complete tasks + reminders)  
- Cybersecurity Quiz (10 mixed T/F & MCQs, instant feedback, score)  
- Activity Log (tracks tasks, reminders, quiz, NLP actions)

Structure

CyberSecurityChatBotGUI
├─ /Helpers (Art.cs, ChatBotProcessor.cs)
├─ /Models (LogEntry.cs, QuizQuestion.cs, TaskItem.cs)
├─ /Services (LogService.cs, NlpService.cs, QuizService.cs, ReminderService.cs)
├─ /Views
│ ├─ MainWindow.xaml(.cs)
│ └─ /Controls (NlpControl, TaskControl, QuizControl, LogControl)
├─ App.xaml(.cs)
└─ README.md


Getting Started
Features & Quick Tests
Task Assistant

GUI: Fill Title/Description/Date → Add → appears in list + log.

Chat: “Add task review privacy settings” → follow-up “Yes, in 3 days” → scheduled.

Quiz

GUI or Start quiz in chat → 10 Qs (T/F & MCQ) → instant “Correct!”/“Wrong.” → final score.

NLP Flexibility

Variations like “I want to create a task …”, “Can you remind me to … in 2 days?”

“Tell me about password safety” → tip; “Tell me more” → second tip.

“I’m worried about phishing” → empathy + tip.

“I am interested in privacy” → stored preference; “What is my preference?” → tip.

Activity Log

GUI: Last 10 actions in Log tab.

Chat: “Show activity log” or “What have you done for me?” → inline summary.

🎥 Demo
(YouTube)

