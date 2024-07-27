using System;
using System.Collections.Generic;
using QB.Model;
using QB.Domain;

namespace QB.ConsoleUI
{
    public class QuizUI
    {
        QuizProcessor quizProcessorObj = new();

        public void DisplayQuestions(List<Question> questions)
        {
            foreach (var question in questions)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine($"Topic: {question.Topic}");
                Console.WriteLine();
                Console.WriteLine($"Sub-Topic: {question.SubTopic}");
                Console.WriteLine();
                Console.WriteLine($"Question ID: {question.Id}");
                Console.WriteLine($"Title: {question.Title}");
                Console.WriteLine($"Options:");
                foreach (var option in question.Options)
                {
                    Console.WriteLine($"{option.Id}. {option.Title} (Correct: {option.IsCorrect})");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine("*******************************************************************************************");
        }

        public List<Question> ProcessQuiz(string filePath)
        {
            try
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                var questions = new List<Question>();
                Question? currentQuestion = null;
                string? currentTopic = null;
                string? currentSubTopic = null;

                foreach (var line in lines)
                {
                    if (line.StartsWith("## "))
                    {
                        currentTopic = quizProcessorObj.ExtractTopic(line);
                        currentQuestion = null;
                        currentSubTopic = null;
                    }
                    else if (line.StartsWith("### "))
                    {
                        currentSubTopic = quizProcessorObj.ExtractSubTopic(line);
                        currentQuestion = null;
                    }
                    else if (line.Contains('.') && int.TryParse(line.Split('.')[0], out int questionNumber))
                    {
                        currentQuestion = quizProcessorObj.ExtractQuestion(line, currentTopic!, currentSubTopic!);
                        questions.Add(currentQuestion);
                    }
                    else if (currentQuestion != null && !string.IsNullOrWhiteSpace(line) && line.Contains('.'))
                    {
                        quizProcessorObj.ExtractOption(line, currentQuestion);
                    }
                }

                return questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProcessQuiz: {ex.Message}");
                throw; 
            }
        }
    }
}
