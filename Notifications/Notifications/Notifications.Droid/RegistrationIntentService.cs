using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using System.Diagnostics;

namespace Notifications.Droid
{
    [Service(Exported = false)]
    class RegistrationIntentService : IntentService
    {
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                lock (locker)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    var token = instanceID.GetToken(
                        "1014112629098", GoogleCloudMessaging.InstanceIdScope, null);

                    Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                    SendRegistrationToAppServer(token);
                    Subscribe(token);
                }
            }
            catch (Exception e)
            {
                App.sub.Text = "not good";
                Log.Debug("RegistrationIntentService", e.ToString());
                return;
            }
        }

        void SendRegistrationToAppServer(string token)
        {
            App.tmp.Text = token;
         //  Console.OpenStandardOutput();
         //  System.Diagnostics.Debug.WriteLine(token);
         //  Console.WriteLine(token);
            Debug.WriteLine(token);
        }

        void Subscribe(string token)
        {
            var pubSub = GcmPubSub.GetInstance(this);
            pubSub.Subscribe(token, "/topics/global", null);
            App.sub.Text = "subscribed";
        }
    }
}