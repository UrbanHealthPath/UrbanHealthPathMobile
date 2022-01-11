using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IMediaFilesLoader
    {
        IList<MediaFile> LoadMediaFiles();
    }
}