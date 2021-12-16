namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public interface IPathProgressManager
    {
        PathProgress CurrentProgress { get; }

        void LoadProgress();
        void SaveProgress(PathProgress progress);
    }
}