using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;
using System.Collections.Generic;

namespace MessageSender
{
    class MessageSender
    {
        //private static IEnumerable<string> MY_REGISTRATION_IDS;
        public const string API_KEY = "AIzaSyBNWqMjtuyhzgGn5mbAU1v_OHy0zhpeimY";
        public const string MESSAGE = "Hello, Xamarin!";

        static void Main(string[] args)
        {
            /*
            var jGcmData = new JObject();
            var jData = new JObject();

            jData.Add("message", MESSAGE);
            jGcmData.Add("to", "/topics/global");
            jGcmData.Add("data", jData);

            var url = new Uri("https://gcm-http.googleapis.com/gcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + API_KEY);

                    Task.WaitAll(client.PostAsync(url,
                        new StringContent(jGcmData.ToString(), Encoding.Default, "application/json"))
                            .ContinueWith(response =>
                            {
                                Console.WriteLine(response);
                                Console.WriteLine("Message sent: check the client device notification tray.");
                            }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to send GCM message:");
                Console.Error.WriteLine(e.StackTrace);
            }

            */

            /*
            

            // Configuration
            var config = new GcmConfiguration("1014112629098", "AIzaSyBNWqMjtuyhzgGn5mbAU1v_OHy0zhpeimY", "com.example.notifications");

           // Console.WriteLine("TEST: " + config.SenderAuthToken);
            // Create a new broker

            var gcmBroker = new GcmServiceBroker(config);
         //  Console.WriteLine("TEST: " + gcmBroker.ToString());
         //
         //   gcmBroker.QueueNotification(new GcmNotification()
         //  .ForDeviceRegistrtionId(googleToken)
         //  .WithData(new Dictionary<string, string>()
         //  {
         //      { "message", "Hello World" },
         //      { "title", "Test" }
         //  }));
         //
         //  gcmBroker.QueueNotification(new GcmNotification()
         //  {
         //
         //  }

            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {
                    Console.WriteLine("ON NOTIFICATION FAILED");
                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        Console.WriteLine("ON NOTIFICATION FAILED");
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine($"GCM Notification Failed: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc=");
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                    }
                    else
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            gcmBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("GCM Notification Sent!");
            };

            string s = "dS9gfDvvRb0:APA91bHwKx_MoqS3PfEnEZgokiGQyd0rSCqdspC--ueAWn5aqxGYdA6qxxJ637EpEoX8i9oLa3mEWPiZcTDavTCGqAlqHSOpPeHsA47aYUirKY1ZF7DAYh6tNJOZNBASG7MggrSLew0l";
            // Start the broker
            gcmBroker.Start();

            MY_REGISTRATION_IDS = new List<string>()
            {
                s
            };
            Console.WriteLine("ENDING");
            foreach (var regId in MY_REGISTRATION_IDS)
            {
                // Queue a notification to send
                Console.WriteLine("IN FOREACH");
                gcmBroker.QueueNotification(new GcmNotification
                {
                    RegistrationIds = new List<string> {
                        regId
                    },
                    //Data = JObject.Parse("{ \"somekey\" : \"somevalue\" }")
                });
                Console.WriteLine("BROKER: " + gcmBroker.ToString()); 
            } 

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            gcmBroker.Stop();
            
    */




            //2.2.1 
            //Create our push services broker            
            var push = new PushBroker();
            
            //Wire up the events for all the services that the broker registers
            push.OnNotificationSent += NotificationSent;
            push.OnChannelException += ChannelException;
            push.OnServiceException += ServiceException;
            push.OnNotificationFailed += NotificationFailed;
            push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            push.OnChannelCreated += ChannelCreated;
            push.OnChannelDestroyed += ChannelDestroyed;


            List<String> devices = new List<String>();  // Hämta alla devices till den här listan på något vis?
            devices.Add("dvsG2nQmqQk:APA91bHl1Ok4bNALtKLYAWYS3y_UKQ3qsM0Xn2K_n5OOO8Y1yL4bz92Vo5OS9JWH_nO8XEZaGe6ZXCRWvvAI5LoUaUb1-LStjMxJgxlJGwhjn18Po3F0QlI8viSIq5HLSWcgTP_thIgF"); // Lägger till ID för min emulator

            // Skicka till alla devices
            foreach(string d in devices)
            {
                push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBNWqMjtuyhzgGn5mbAU1v_OHy0zhpeimY"));

                push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(d)
                                      .WithJson(@"{""message"":""glöm inte matlåda"",""to"":""/topics/global"",""title"":""säger hej"",""badge"":7,""sound"":""sound.caf""}"));
            }

            //   Stop and wait for the queues to drains before it dispose 
            push.StopAllServices();

        }

        static void DeviceSubscriptionChanged(object sender,
        string oldSubscriptionId, string newSubscriptionId, INotification notification)
        {
            Console.WriteLine("DeviceSubscriptionChanged***");
            Console.WriteLine(notification);
        }

        //this even raised when a notification is successfully sent
        static void NotificationSent(object sender, INotification notification)
        {
            Console.WriteLine("Notification sent***");
            Console.WriteLine(notification);
        }

        //this is raised when a notification is failed due to some reason
        static void NotificationFailed(object sender,
        INotification notification, Exception notificationFailureException)
        {
            Console.WriteLine("NOTIFICATION FAILED***");
            Console.WriteLine(notificationFailureException);
        }

        //this is fired when there is exception is raised by the channel
        static void ChannelException
            (object sender, IPushChannel channel, Exception exception)
        {
            Console.WriteLine("ChannelException***");
            Console.WriteLine(exception);
        }

        //this is fired when there is exception is raised by the service
        static void ServiceException(object sender, Exception exception)
        {
            Console.WriteLine("ServiceException***");
            Console.WriteLine(exception);
        }

        //this is raised when the particular device subscription is expired
        static void DeviceSubscriptionExpired(object sender,
        string expiredDeviceSubscriptionId,
            DateTime timestamp, INotification notification)
        {
            Console.WriteLine("DeviceSubscriptionExpired***");
            Console.WriteLine(notification);
        }

        //this is raised when the channel is destroyed
        static void ChannelDestroyed(object sender)
        {
            Console.WriteLine("ChannelDestroyed***");
            Console.WriteLine(sender);
        }

        //this is raised when the channel is created
        static void ChannelCreated(object sender, IPushChannel pushChannel)
        {
            Console.WriteLine("ChannelCreated***");
            Console.WriteLine(pushChannel);
        }
    }
}