using System;
using System.Collections.Generic;

// Level Enum
enum Level
{
    Easy,
    Medium,
    Hard
}

// Base Question Class
class Question
{
    public string Header;
    public int Marks;
    public Level Level;

    public virtual void Display()
    {
        Console.WriteLine(Header);
    }

    public virtual bool CheckAnswer(string answer)
    {
        return false;
    }
}

// True False Question
class TrueFalseQuestion : Question
{
    public string CorrectAnswer;

    public override void Display()
    {
        Console.WriteLine(Header);
        Console.WriteLine("1. True");
        Console.WriteLine("2. False");
    }

    public override bool CheckAnswer(string answer)
    {
        return answer.ToLower() == CorrectAnswer.ToLower();
    }
}

// Choose One Question
class ChooseOneQuestion : Question
{
    public string[] Choices = new string[4];
    public int CorrectChoice;

    public override void Display()
    {
        Console.WriteLine(Header);

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine((i + 1) + ". " + Choices[i]);
        }
    }

    public override bool CheckAnswer(string answer)
    {
        return answer == CorrectChoice.ToString();
    }
}

// Multiple Choice Question
class MultipleChoiceQuestion : Question
{
    public string[] Choices = new string[4];
    public string CorrectAnswers;

    public override void Display()
    {
        Console.WriteLine(Header);

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine((i + 1) + ". " + Choices[i]);
        }

        Console.WriteLine("Enter answers separated by comma");
    }

    public override bool CheckAnswer(string answer)
    {
        return answer.Replace(" ", "") ==
               CorrectAnswers.Replace(" ", "");
    }
}

class Program
{
    static List<Question> QuestionBank =
        new List<Question>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== Examination System =====");
            Console.WriteLine("1. Doctor Mode");
            Console.WriteLine("2. Student Mode");
            Console.WriteLine("3. Exit");

            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                DoctorMode();
            }
            else if (choice == "2")
            {
                StudentMode();
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }

    // Doctor Mode
    static void DoctorMode()
    {
        Console.Write("Number of questions: ");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine();
            Console.WriteLine("Question Type");
            Console.WriteLine("1. True/False");
            Console.WriteLine("2. Choose One");
            Console.WriteLine("3. Multiple Choice");

            int type = int.Parse(Console.ReadLine());

            Console.WriteLine("Level");
            Console.WriteLine("0. Easy");
            Console.WriteLine("1. Medium");
            Console.WriteLine("2. Hard");

            Level level =
                (Level)int.Parse(Console.ReadLine());

            Console.Write("Question Header: ");
            string header = Console.ReadLine();

            Console.Write("Marks: ");
            int marks = int.Parse(Console.ReadLine());

            if (type == 1)
            {
                TrueFalseQuestion q =
                    new TrueFalseQuestion();

                q.Header = header;
                q.Marks = marks;
                q.Level = level;

                Console.Write("Correct Answer (True/False): ");
                q.CorrectAnswer = Console.ReadLine();

                QuestionBank.Add(q);
            }

            else if (type == 2)
            {
                ChooseOneQuestion q =
                    new ChooseOneQuestion();

                q.Header = header;
                q.Marks = marks;
                q.Level = level;

                for (int j = 0; j < 4; j++)
                {
                    Console.Write("Choice " + (j + 1) + ": ");
                    q.Choices[j] = Console.ReadLine();
                }

                Console.Write("Correct Choice Number: ");
                q.CorrectChoice =
                    int.Parse(Console.ReadLine());

                QuestionBank.Add(q);
            }

            else if (type == 3)
            {
                MultipleChoiceQuestion q =
                    new MultipleChoiceQuestion();

                q.Header = header;
                q.Marks = marks;
                q.Level = level;

                for (int j = 0; j < 4; j++)
                {
                    Console.Write("Choice " + (j + 1) + ": ");
                    q.Choices[j] = Console.ReadLine();
                }

                Console.Write("Correct Answers (1,2): ");
                q.CorrectAnswers =
                    Console.ReadLine();

                QuestionBank.Add(q);
            }
        }

        Console.WriteLine("Questions Added Successfully");
    }

    // Student Mode
    static void StudentMode()
    {
        if (QuestionBank.Count == 0)
        {
            Console.WriteLine("No Questions Found");
            return;
        }

        Console.WriteLine("Exam Type");
        Console.WriteLine("1. Practical");
        Console.WriteLine("2. Final");

        int examType =
            int.Parse(Console.ReadLine());

        Console.WriteLine("Level");
        Console.WriteLine("0. Easy");
        Console.WriteLine("1. Medium");
        Console.WriteLine("2. Hard");

        Level level =
            (Level)int.Parse(Console.ReadLine());

        List<Question> examQuestions =
            new List<Question>();

        foreach (Question q in QuestionBank)
        {
            if (q.Level == level)
            {
                examQuestions.Add(q);
            }
        }

        if (examQuestions.Count == 0)
        {
            Console.WriteLine("No Questions In This Level");
            return;
        }

        if (examType == 1)
        {
            int half = examQuestions.Count / 2;

            if (half == 0)
                half = 1;

            examQuestions =
                examQuestions.GetRange(0, half);
        }

        int studentResult = 0;
        int totalMarks = 0;

        foreach (Question q in examQuestions)
        {
            totalMarks += q.Marks;

            Console.WriteLine();
            q.Display();

            Console.Write("Answer: ");
            string answer = Console.ReadLine();

            if (q.CheckAnswer(answer))
            {
                studentResult += q.Marks;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Your Result : "
                          + studentResult
                          + " / "
                          + totalMarks);
    }
}
