using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class ImageSelectionExerciseLevel : ExerciseLevel
    {
        public string Question { get; }
        public IList<LateBoundValue<MediaFile>> Images { get; }
        public int CorrectAnswer { get; }

        public ImageSelectionExerciseLevel(DifficultyRange difficultyRange, string question,
            IList<LateBoundValue<MediaFile>> images, int correctAnswer) : base(difficultyRange)
        {
            Question = question;
            Images = images;
            CorrectAnswer = correctAnswer;
        }
    }
}