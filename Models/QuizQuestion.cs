using System.Collections.Generic;

namespace CyberSecurityChatBotGUI.Models
{
    public class QuizQuestion
    {
        public string Prompt { get; }

        public IReadOnlyList<string> Options { get; }

        public int Index { get; }

        public string Explanation { get; }

        public QuizQuestion(string prompt, List<string> options, int index, string explanation)
        {
            Prompt = prompt;
            Options = options;
            Index = index;
            Explanation = explanation;
        }
    }
}
/**************************************
       * Reference list  
       * Title : Help me with some of my code
       * Author: ChatGPT
       * Date 2025/05/20
       * Code version N/A
       * Available at : https://chatgpt.com/c/6831c044-7f6c-8008-848a-25aa7e1f1cee
**************************************/