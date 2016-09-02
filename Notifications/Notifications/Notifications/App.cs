using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;

namespace Notifications
{
    public class App : Application
    {

        public static Editor tmp; /* inte så snyggt */
        public static Label services;
        public static Label sub;

        public App()
        {
            tmp = new Editor
            {
                Text = "Testing..."
            };
            services = new Label
            {
                Text = "is google up or down?"
            };
            sub = new Label
            {
                Text = "sub or not...."
            };

            // The root page of your application
            MainPage = new ContentPage
            {

                Content = new StackLayout
                {
                    
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "REGISTRATION TOKEN:"
                        },
                        tmp,
                        services,
                        sub
                    }
                    
                }
                
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
