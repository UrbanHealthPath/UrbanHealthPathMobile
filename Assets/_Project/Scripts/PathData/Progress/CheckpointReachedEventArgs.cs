using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class CheckpointReachedEventArgs : EventArgs
    {
        public PathProgressCheckpoint Checkpoint { get; }

        public CheckpointReachedEventArgs(PathProgressCheckpoint checkpoint)
        {
            Checkpoint = checkpoint;
        }
    }
}