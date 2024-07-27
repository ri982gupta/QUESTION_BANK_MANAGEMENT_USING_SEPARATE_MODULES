using System;
using System.Collections;
using System.IO;
using QB.ConsoleUI;

namespace QB.Main
{
    public class Program
    {
        static void Main()
        {
            QuizUI quizUIObj = new();

            Console.WriteLine("************************************** MCQ QUESTIONS **************************************");
            Console.WriteLine();
            string filePath = "C:/Users/RiGupta/Desktop/QUESTION__BANK/sample-questions.txt";

            var questions = quizUIObj.ProcessQuiz(filePath);
            quizUIObj.DisplayQuestions(questions);
        }
    }
}
