﻿using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Java.IO;
using System.IO;
using myApp;
using myApp.Droid;
[assembly: ExportRenderer(typeof(ImageWithTouch), typeof(ImageWithTouchRenderer))]

namespace myApp.Droid
{
    public class ImageWithTouchRenderer : ViewRenderer<ImageWithTouch, DrawView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ImageWithTouch> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                SetNativeControl(new DrawView(Context));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageWithTouch.ClearImagePathProperty.PropertyName)
            {
                Control.IsImageCleared = true;
                Control.Clear();
            }
            else if (e.PropertyName == ImageWithTouch.BackgroundImagePathProperty.PropertyName)
            {
                Control.LoadBackgroundImage(Element.BackgroundImagePath, Element.Width, Element.Height);
            }
            else if (e.PropertyName == ImageWithTouch.SavedImagePathProperty.PropertyName)
            {
                Bitmap curDrawingImage = Control.GetImageFromView();

                if (curDrawingImage != null)
                {
                    Control.IsImageCleared = false;

                    Byte[] imgBytes = ImageHelper.BitmapToBytes(curDrawingImage);
                    Java.IO.File f = new Java.IO.File(Element.SavedImagePath);

                    f.CreateNewFile();

                    FileOutputStream fo = new FileOutputStream(f);
                    fo.Write(imgBytes);

                    fo.Close();
                }
            }
            else
            {
                UpdateControl(true);
            }
        }

        private void UpdateControl(bool bDisplayFlag)
        {
            Control.CurrentLineColor = Element.CurrentLineColor.ToAndroid();
            Control.PenWidth = Element.CurrentLineWidth * 3;
            Control.ImageFilePath = Element.CurrentImagePath;

            if (bDisplayFlag)
            {
                Control.LoadImageFromFile();
                Control.Invalidate();
            }
        }
    }
}