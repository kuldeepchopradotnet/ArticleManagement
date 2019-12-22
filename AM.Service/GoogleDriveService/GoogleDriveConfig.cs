using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AM.Service.GoogleDriveService
{
    public static class GoogleDriveConfig
    {
        //public GoogleDriveConfig()
        //{
        //   // AuthCredential();
        //}

        public static void AddAuthDriveCredential(this IServiceCollection services)
        {
           // string[] Scopes = { DriveService.Scope.Drive };
             string[] Scopes = { DriveService.Scope.Drive,
                       DriveService.Scope.DriveAppdata,
                       DriveService.Scope.DriveFile,
                       DriveService.Scope.DriveMetadataReadonly,
                       DriveService.Scope.DriveReadonly,
                       DriveService.Scope.DriveScripts };

            UserCredential credential;
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            services.AddScoped<IGoogleDriveService>(x => new GoogleDriveService(credential));
        }




    }
}
