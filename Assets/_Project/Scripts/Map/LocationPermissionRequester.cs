using UnityEngine.Android;

namespace PolSl.UrbanHealthPath.Map
{
    
    public class LocationPermissionRequester
    {
        public bool RequestPermission()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            return Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        }
    }
}
