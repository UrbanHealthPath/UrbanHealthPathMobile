using System;
using System.Threading.Tasks;

namespace PolSl.UrbanHealthPath.Utils.PermissionManagement
{
    public interface IPermissionRequest
    {
        RequestResult Result { get; }
        
        void Request();
    }
}