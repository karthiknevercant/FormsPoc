using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace FormsPoc
{
    public partial class App : Application
    {
        List<Permission> all_permissions = new List<Permission>();

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SecondPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=0ba88937-fa84-4c51-ac5c-f67ecb7928aa;" +
                  "ios=5f70d34f-eb82-4eb6-9a55-b546a506e477;", 
            typeof(Analytics), typeof(Crashes));

            Get__Camera_Permission();             Get_Storage_Permission();             Get_GPS_Permission();             if (all_permissions != null)             {                 Request_Permission(all_permissions);             }
        }

        private async void Get_GPS_Permission()         {             await Check_permissions(Permission.Location);         }          public async Task Get__Camera_Permission()         {             await Check_permissions(Permission.Camera);         }         public async Task Get_Storage_Permission()         {             await Check_permissions(Permission.Storage);         }

        public async Task Request_Permission(List<Permission> permissions)         {             var result = await CrossPermissions.Current.RequestPermissionsAsync(permissions.ToArray());         }

        public async Task Check_permissions(Permission permission)         {             try             {                 var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);                 if (status != PermissionStatus.Granted)                 {                     all_permissions.Add(permission);

                }             }             catch (Exception e)             {                 string str = e.Message.ToString();             }         }

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
