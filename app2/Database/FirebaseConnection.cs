using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace app2.Database
{
    public class FirebaseConnection
    {
        //firebase connection Settings
        public IFirebaseConfig fc = new FirebaseConfig()
        {
            AuthSecret = "fv1FY8DjZi0d0oAz0BOdCu1gs32Pq3LXJ4hFUD2o",
            //BasePath = "iotproject-75b11-default-rtdb.firebaseio.com"
            BasePath = "https://iotproject-75b11-default-rtdb.firebaseio.com/"

        };

        public IFirebaseClient client;
        //Code to warn console if class cannot connect when called.
        public FirebaseConnection()
        {
            try
            {
                client = new FireSharp.FirebaseClient(fc);
                Console.WriteLine("Connection good");

            }
            catch (Exception)
            {
                Console.WriteLine("Connection Error");
            }
        }
    }
}
