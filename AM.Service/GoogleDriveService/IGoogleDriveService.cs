using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Service.GoogleDriveService
{
    public interface IGoogleDriveService
    {
        IList<Google.Apis.Drive.v3.Data.File> files();

        string UploadFile();

    }
}
