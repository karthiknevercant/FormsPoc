using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace FormsPoc
{
    public partial class SkiaSharpTestPage : ContentPage
    {
        Stream stream;

        public SkiaSharpTestPage()
        {
            InitializeComponent();

            //SKCanvas canvas = new SKCanvas(bitmap);
        }

        void Handle_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            //throw new NotImplementedException();
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            //SKBitmap sKBitmap = SKBitmap.Decode(stream);
            //canvas.DrawBitmap(sKBitmap, info.Rect);
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
                 stream = Takeresult.GetStream();

                //using (Stream stream = Takeresult.GetStream())
                //{
                //    if (stream != null)
                //    {
                //        //libraryBitmap = SKBitmap.Decode(stream);
                //        canvasView
                //    }
                //}
            }
        }
    }
}
