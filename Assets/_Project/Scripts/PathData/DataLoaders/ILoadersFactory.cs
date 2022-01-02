namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface ILoadersFactory
    {
        IMediaFilesLoader CreateMediaFilesLoader();
        IHistoricalFactsLoader CreateHistoricalFactsLoader();
        IExercisesLoader CreateExercisesLoader();
        IWaypointsLoader CreateWaypointsLoader();
        IUrbanPathsLoader CreateUrbanPathsLoader();
    }
}