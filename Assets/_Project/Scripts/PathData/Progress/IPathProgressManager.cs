﻿using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public interface IPathProgressManager
    {
        event EventHandler CheckpointReached;
        
        bool IsPathInProgress { get; }

        bool TryRestoreProgress();
        void StartNewPath();
        bool AddCheckpoint(PathProgressCheckpoint checkpoint);
        void CancelPath();
        
    }
}