using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class PathProgress
    {
        public DateTime StartedAt { get; }
        [JsonIgnore] public PathProgressCheckpoint LastCheckpoint => _progressCheckpoints.LastOrDefault();

        [JsonProperty] private IList<PathProgressCheckpoint> _progressCheckpoints;

        public PathProgress(DateTime startedAt, IList<PathProgressCheckpoint> progressCheckpoints)
        {
            StartedAt = startedAt;
            _progressCheckpoints = progressCheckpoints;
        }

        public void AddCheckpoint(PathProgressCheckpoint checkpoint)
        {
            _progressCheckpoints.Add(checkpoint);
        }
    }
}