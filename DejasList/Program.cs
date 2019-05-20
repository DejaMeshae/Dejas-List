using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DejasList
{
    class Program
    {


        //    [STAThread]
        //    static void Main(string[] args)
        //    {
        //        Console.WriteLine("Discovery API Sample");
        //        Console.WriteLine("====================");
        //        try
        //        {
        //            new Program().Run().Wait();
        //        }
        //        catch (AggregateException ex)
        //        {
        //            foreach (var e in ex.InnerExceptions)
        //            {
        //                Console.WriteLine("ERROR: " + e.Message);
        //            }
        //        }
        //        Console.WriteLine("Press any key to continue...");
        //        Console.ReadKey();
        //    }

        //    private async Task Run()
        //    {
        //        // Create the service.
        //        var service = new DiscoveryService(new BaseClientService.Initializer
        //        {
        //            ApplicationName = "Discovery Sample",
        //            ApiKey = APIKeys.GmailAPIKey,
        //        });

        //        // Run the request.
        //        Console.WriteLine("Executing a list request...");
        //        var result = await service.Apis.List().ExecuteAsync();

        //        // Display the results.
        //        if (result.Items != null)
        //        {
        //            foreach (DirectoryList.ItemsData api in result.Items)
        //            {
        //                Console.WriteLine(api.Id + " - " + api.Title);
        //            }
        //        }
        //    }
        //}





        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
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
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            // List labels.
            IList<Label> labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }
            Console.Read();
        }

    }
}