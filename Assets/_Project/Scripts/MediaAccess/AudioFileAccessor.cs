using System;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;

namespace PolSl.UrbanHealthPath.MediaAccess
{
    public class AudioFileAccessor : MediaFileAccessor<AudioClip>
    {
        public AudioFileAccessor(MediaFile mediaFile) : base(mediaFile, new []{MediaFileType.Audio})
        {
        }

        protected override AudioClip GetLocalMedia()
        {
            return Resources.Load<AudioClip>(_mediaFile.Path);
        }

        protected override AudioClip GetRemoteMedia()
        {
            throw new System.NotImplementedException();
        }
    }
}