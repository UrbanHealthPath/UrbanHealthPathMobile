namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface ILoadersFactory
    {
        IMediaFilesLoader CreateMediaFilesLoader();
        IExercisesLoader CreateExercisesLoader();
        IWaypointsLoader CreateWaypointsLoader();
        IUrbanPathsLoader CreateUrbanPathsLoader();
        ITestLoader CreateTestLoader();
    }
}