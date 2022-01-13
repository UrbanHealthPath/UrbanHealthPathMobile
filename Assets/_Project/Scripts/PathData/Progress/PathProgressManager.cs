﻿using System;
using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class PathProgressManager : IPathProgressManager
    {
        public event EventHandler CheckpointReached;
        
        private readonly IPathProgressPersistor _persistor;
        
        private PathProgress _currentProgress;

        public bool IsPathInProgress => _currentProgress is {WasFinished: false};
        public PathProgressCheckpoint LastCheckpoint => _currentProgress?.LastCheckpoint;

        public PathProgressManager(IPathProgressPersistor persistor)
        {
            _persistor = persistor;
        }

        public bool TryRestoreProgress()
        {
            PathProgress progress = _persistor.LoadPathProgress();

            if (progress != null)
            {
                _currentProgress = progress;
                return true;
            }

            return false;
        }

        public void StartNewPath()
        {
            if (_currentProgress != null)
            {
                CancelPath();
            }
            
            _currentProgress = new PathProgress(DateTime.Now, PathState.InProgress, new List<PathProgressCheckpoint>());
            SavePathProgress();
        }
        
        public bool AddCheckpoint(PathProgressCheckpoint checkpoint)
        {
            if (_currentProgress == null)
            {
                return false;
            }

            bool wasAdded = _currentProgress.AddCheckpoint(checkpoint);

            if (wasAdded)
            {
                OnCheckpointReached();
                SavePathProgress();
            }
            
            return wasAdded;
        }

        public void CompletePath()
        {
            if (_currentProgress == null)
            {
                return;
            }
            
            _currentProgress.CompletePath();
            SavePathProgress();
        }

        public void CancelPath()
        {
            if (_currentProgress == null)
            {
                return;
            }
            
            _currentProgress.CancelPath();
            SavePathProgress();
        }

        protected virtual void OnCheckpointReached()
        {
            CheckpointReached?.Invoke(this, EventArgs.Empty);
        }

        private void SavePathProgress()
        {
            _persistor.SavePathProgress(_currentProgress);
        }
    }
}