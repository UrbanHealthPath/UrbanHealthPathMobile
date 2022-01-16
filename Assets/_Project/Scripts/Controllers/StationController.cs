using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManager;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class StationController : BaseController
    {
        private readonly PopupManager _popupManager;
        private readonly CoroutineManager _coroutineManager;
        private readonly IPathProgressManager _pathProgressManager;

        public StationController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager, IPathProgressManager pathProgressManager) : base(viewManager)
        {
            _popupManager = popupManager;
            _coroutineManager = coroutineManager;
            _pathProgressManager = pathProgressManager;
        }

        public void ShowNextStationConfirmation(UrbanPath path)
        {
            Station nextStation = GetNextStation(path.Waypoints.Select(waypoint => waypoint.Value).ToList());
            
            _coroutineManager.StartCoroutine(ShowNextStationConfirmationPopup(nextStation, () =>
            {
                _popupManager.CloseCurrentPopup();
                //BuildStationView(nextStation);
            }));
        }
        
        private IEnumerator ShowNextStationConfirmationPopup(Station nextStation, UnityAction confirmationButtonAction)
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new TextureFileAccessor(nextStation.Image).GetMedia();
            RectTransform transform = ViewManager.CurrentView.GetComponent<PathView>().PopupArea;

            _popupManager.OpenPopup(PopupType.ConfirmArrival,
                new PopupConfirmArrivalInitializationParameters(confirmationButtonAction, "Czy jesteś tutaj?", texture,
                    new PopupPayload(transform)));
        }
        
        private Station GetNextStation(IList<Waypoint> pathWaypoints)
        {
            PathProgressCheckpoint lastCheckpoint = _pathProgressManager.LastCheckpoint;

            if (lastCheckpoint is null)
            {
                return (Station) pathWaypoints.FirstOrDefault(x => x is Station);
            }

            Waypoint lastWaypoint = pathWaypoints.Single(x => x.WaypointId == lastCheckpoint.WaypointId);
            int indexOfLastWaypoint = pathWaypoints.IndexOf(lastWaypoint);

            for (int i = indexOfLastWaypoint + 1; i < pathWaypoints.Count; i++)
            {
                if (pathWaypoints[i] is Station station)
                {
                    return station;
                }
            }

            return null;
        }
    }
}