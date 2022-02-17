using System;
using System.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.MediaAccess
{
    public abstract class MediaFileAccessor<T>
    {
        protected readonly MediaFile _mediaFile;
        private MediaFileType[] _acceptedMediaFileTypes;

        protected MediaFileAccessor(MediaFile mediaFile, MediaFileType[] acceptedMediaFileTypes)
        {
            _mediaFile = mediaFile;
            _acceptedMediaFileTypes = acceptedMediaFileTypes;

            if (!_acceptedMediaFileTypes.Contains(mediaFile.Type))
            {
                throw new ArgumentException("Invalid MediaFile Type!", nameof(mediaFile));
            }
        }

        public T GetMedia()
        {
            if (_mediaFile.StorageType == MediaFileStorageType.Local)
            {
                return GetLocalMedia();
            }
            else
            {
                return GetRemoteMedia();
            }
        }

        protected abstract T GetLocalMedia();
        protected abstract T GetRemoteMedia();
    }
}