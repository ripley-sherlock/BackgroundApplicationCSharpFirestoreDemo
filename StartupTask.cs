using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace BackgroundApplicationCSharpFirestoreDemo
{
    public sealed class StartupTask : IBackgroundTask
    {
        private static void InitializeProjectId(string project)
        {
            // [START fs_initialize_project_id]
            FirestoreDb db = FirestoreDb.Create(project);
            Console.WriteLine("Created Cloud Firestore client with project ID: {0}", project);
            // [END fs_initialize_project_id]
        }

        private static async Task AddData1(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            // [START fs_add_data_1]
            DocumentReference docRef = db.Collection("users").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };
            await docRef.SetAsync(user);
            // [END fs_add_data_1]
            Console.WriteLine("Added data to the alovelace document in the users collection.");
        }

        private static async Task AddData2(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            // [START fs_add_data_2]
            DocumentReference docRef = db.Collection("users").Document("aturing");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Alan" },
                { "Middle", "Mathison" },
                { "Last", "Turing" },
                { "Born", 1912 }
            };
            await docRef.SetAsync(user);
            // [END fs_add_data_2]
            Console.WriteLine("Added data to the aturing document in the users collection.");
        }

        private static async Task RetrieveAllDocuments(string project)
        {
            FirestoreDb db = FirestoreDb.Create(project);
            // [START fs_get_all]
            CollectionReference usersRef = db.Collection("users");
            QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Console.WriteLine("User: {0}", document.Id);
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Console.WriteLine("First: {0}", documentDictionary["First"]);
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Console.WriteLine("Middle: {0}", documentDictionary["Middle"]);
                }
                Console.WriteLine("Last: {0}", documentDictionary["Last"]);
                Console.WriteLine("Born: {0}", documentDictionary["Born"]);
                Console.WriteLine();
            }
            // [END fs_get_all]
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            //
            // Create the deferral by requesting it from the task instance.
            //
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            
            string project = "seesharptestapp";
            //InitializeProjectId(project);
            //AddData1(project).Wait();
            FirestoreDb db = FirestoreDb.Create(project);
            // [START fs_add_data_1]
            DocumentReference docRef = db.Collection("users").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };

            //
            // Call asynchronous method(s) using the await keyword.
            //
            await docRef.SetAsync(user);
            // [END fs_add_data_1]
            Console.WriteLine("Added data to the alovelace document in the users collection.");

            //
            // Once the asynchronous method(s) are done, close the deferral.
            //
            deferral.Complete();

        }
    }
}
