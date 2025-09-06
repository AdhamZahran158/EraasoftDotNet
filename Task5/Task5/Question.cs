using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    abstract class Question
    {
        public Question(string difficulty, double questionMarks, string questionContent)
        {
            Difficulty = difficulty;
            QuestionMarks = questionMarks;
            QuestionContent = questionContent;
        }

        string Difficulty {  get; set; }
        public double QuestionMarks { get; protected set; }
        protected string QuestionContent { set;  get; }

        public abstract string DisplayQuestion();
        public abstract bool CheckAnswer(string ans);

    }

    class MCQ : Question
    {
        List<string> MCQs = new List<string>();
        List<int> modelAnswers = new List<int>();
        string ModelAnswer ="";
        string DispMcqs = "";
        


        public MCQ(string difficulty, double questionMarks, string questionContent, List<string> mcqs, List<int> modelAnswer) : base(difficulty,questionMarks,questionContent)
        {
            MCQs = mcqs;
            modelAnswers = modelAnswer;

            int i = 1;
            foreach (var mcq in MCQs)
            {
                DispMcqs += $"({i}) {mcq}\n";
                // Convert.ToChar( i++);
                i++;
            }

            foreach (var modAns in modelAnswers)
            {
                ModelAnswer += Convert.ToString(modAns);
                ModelAnswer += " ";
            }
        }

        public override string DisplayQuestion() { return $"{QuestionContent}   ({QuestionMarks} Marks)\n{DispMcqs}"; }
        public override bool CheckAnswer(string ans)
        {
            if (ans.Trim() == this.ModelAnswer.Trim())
            {
                return true ;
            }
            else 
                //Console.WriteLine(this.ModelAnswer);
                return false ;
        }
    }

    class ChooseOne : Question
    {
        List<string> Choices = new List<string>();
        int ModelAnswer { get; set; }
        string DispChoices = "";
        public ChooseOne(string difficulty, double questionMarks, string questionContent, List<string> choices, int modelAnswer) : base (difficulty,questionMarks,questionContent)
        {
            Choices = choices;
            ModelAnswer = modelAnswer;
            int i = 1;
            foreach (var mcq in Choices)
            {
                DispChoices += $"({i}) {mcq}\n";
                //Convert.ToChar(i++);
                i++;
            }
        }
        public override string DisplayQuestion() { return $"{QuestionContent}  ({QuestionMarks} Marks)\n{DispChoices}"; }

        public override bool CheckAnswer(string ans)
        {
            if (Convert.ToInt32(ans) == this.ModelAnswer)
                {
                return true;
                }
             else
                return false;
        }

    }

    class TrueOrFalse : Question
    {
        string Answer { get; set; }
        public TrueOrFalse(string difficulty, double questionMarks, string questionContent, string answer) : base(difficulty, questionMarks, questionContent)
        {
            Answer = answer;
        }
        public override string DisplayQuestion() { return $"{QuestionContent}  ({QuestionMarks} Marks)\n (1) T\n (2) F\n"; }

        public override bool CheckAnswer(string ans)
        {
            if (ans.ToLower() == this.Answer.ToLower())
            {
                return true;
            }
            else
                return false;
        }
    }
}
