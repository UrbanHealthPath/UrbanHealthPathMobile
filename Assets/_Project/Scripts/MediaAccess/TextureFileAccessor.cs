using System;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;

namespace PolSl.UrbanHealthPath.MediaAccess
{
    public class TextureFileAccessor : MediaFileAccessor<Texture2D>
    {
        public TextureFileAccessor(MediaFile mediaFile) : base(mediaFile, new [] {MediaFileType.Image})
        {
        }

        protected override Texture2D GetLocalMedia()
        {
            return Resources.Load<Texture2D>(_mediaFile.Path);
        }

        protected override Texture2D GetRemoteMedia()
        {
            throw new NotImplementedException();
        }
    }
}