using System;
using System.Collections.Generic;
using System.Reflection;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FormsPoc
{
    public partial class ImageAnnotation : ContentPage
    {
        public ImageAnnotation()
        {
            InitializeComponent();
            //this.BindingContext = new ImageAnnotationVM();
        }

        async void AddPhotoClicked(object sender, System.EventArgs e)
        {
            var Takeresult = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                SaveToAlbum = false,
            });

            if (Takeresult != null)
            {
                imageEditor.Source = Takeresult.Path;
                imageEditor.IsEnabled = true;
            }
        }

        async void PickPhotoClickedAsync(object sender, System.EventArgs e)
        {
            var Pickresult = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 20
            });

            if (Pickresult != null)
            {
                imageEditor.Source = Pickresult.Path;
                imageEditor.IsEnabled = true;
            }
        }
    }

    public class ImageAnnotationVM
    {
        public ImageSource Image { get; set; }

        public ImageAnnotationVM()
        {
            Assembly assembly = typeof(ImageAnnotationVM).GetTypeInfo().Assembly;
            Image = ImageSource.FromResource("FormsPoc.go_back_modi.jpeg", assembly);
        }
    }
}
