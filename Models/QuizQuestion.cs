using System.Collections.Generic;

namespace CyberSecurityChatBotGUI.Models
{
    public class QuizQuestion
    {
        public string Prompt { get; set; }
        public List<string> Options { get; set; }   
        public int CorrectIndex { get; set; }       
        public string Explanation { get; set; }

        public QuizQuestion(string prompt, List<string> options, int correctIndex, string explanation)
        {
            Prompt = prompt;
            Options = options;
            CorrectIndex = correctIndex;
            Explanation = explanation;
        }
    }
}
