namespace Task5
{
    internal class Program
    {
        static void AddQuestion(List<Question> questions)
        {
            while (true)
            {
                string difficulty = "";
                double questionMarks;
                int QuestionType = 0;
                try
                {
                    Console.WriteLine("Choose the type of the question to add: \n1- Multible choices (more than one correct) \n2- Multible choices (Only one correct) \n3- True or False \n4- Back");
                    Console.Write("-->");
                    QuestionType = Convert.ToInt32(Console.ReadLine());
                    if (QuestionType == 4)
                    {
                        break;
                    }
                DiffChoice:
                    try
                    {
                        Console.WriteLine("Choose the Difficulty:\n1-Easy \n2- Medium \n3- Hard");
                        Console.Write("-->");
                        int difficultyChoice = Convert.ToInt32(Console.ReadLine());
                        switch (difficultyChoice)
                        {
                            case 1:
                                {
                                    difficulty = "Easy";
                                    break;
                                }
                            case 2:
                                {
                                    difficulty = "Medium";
                                    break;
                                }
                            case 3:
                                {
                                    difficulty = "Hard";
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Invalid Choice");
                                    goto DiffChoice;
                                }
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Choice");
                        goto DiffChoice;
                    }
                questionMarks:
                    try
                    {
                        Console.WriteLine("Enter the Question marks");
                        Console.Write("-->");
                        questionMarks = Convert.ToDouble(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Entery");
                        goto questionMarks;
                    }

                    switch (QuestionType)
                    {
                        case 1:
                            {
                                Console.WriteLine("Enter Question Content");
                                Console.Write("-->");
                                string content = Console.ReadLine();
                                List<string> mcqs = new List<string>();
                                string mcq;
                                for (int i = 1; ; i++)
                                {
                                    Console.WriteLine($"Enter mcq number {i} (Write Q to quit)");
                                    Console.Write("-->");
                                    mcq = Console.ReadLine();
                                    if (mcq.ToLower() == "q")
                                        break;
                                    if (mcq != null)
                                    {
                                        mcqs.Add(mcq);
                                    }
                                    else
                                    { Console.WriteLine("MCQ Can not empty"); }
                                }
                                Console.WriteLine("MCQs:");
                                int j = 1;
                                foreach (var mc in mcqs)
                                {
                                    Console.WriteLine($"{j}-  {mc}");
                                    j++;
                                }

                                List<int> modelAns = new List<int>();
                            modelAnswers:
                                Console.WriteLine("Enter your Model Answers With choice number one after another (Write Q when done)");
                                try
                                {
                                    while (true)
                                    {
                                        Console.Write("-->");
                                        string ma = Console.ReadLine();
                                        if (ma.ToLower() == "q")
                                            break;
                                        modelAns.Add(Convert.ToInt32(ma));
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("you must enter question number");
                                    goto modelAnswers;
                                }
                                questions.Add(new MCQ(difficulty, questionMarks, content, mcqs, modelAns));
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter Question Content");
                                string content = Console.ReadLine();
                                List<string> mcqs = new List<string>();
                                string mcq;
                                for (int i = 1; ; i++)
                                {
                                    Console.WriteLine($"Enter mcq number {i} (Write Q to quit)");
                                    mcq = Console.ReadLine();
                                    if (mcq.ToLower() == "q")
                                        break;
                                    if (mcq != null)
                                    {
                                        mcqs.Add(mcq);
                                    }
                                    else
                                    { Console.WriteLine("MCQ Can not empty"); }
                                }
                                Console.WriteLine("MCQs:");
                                int j = 1;
                                foreach (var mc in mcqs)
                                {
                                    Console.WriteLine($"{j}- {mc}");
                                    j++;
                                }

                                Console.WriteLine("Enter your model answer number according to the mcqs");
                            modAns:
                                int modelAns;
                                try
                                {
                                    Console.Write("Model Answer Number --->");
                                    modelAns = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid entery");
                                    goto modAns;
                                }
                                questions.Add(new ChooseOne(difficulty, questionMarks, content, mcqs, modelAns));

                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Enter Question Content");
                                Console.Write("-->");
                                string content = Console.ReadLine();
                                Console.WriteLine("Enter the model Answer whether True or False (Enter T or F)");
                                Console.Write("-->");
                                string modAns = Console.ReadLine();
                                questions.Add(new TrueOrFalse(difficulty, questionMarks, content, modAns));
                                break;
                            }
                        case 4:
                            {
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid choice");
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Choice");
                }
                
            }
        }


        static void Main(string[] args)
        {
            List<Exam> exams = new List<Exam>();
            List<double> studentMarks = new List<double>();

            while (true)
            {
                Console.WriteLine("Choose your identity:\n1- Student\n2- Doctor \n3- Exit ");
                try
                {
                    Console.Write("-->");
                    int identity = Convert.ToInt32(Console.ReadLine());
                    if (identity == 3)
                        break;
                    switch (identity)
                    {
                        case 2:
                            {
                                int DoctorAction = 0;
                                while (true)
                                {
                                    Console.WriteLine("Choose your action: \n1- Add Exam \n2- Show Exams \n3- Edit Exam \n4- Back");
                                    try
                                    {
                                        Console.Write("-->");
                                        DoctorAction = Convert.ToInt32(Console.ReadLine());
                                        switch (DoctorAction)
                                        {
                                            case 1:
                                                {
                                                    List<Question> questions = new List<Question>();
                                                    AddQuestion(questions);
                                                    if (questions.Count != 0)
                                                      exams.Add(new Exam(questions));
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    if(exams.Count != 0)
                                                    {
                                                        foreach (var ex in exams)
                                                        {
                                                            Console.WriteLine(ex.DispExam());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("No Exams added yet");
                                                    }
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    while(true)
                                                    {
                                                        try
                                                        {
                                                            Console.WriteLine("Choose The Action:  (note: you must know the exam ID)\n1- Add Questions \n2- Remove Question \n3- Back");
                                                            int action = 0;
                                                            Console.Write("-->");
                                                            action = Convert.ToInt32(Console.ReadLine());
                                                            if (action == 3)
                                                                break;
                                                            Console.WriteLine("Enter Exam ID");
                                                            Console.Write("ID --->");
                                                            string exID = Console.ReadLine();
                                                            switch (action)
                                                            {
                                                                case 1:
                                                                    {
                                                                        bool added = false;
                                                                        foreach (var ex in exams)
                                                                        {
                                                                            if(exID == ex.GetID())
                                                                            {
                                                                                AddQuestion(ex.Enroll());
                                                                                added = true;
                                                                            }
                                                                        }
                                                                        if (added)
                                                                            Console.WriteLine("\nQuestions Added Successfully");
                                                                        else
                                                                            Console.WriteLine("\nFailed to Add wrong ID");
                                                                        break;
                                                                    }
                                                                case 2:
                                                                    {
                                                                        Console.WriteLine("Enter the question number you want to remove");
                                                                        Console.Write("-->");
                                                                        int quesNum = Convert.ToInt32(Console.ReadLine());
                                                                        bool removed = false;
                                                                        foreach (var ex in exams)
                                                                        {
                                                                            if(ex.GetID() == exID)
                                                                            {
                                                                                ex.RemoveQuestion( quesNum - 1);
                                                                                removed = true;
                                                                                break;
                                                                            }
                                                                        }
                                                                        if(removed)
                                                                           Console.WriteLine($"Removed Question {quesNum} Successfully");
                                                                        else
                                                                           Console.WriteLine($"Couldn't remove question {quesNum} must be wrong ID");
                                                                        break;
                                                                    }
                                                                default:
                                                                    {
                                                                        Console.WriteLine("Invalid Choice");
                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                        catch(FormatException)
                                                        {
                                                            Console.WriteLine("Invalid Choice");
                                                        }
                                                       
                                                    }
                                                    break;
                                                }

                                            case 4:
                                                {
                                                    break ;
                                                }
                                            default:
                                                {
                                                    Console.WriteLine("Invalid Choice");
                                                    break ;
                                                }
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Invalid Choice");
                                    }

                                    if(DoctorAction == 4)
                                        break ;

                                }
                                break;
                            }
                        case 1:
                            {
                                
                                while (true)
                                {
                                Console.WriteLine("Select the Action: \n1- Show Available Exams \n2- Enroll an Exam (You must know the exam ID) \n3- Show the marks of the previous enrolled exams \n4- Back");
                                    try
                                    {
                                        Console.Write("-->");
                                        int StudentAction = Convert.ToInt32(Console.ReadLine());
                                        if (StudentAction == 4)
                                            break;
                                        switch (StudentAction)
                                        {
                                            case 1:
                                                {
                                                    if (exams.Count != 0)
                                                    {
                                                        foreach (var ex in exams)
                                                        {
                                                            Console.WriteLine(ex.DispExam());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("No Available exams");
                                                    }
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    Console.WriteLine("Enter Exam ID");
                                                    Console.Write("-->");
                                                    string exID = Console.ReadLine();
                                                    List<Question> selectedExam = new List<Question>();
                                                    bool found = false;
                                                    foreach (var ex in exams)
                                                    {
                                                        if (ex.GetID() == exID)
                                                        {
                                                            selectedExam = ex.Enroll();
                                                            found = true;
                                                        }
                                                    }
                                                    if (found)
                                                    {
                                                        double examMark = 0;
                                                        Console.WriteLine("Enrolling the Exam...");
                                                        foreach (var ques in selectedExam)
                                                        {
                                                            if(ques is MCQ)
                                                                Console.WriteLine("=================! More Than One correct please enter your answers with spaces in between !=============");
                                                            else if(ques is ChooseOne)
                                                                Console.WriteLine("=================! Choose only one and write the choice number !=============");
                                                            else if (ques is TrueOrFalse)
                                                                Console.WriteLine("=================! True or False Please write (T/t) or (F/f) only !=============");
                                                            Console.WriteLine(ques.DisplayQuestion());
                                                            Console.WriteLine("Enter your Answer");
                                                            Console.Write("Ans ---->");
                                                            if (ques.CheckAnswer(Console.ReadLine()))
                                                            {
                                                                examMark += ques.QuestionMarks;
                                                                Console.WriteLine("Correct Answer!");
                                                            }
                                                            else
                                                                Console.WriteLine("Wrong Answer :(");
                                                        }
                                                        studentMarks.Add(examMark);
                                                    }
                                                    else
                                                        Console.WriteLine("Did not find an exam match this ID, please try again");
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    if(studentMarks.Count != 0)
                                                    {
                                                    int i = 1;
                                                    foreach (var mark in studentMarks)
                                                    {
                                                        Console.WriteLine($"Exam {i} Mark: {mark}");
                                                        i++;
                                                    }
                                                    }
                                                    else
                                                        Console.WriteLine("No Enrolled Exams yet!");
                                                    break;
                                                }
                                            default:
                                                {
                                                    Console.WriteLine("Invalid Choice");
                                                    break;
                                                }
                                        }
                                    }
                                    catch(FormatException)
                                    {
                                        Console.WriteLine("Invalid Choice");
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid Choice");
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice please try again");
                }
                
            }
        }
    }
}
