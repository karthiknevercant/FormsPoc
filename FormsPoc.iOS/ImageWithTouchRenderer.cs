using FallingWords.iOS.Render;
using FallingWords.Models;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ImageWithTouch), typeof(ImageWithTouchRenderer))]
namespace FallingWords.iOS.Render
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Models;
    using Foundation;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    /// <summary>
    /// Class ImageWithTouchRenderer.
    /// </summary>
    public class ImageWithTouchRenderer : ViewRenderer<ImageWithTouch, DrawView>
    {
        /// <summary>
        /// The draw view
        /// </summary>
        private DrawView _drawView;

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="elementChangedEventArgs">The element changed event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<ImageWithTouch> elementChangedEventArgs)
        {
            try
            {
                base.OnElementChanged(elementChangedEventArgs);

                _drawView = new DrawView(RectangleF.Empty);

                SetNativeControl(_drawView);
            }
            catch (Exception exception)
            {
                //LoggingManager.Error(exception);
            }
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

                if (e.PropertyName == ImageWithTouch.ClearImagePathProperty.PropertyName)
                {
                    Control.Clear();
                }
                else if (e.PropertyName == ImageWithTouch.SavedImagePathProperty.PropertyName)
                {
                    UIImage curDrawingImage = Control.GetImageFromView();
                    NSData data = curDrawingImage.AsPNG();
                    NSError error;
                    data.Save(Element.SavedImagePath, true, out error);
                }
                else if (e.PropertyName == ImageWithTouch.EraseImagePathProperty.PropertyName)
                {
                    Control.Erase();
                }
                else
                {
                    UpdateControl(e.PropertyName == ImageWithTouch.CurrentLineColorProperty.PropertyName ||
                                  e.PropertyName == ImageWithTouch.CurrentImageProperty.PropertyName ||
                                  e.PropertyName == ImageWithTouch.CurrentLineWidthProperty.PropertyName);
                }
                if (e.PropertyName == ImageWithTouch.UndoImagePathProperty.PropertyName)
                {
                    Control.OnClickUndo();
                }
                if (e.PropertyName == ImageWithTouch.RedoImagePathProperty.PropertyName)
                {
                    Control.OnClickRedo();
                }

                if (e.PropertyName == ImageWithTouch.CurrentLineColorProperty.PropertyName)
                {
                    UpdateControl(true);
                }
            }
            catch (Exception exception)
            {
                //LoggingManager.Error(exception);
            }
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="displayFlag">if set to <c>true</c> [display flag].</param>
        private void UpdateControl(bool displayFlag)
        {
            Control.CurrentLineColor = Element.CurrentLineColor.ToUIColor();

            Control.PenWidth = Element.CurrentLineWidth;

            if (Control.ImageFilePath != Element.CurrentImagePath)
            {
                Control.ImageFilePath = Element.CurrentImagePath;
                Control.LoadImageFromFile();
            }

            if (displayFlag)
            {
                Control.SetNeedsDisplay();
            }
        }
    }
}