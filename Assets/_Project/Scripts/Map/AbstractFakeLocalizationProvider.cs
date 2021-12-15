using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
	public abstract class AbstractFakeLocalizationProvider : AbstractLocationProvider
	{
		[SerializeField] protected int _accuracy;

		[SerializeField] bool _sendEvent;

		WaitForSeconds _wait = new WaitForSeconds(0);
		
		protected IEnumerator QueryLocation()
		{
			SetLocation();
			SendLocation(_currentLocation);
			yield return _wait;
		}


		// Added to support TouchCamera script. 
		public void SendLocationEvent()
		{
			SetLocation();
			SendLocation(_currentLocation);
		}


		protected virtual void OnValidate()
		{
			if (_sendEvent)
			{
				_sendEvent = false;
				SendLocation(_currentLocation);
			}
		}

		protected abstract void SetLocation();
	}
}

