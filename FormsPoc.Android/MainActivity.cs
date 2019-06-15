using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using System;

namespace FormsPoc.Droid
{
    [Activity(Label = "FormsPoc", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            //AppCenter.Start("0ba88937-fa84-4c51-ac5c-f67ecb7928aa", typeof(Analytics), typeof(Crashes));

        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
            //// check if the current item id 
            //// is equals to the back button id
            //if (item.ItemId == 16908332)
            //{
            //    // retrieve the current xamarin forms page instance
            //    var currentpage = (CustomPage)
            //      Xamarin.Forms.Application.
            //      Current.MainPage.Navigation.
            //      NavigationStack[Xamarin.Forms.Application.
            //      Current.MainPage.Navigation.
            //      NavigationStack.Count - 1];

            //    // check if the page has subscribed to 
            //    // the custom back button event
            //    if (currentpage?.CustomBackButtonAction != null)
            //    {
            //        // invoke the Custom back button action
            //        currentpage?.CustomBackButtonAction.Invoke();
            //        // and disable the default back button action
            //        return false;
            //    }

            //    // if its not subscribed then go ahead 
            //    // with the default back button action
            //    return base.OnOptionsItemSelected(item);
            //}
            //else
            //{
            //    // since its not the back button 
            //    //click, pass the event to the base
            //    return base.OnOptionsItemSelected(item);
            //}
        //}

        //public override void OnBackPressed()
        //{
            //    // this is not necessary, but in Android user 
            //    // has both Nav bar back button and
            //    // physical back button its safe 
            //    // to cover the both events

            //    // retrieve the current xamarin forms page instance
            //    var currentpage = (CustomPage)
            //    Xamarin.Forms.Application.
            //    Current.MainPage.Navigation.
            //    NavigationStack[Xamarin.Forms.Application.
            //    Current.MainPage.Navigation.
            //    NavigationStack.Count - 1];

            //    // check if the page has subscribed to 
            //    // the custom back button event
            //    if (currentpage?.CustomBackButtonAction != null)
            //    {
            //        currentpage?.CustomBackButtonAction.Invoke();
            //    }
            //    else
            //    {
            //        base.OnBackPressed();
            //    }
        //    base.OnBackPressed();
        //}

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //try
            //{
            //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //    //Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //    if (grantResults[0] == Android.Content.PM.Permission.Denied)
            //    {
            //        //var page = new QRScanner();
            //        //page.Title = "Scanning Package";
            //        //await Navigation.PushAsync(page);
            //        //Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
            //        // Xamarin.Forms.Application.Current.MainPage=new DashBordMenuePage();
            //    }
            //    else
            //    {
            //        ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //    }

            //    // global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //}
            //catch (Exception e)
            //{

            //}
        }
    }
}