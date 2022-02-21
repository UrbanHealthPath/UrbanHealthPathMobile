﻿using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    /// <summary>
    /// Class responsible for loading configurable data regarding paths.
    /// </summary>
    public class ApplicationDataLoader : IApplicationDataLoader
    {
        private IMediaFilesLoader _mediaFilesLoader;
        private IExercisesLoader _exercisesLoader;
        private IWaypointsLoader _waypointsLoader;
        private IUrbanPathsLoader _urbanPathsLoader;

        public  ApplicationDataLoader(ILoadersFactory loadersFactory)
        {
            CreateLoaders(loadersFactory);
        }

        public IApplicationData LoadData()      
        {
            IList<MediaFile> mediaFiles = _mediaFilesLoader.LoadMediaFiles();
            IList<Exercise> exercises = _exercisesLoader.LoadExercises();
            IList<Waypoint> waypoints = _waypointsLoader.LoadWaypoints();
            IList<UrbanPath> urbanPaths = _urbanPathsLoader.LoadUrbanPaths();

            ApplicationData applicationData =
                new ApplicationData(mediaFiles, exercises, waypoints, urbanPaths);
            applicationData.SetLateBindings();
            
            return applicationData;
        }

        private void CreateLoaders(ILoadersFactory loadersFactory)
        {
            _mediaFilesLoader = loadersFactory.CreateMediaFilesLoader();
            _exercisesLoader = loadersFactory.CreateExercisesLoader();
            _waypointsLoader = loadersFactory.CreateWaypointsLoader();
            _urbanPathsLoader = loadersFactory.CreateUrbanPathsLoader();
        }
    }
}