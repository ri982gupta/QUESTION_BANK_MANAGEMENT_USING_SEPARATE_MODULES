using System;
using System.Collections.Generic;
using System.IO;
using QB.Model;

namespace QB.Domain
{
    public class QuizProcessor
    {
        public string ExtractTopic(string line)
        {
            try
            {
                return line.Substring(3).Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExtractTopic: {ex.Message}");
                throw;
            }
        }

        public string ExtractSubTopic(string line)
        {
            try
            {
                return line.Substring(4).Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExtractSubTopic: {ex.Message}");
                throw;
            }
        }

        public Question ExtractQuestion(string line, string topic, string subTopic)
        {
            try
            {
                var questionParts = line.Split(new[] { '.' }, 2);
                var questionId = questionParts[0].Trim();
                var questionTitle = questionParts[1].Trim();

                return new Question
                {
                    Id = questionId,
                    Title = questionTitle,
                    Options = new List<Option>(),
                    Topic = topic,
                    SubTopic = subTopic
                };
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ExtractQuestion: {ex.Message}");
                throw;
            }
        }

        public void ExtractOption(string line, Question question)
        {
            try
            {
                var optionParts = line.Split(new[] { '.' }, 2);
                var optionId = optionParts[0].Trim();
                var optionTitle = optionParts[1].Trim();
                bool isCorrect = optionId.EndsWith('*');
                if (isCorrect)
                    optionId = optionId.TrimEnd('*');

                var option = new Option
                {
                    Id = optionId,
                    Title = optionTitle,
                    IsCorrect = isCorrect
                };
                question.Options.Add(option);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ExtractOption: {ex.Message}");
                throw;
            }
        }
    }
}
