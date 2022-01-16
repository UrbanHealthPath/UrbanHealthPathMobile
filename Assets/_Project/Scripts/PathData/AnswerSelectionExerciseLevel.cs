using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class AnswerSelectionExerciseLevel : ExerciseLevel
    {
        public string Question { get; }
        public IList<string> Answers { get; }
        public IList<int> CorrectAnswers { get; }

        public AnswerSelectionExerciseLevel(DifficultyRange difficultyRange, string question,
            IList<string> answers, IList<int> correctAnswers) : base(difficultyRange)
        {
            Question = question;
            Answers = answers;
            CorrectAnswers = correctAnswers;
        }
    }
}