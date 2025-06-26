Cybersecurity Awareness Chatbot ST10266083

A WPF desktop chatbot that raises cybersecurity awareness through smart chat, task reminders, and a quick quiz — all tracked in an activity log.

Features

- **NLP Simulation**  
  - Keyword + regex-based recognition  
  - Supports follow-ups, preferences, and tips

- **Task Assistant**  
  - Add/delete/complete tasks (GUI + chat)  
  - Date reminders with input validation

- **Cybersecurity Quiz**  
  - 10 True/False & MCQs  
  - Instant red/green feedback + score

- **Activity Log**  
  - Tracks all actions  
  - View in GUI or ask: “Show activity log”

Project Structure

- `Helpers/` – Art, chatbot logic  
- `Models/` – Task, log, quiz models  
- `Services/` – NLP, logging, reminders, quiz  
- `Views/Controls/` – NlpControl, TaskControl, QuizControl, LogControl  
- `MainWindow.xaml(.cs)` – Main app UI  
- `App.xaml(.cs)` – App config  
- `README.md` – Project info

Quick Examples
- **Task (GUI)**: Fill Title, Desc, Date → Add → Task + log  
- **Task (Chat)**: “Add task update antivirus” → “Yes, in 2 days”  
- **Quiz**: “Start quiz” → Answer Qs → “Correct!” / “Wrong.” → Final score  
- **Log**: Check Log tab or ask “What have you done for me?”
  
Screen shots of each page:
![image](https://github.com/user-attachments/assets/ad1b0724-6306-4e48-90fa-072cec9a27c4)
![image](https://github.com/user-attachments/assets/a10ff33d-532d-4b7f-83ab-89768ddc87aa)
![image](https://github.com/user-attachments/assets/522e6e3b-d930-43f6-bd64-f48aa4ec2d27)
![image](https://github.com/user-attachments/assets/8a076b1c-b652-4007-8987-fb22849e904b)




Demo

📺 [[Watch Video](https://youtu.be/--7G9RVZE8Y)](#) 
