using System;
using UnityEngine;
using VoxelBusters.EssentialKit;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class ShareController
    {
        public event Action<string, SocialShareComposerResultCode> WhatsappShared;
        
        public void ShareWhatsapp(string message)
        {
            SocialShareComposer composer = SocialShareComposer.CreateInstance(SocialShareComposerType.WhatsApp);
            composer.SetText(message);
            composer.SetCompletionCallback((result, error) => {
                WhatsappShared?.Invoke(message, result.ResultCode);
            });
            composer.Show();
        }
    }
}