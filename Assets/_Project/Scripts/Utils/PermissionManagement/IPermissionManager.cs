using System;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    public interface IPermissionManager
    {
        void RequirePermission(Permission permissionRequest, Action<RequestResult> resultHandler);
    }
}