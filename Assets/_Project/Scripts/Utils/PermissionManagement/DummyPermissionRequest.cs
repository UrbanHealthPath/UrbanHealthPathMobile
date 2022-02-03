using System;
using System.Threading.Tasks;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    public class DummyPermissionRequest : IPermissionRequest
    {
        public RequestResult Result { get; private set; }

        public void Request()
        {
            Result = RequestResult.Granted;
        }
    }
}