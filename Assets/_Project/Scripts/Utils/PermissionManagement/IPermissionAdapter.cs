namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    public interface IPermissionAdapter
    {
        IPermissionRequest CreateRequest(Permission permission);
        bool HasPermission(Permission permission);
    }
}