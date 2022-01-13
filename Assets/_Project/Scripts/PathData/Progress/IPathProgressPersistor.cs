namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public interface IPathProgressPersistor
    {
        PathProgress LoadPathProgress();
        bool SavePathProgress(PathProgress progress);
    }
}