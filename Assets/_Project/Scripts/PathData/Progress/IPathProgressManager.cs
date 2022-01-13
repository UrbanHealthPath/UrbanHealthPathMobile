using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public interface IPathProgressManager
    {
        event EventHandler CheckpointReached;
        
        bool IsPathInProgress { get; }
        PathProgressCheckpoint LastCheckpoint { get; }

        bool TryRestoreProgress();
        void StartNewPath();
        bool AddCheckpoint(PathProgressCheckpoint checkpoint);
        void CompletePath();
        void CancelPath();
        
    }
}