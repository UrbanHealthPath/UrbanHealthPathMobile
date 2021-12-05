namespace PolSl.UrbanHealthPath.PathData
{
    public class MediaFile
    {
        private readonly MediaFileType _type;
        private readonly MediaFileStorageType _storageType;
        private readonly string _path;

        public string MediaId { get; }

        public MediaFile(string mediaId, MediaFileType type, MediaFileStorageType storageType, string path)
        {
            MediaId = mediaId;
            _type = type;
            _storageType = storageType;
            _path = path;
        }
    }
}