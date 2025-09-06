using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Exam
    {
        List<Question> questions;
        string examID = Guid.NewGuid().ToString().Substring(24,4);
        public Exam(List<Question> questions)
        {
            this.questions = questions;
        }

        public string DispExam()
        {
            string dispExam = "";
            int i = 1;
            dispExam += $"Exam ID: {examID}\n\n";
            foreach (var question in questions)
            {
                if(question is MCQ)
                {
                    dispExam += "============================= Choose More Than One ==============================\n";
                    dispExam += $"{i}- ";
                    dispExam += question.DisplayQuestion();
                }
                else if(question is ChooseOne)
                {
                    dispExam += "============================= Choose one ==============================\n";
                    dispExam += $"{i}- ";
                    dispExam += question.DisplayQuestion();
                }
                else
                {
                    dispExam += "============================= True Or False ==============================\n";
                    dispExam += $"{i}- ";
                    dispExam += question.DisplayQuestion();
                }
                i++;
            }
            return dispExam;
        }

        public string GetID()
        { return examID; }

        //public bool AddQuestion(string id,Question question)
        //{
        //    if(id == examID)
        //    {
        //        this.questions.Add(question);
        //        return true;
        //    }
        //    return false;
        //}

        public void RemoveQuestion(int idx)
        {
              questions.RemoveAt(idx);
        }

        public List<Question> Enroll()
        {
            return this.questions;
        }
    }
}
