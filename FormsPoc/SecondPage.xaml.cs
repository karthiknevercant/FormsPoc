using System.Threading;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FormsPoc
{
    public partial class SecondPage : CustomPage
    {
        string path;
        public SecondPage()
        {
            InitializeComponent();
            drawingImage.IsEnabled = false;
            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction = async () =>
                {
                    var result = await this.DisplayAlert(null,
                        "Hey wait now! are you sure " +
                        "you want to go back?",
                        "Yes go back", "Nope");

                    if (result)
                    {
                        await Navigation.PopAsync(true);
                    }
                };
            }
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
                drawingImage.BackgroundImagePath = Takeresult.Path;
                drawingImage.SavedImagePath = DependencyService.Get<ISign>().Sign();
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
                drawingImage.IsEnabled = true;
                path = Pickresult.Path;
                drawingImage.BackgroundImagePath = Pickresult.Path;
                //drawingImage.SavedImagePath = DependencyService.Get<ISign>().Sign();
            }
        }

        async void CheckPhotoByChnaging(object sender, System.EventArgs e)
        {
            //drawingImage.ClearPath = true;
            if(!string.IsNullOrEmpty(drawingImage.BackgroundImagePath))
            {
                drawingImage.BackgroundImagePath = null;
               drawingImage.BackgroundImagePath = path;

                //drawingImage.SavedImagePath = DependencyService.Get<ISign>().Sign();
            }
                
            //drawingImage.ClearPath = true;
            //drawingImage.SavedImagePath = DependencyService.Get<ISign>().Sign();
            //drawingImage.ClearPath = true;
            //int milliseconds = 2000;
            //Thread.Sleep(milliseconds);
            //image.Source = DependencyService.Get<ISign>().Sign();

            //drawingImage.SavedImagePath = DependencyService.Get<ISign>().Sign();
            //drawingImage.CurrentImagePath = DependencyService.Get<ISign>().Sign();

        }


        //protected override bool OnBackButtonPressed()
        //{
        //    return base.OnBackButtonPressed();
        //}
    }
}
