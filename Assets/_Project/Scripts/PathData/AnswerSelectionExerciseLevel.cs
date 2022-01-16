using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class AnswerSelectionExerciseLevel : ExerciseLevel
    {
        public string Question { get; }
        public IList<string> Answers { get; }
        public int CorrectAnswer { get; }

        public AnswerSelectionExerciseLevel(DifficultyRange difficultyRange, string question,
            IList<string> answers, int correctAnswer) : base(difficultyRange)
        {
            Question = question;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}