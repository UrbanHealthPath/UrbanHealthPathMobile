namespace PolSl.UrbanHealthPath.PathData
{
    public struct DifficultyRange
    {
        public int MinDifficulty { get; }
        public int MaxDifficulty { get; }

        public DifficultyRange(int minDifficulty, int maxDifficulty)
        {
            MinDifficulty = minDifficulty;
            MaxDifficulty = maxDifficulty;
        }
    }
}