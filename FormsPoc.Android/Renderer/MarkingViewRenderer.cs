using System;
using System.IO;
using Android.Graphics;
using Android.Graphics.Drawables;
using FormsPoc;
using FormsPoc.Droid.Renderer;
using SignaturePad.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android; 

//[assembly: ExportRenderer(typeof(MarkingView), typeof(MarkingViewRenderer))]
namespace FormsPoc.Droid.Renderer
{
    //public class MarkingViewRenderer : ViewRenderer<MarkingView, SignaturePadView>
    //{
    //    protected override void OnElementChanged(ElementChangedEventArgs<MarkingView> e)
    //    {
    //        base.OnElementChanged(e);

    //        if (e.OldElement != null)
    //        {
    //            e.OldElement.SaveImageWithBackground = null;
    //        }
    //        if (e.NewElement != null)
    //        {
    //            e.NewElement.SaveImageWithBackground += NewElement_SaveImageWithBackground;
    //        }
    //    }

    //    string NewElement_SaveImageWithBackground()
    //    {
    //        using (Bitmap bm = Bitmap.CreateBitmap(Width, Height, Bitmap.Config.Argb8888))
    //        {
    //            using (Canvas canvas = new Canvas(bm))
    //            {
    //                Drawable bgDrawable = Background;

    //                if (bgDrawable != null)
    //                {
    //                    bgDrawable.Draw(canvas);
    //                }

    //                Draw(canvas);

    //                string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    //                string filename;
    //                do
    //                {
    //                    filename = System.IO.Path.Combine(folder, "Marking-" + DateTime.Now.Ticks + ".png");
    //                }
    //                while (File.Exists(filename));

    //                using (var fs = new System.IO.FileStream(filename, FileMode.CreateNew))
    //                {
    //                    bm.Compress(Bitmap.CompressFormat.Png, 90, fs);
    //                    bm.Recycle();

    //                    return filename;
    //                }
    //            }
    //        }
    //    }
    //}
}
