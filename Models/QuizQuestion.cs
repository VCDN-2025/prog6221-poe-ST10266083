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
       * Title : Help with my code
       * Author: ChatGPT
       * Date 2025/06/24
       * Code version N/A
       * Available at : https://chatgpt.com/c/685c5f68-679c-8008-ba45-c7d2533a1106
**************************************/