using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Service.GoogleDriveService
{
    public class GoogleDriveService : IGoogleDriveService
    {
        string ApplicationName = "DriveAPItest";
        private readonly DriveService service;
        public GoogleDriveService(UserCredential userCredential)
        {
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });
        }

        public IList<Google.Apis.Drive.v3.Data.File> files()
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            return  listRequest.Execute()
                .Files;
            //Console.WriteLine("Files:");
            //if (files != null && files.Count > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        Console.WriteLine("{0} ({1})", file.Name, file.Id);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No files found.");
            //}
        }


        public string UploadFile() {
            try
            {
                var fileMetadata = new File()
                {
                    Name = "Screenshot_1.jpg"
                };

                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream("Screenshot_1.jpg",
                                        System.IO.FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream, "image/jpeg");
                    request.Fields = "idnewxersdf";
                    request.Upload();
                }
                var file = request.ResponseBody;
                return "";
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }


    }
}
