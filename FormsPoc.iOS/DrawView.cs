namespace FallingWords.iOS.Render
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using CoreGraphics;

    using FallingWords.Helpers;

    using Foundation;

    using UIKit;

    /// <summary>
    /// Class DrawView.
    /// </summary>
    /// <seealso cref="UIKit.UIView" />
    public class DrawView : UIView
    {
        /// <summary>
        /// The history lines
        /// </summary>
        private readonly Stack<VESLine> _historyLines = new Stack<VESLine>();

        /// <summary>
        /// The current path
        /// </summary>
        private UIBezierPath _currentPath;

        /// <summary>
        /// The image
        /// </summary>
        private UIImage _image;

        /// <summary>
        /// The index count
        /// </summary>
        private byte _indexCount;

        /// <summary>
        /// The lines
        /// </summary>
        private List<VESLine> _lines;

        /// <summary>
        /// The previous point
        /// </summary>
        private CGPoint _previousPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawView"/> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        public DrawView(RectangleF frame)
        : base(frame)
        {
            CurrentLineColor = UIColor.Black;
            PenWidth = 30.0f;
            _lines = new List<VESLine>();
        }

        /// <summary>
        /// Gets or sets the color of the current line.
        /// </summary>
        /// <value>The color of the current line.</value>
        public UIColor CurrentLineColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image file path.
        /// </summary>
        /// <value>The image file path.</value>
        public String ImageFilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the pen.
        /// </summary>
        /// <value>The width of the pen.</value>
        public float PenWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Saves the rotate image.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SaveRotateImage(string imagePath)
        {
            UIImage imageCopy = GetRotateImage(imagePath);

            NSData data = imageCopy.AsPNG();
            NSError error;
            bool bSuccess = data.Save(imagePath, true, out error);

            return bSuccess;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            CurrentLineColor = UIColor.Black;
            PenWidth = 30.0f;
            _lines = new List<VESLine>();
            InvokeOnMainThread(SetNeedsDisplay);
        }

        /// <summary>
        /// Draws the view within the passed-in rectangle.
        /// </summary>
        /// <param name="rect">The <see cref="T:System.Drawing.RectangleF" /> to draw.</param>
        /// <remarks><para>
        /// The <see cref="M:UIKit.UIView.Draw" /> method should never be called directly. It is called by iOS
        /// during run loop processing. The first time through the run loop, it
        /// is called. After that, it will be called on demand whenever the view
        /// has been marked as needing display by calling <see cref="M:UIKit.UIView.SetNeedsDisplay" /> or
        /// <see cref="M:UIKit.UIView.SetNeedsDisplayInRect(System.Drawing.RectangleF)" />.
        /// </para>
        /// <para>
        /// Core Graphics uses device independent points rather than
        /// pixels. This allows drawing code to scale between different
        /// resolutions. For example, on a Retina display, 1 point is equivalent
        /// to 2 pixels, while on non-Retina displays, 1 point corresponds to 1
        /// pixel.
        /// </para>
        /// <example>
        ///   <code lang="C#"><![CDATA[
        /// public override void Draw (RectangleF rect)
        /// {
        /// base.Draw (rect);
        /// var context = UIGraphics.GetCurrentContext ();
        /// context.SetLineWidth(4);
        /// UIColor.Red.SetFill ();
        /// UIColor.Blue.SetStroke ();
        /// var path = new CGPath ();
        /// path.AddLines(new PointF[]{
        /// new PointF(100,200),
        /// new PointF(160,100),
        /// new PointF(220,200)});
        /// path.CloseSubpath();
        /// context.AddPath(path);
        /// context.DrawPath(CGPathDrawingMode.FillStroke);
        /// }
        /// ]]></code>
        /// </example>
        /// <para tool="threads">This can be used from a background thread.</para></remarks>
        public override void Draw(CGRect rect)
        {
            foreach (var line in _lines)
            {
                line.Color.SetStroke();
                line.Path.Stroke();
            }
        }

        /// <summary>
        /// Erases this instance.
        /// </summary>
        public void Erase()
        {
            CurrentLineColor = UIColor.White;
            PenWidth = 30.0f;
        }

        /// <summary>
        /// Gets the image from view.
        /// </summary>
        /// <returns>UIImage.</returns>
        public UIImage GetImageFromView()
        {
            var rect = (RectangleF)Frame;

            UIGraphics.BeginImageContextWithOptions(rect.Size, false, 2.0f);

            CGContext context = UIGraphics.GetCurrentContext();

            if (_image != null)
            {
                context.DrawImage(Frame, _image.CGImage);
            }

            Layer.RenderInContext(context);

            UIImage image = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            return image;
        }

        /// <summary>
        /// Loads the image from file.
        /// </summary>
        public void LoadImageFromFile()
        {
            if (!string.IsNullOrEmpty(ImageFilePath))
            {
                _image = GetRotateImage(ImageFilePath);
            }
        }

        /// <summary>
        /// Called when [click redo].
        /// </summary>
        public void OnClickRedo()
        {
            try
            {
                if (_historyLines.Count != 0)
                {
                    var recentLine = _historyLines.Pop();
                    _lines.Add(recentLine);
                    InvokeOnMainThread(SetNeedsDisplay);
                }

            }
            catch (InvalidOperationException exception)
            {
                LoggingManager.Error(exception);
            }
        }

        /// <summary>
        /// Called when [click undo].
        /// </summary>
        public void OnClickUndo()
        {
            try
            {
                if (_lines.Count >= 1)
                {
                    _historyLines.Push(_lines[_lines.Count - 1]);
                    _lines.RemoveAt(_lines.Count - 1);
                    InvokeOnMainThread(SetNeedsDisplay);
                }
            }
            catch (StackOverflowException exception)
            {
                LoggingManager.Error(exception);
            }
        }

        /// <summary>
        /// Sent when one or more fingers touches the screen.
        /// </summary>
        /// <param name="touches">Set containing the touches as objects of type <see cref="T:UIKit.UITouch" />.</param>
        /// <param name="evt"><para>The UIEvent that encapsulates all of the touches and the event information.</para>
        /// <para tool="nullallowed">This parameter can be <see langword="null" />.</para></param>
        /// <remarks><para>
        /// The <paramref name="touches" /> set containing all of the touch events.
        /// </para>
        /// <para>
        /// If your application tracks the touches starting with this
        /// method, it should also override both the <see cref="M:UIKit.UIResponder.TouchesEnded(Foundation.NSSet,&#xA;     UIKit.UIEvent)" /> and <see cref="M:UIKit.UIResponder.TouchesCancelled(Foundation.NSSet,&#xA;     UIKit.UIEvent)" /> methods to track the end of
        /// the touch processing.
        /// </para>
        /// <para>
        /// UIViews by default only receive a single touch event at
        /// once, if you want to receive multiple touches at the same
        /// time, set the <see cref="P:UIView.MultipleTouchEnabled" /> property
        /// to true.
        /// </para>
        /// <para>
        /// If you only want to handle a single touch, the following idiom can be used:
        /// </para>
        /// <example>
        ///   <code lang="C#"><![CDATA[
        /// public override void TouchesBegan (NSSet touches, UIEvent evt)
        /// {
        /// var touch = touches.AnyObject as UITouch;
        /// Console.WriteLine (touch);
        /// }
        /// ]]></code>
        /// </example>
        /// <para>
        /// If you want to handle multiple touches, you can use this idiom:
        /// </para>
        /// <example>
        ///   <code lang="C#"><![CDATA[
        /// public override void TouchesBegan (NSSet touches, UIEvent evt)
        /// {
        /// foreach (UITouch touch in touches.ToArray<UITouch> ()){
        /// Console.WriteLine (touch);
        /// }
        /// }
        /// ]]></code>
        /// </example></remarks>
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            _indexCount++;

            var path = new UIBezierPath
            {
                LineWidth = PenWidth
            };

            var touch = (UITouch)touches.AnyObject;
            _previousPoint = touch.PreviousLocationInView(this);

            var newPoint = touch.LocationInView(this);
            path.MoveTo(newPoint);

            InvokeOnMainThread(SetNeedsDisplay);

            _currentPath = path;

            var line = new VESLine
            {
                Path = _currentPath,
                Color = CurrentLineColor,
                Index = _indexCount
            };

            _lines.Add(line);
        }

        /// <summary>
        /// Sent when the touch processing has been cancelled.
        /// </summary>
        /// <param name="touches">Set containing the touches as objects of type <see cref="T:UIKit.UITouch" />.</param>
        /// <param name="evt"><para>The UIEvent that encapsulates all of the touches and the event information.</para>
        /// <para tool="nullallowed">This parameter can be <see langword="null" />.</para></param>
        /// <remarks>This method is typically involved because the application
        /// was interrupted by an external source, like for example,
        /// an incoming phone call.</remarks>
        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            InvokeOnMainThread(SetNeedsDisplay);
        }

        /// <summary>
        /// Sent when one or more fingers are lifted from the screen.
        /// </summary>
        /// <param name="touches">Set containing the touches as objects of type <see cref="T:UIKit.UITouch" />.</param>
        /// <param name="evt"><para>The UIEvent that encapsulates all of the touches and the event information.</para>
        /// <para tool="nullallowed">This parameter can be <see langword="null" />.</para></param>
        /// <remarks><para>The default implementation of this method does nothing. However immediate UIKit subclasses of UIResponder, particularly UIView, forward the message up the responder chain. To forward the message to the next responder, send the message to super (the superclass implementation); do not send the message directly to the next responder. For example, </para>
        /// <para>When an object receives a touchesEnded:withEvent: message it should clean up any state information that was established in its touchesBegan:withEvent: implementation.</para>
        /// <para>Multiple touches are disabled by default. In order to receive multiple touch events you must set the a multipleTouchEnabled property of the corresponding view instance to YES.</para>
        /// <para>If you override this method without calling super (a common use pattern), you must also override the other methods for handling touch events, if only as stub (empty) implementations.</para></remarks>
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            InvokeOnMainThread(SetNeedsDisplay);
        }

        /// <summary>
        /// Sent when one or more fingers move on the screen.
        /// </summary>
        /// <param name="touches">Set containing the touches as objects of type <see cref="T:UIKit.UITouch" />.</param>
        /// <param name="evt"><para>The UIEvent that encapsulates all of the touches and the event information.</para>
        /// <para tool="nullallowed">This parameter can be <see langword="null" />.</para></param>
        /// <remarks><para>Since iOS 9.0, <see cref="M:UIKit.UIResponder.TouchesMoved(Foundation.NSSet,UIKit.UIEvent)" /> events are raised on supported hardware and configurations for changes in user-applied pressure. The <see cref="P:UIKit.UITouch.Force" /> property of the <see cref="T:UIKit.UITouch" /> object in the <paramref name="touches" /> set argument contains the magnitude of the touch that raised the event. The following example shows a basic use:</para>
        /// <para>
        ///   <code lang="c#"><![CDATA[
        /// if (TraitCollection.ForceTouchCapability == UIForceTouchCapability.Available) {
        /// UITouch t = touches.AnyObject as UITouch;
        /// ForceLabel.Text = "Force: " + t.Force.ToString ();
        /// }
        /// else {
        /// ForceLabel.Text = "Force Not Active";
        /// }]]></code>
        /// </para></remarks>
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            var touch = (UITouch)touches.AnyObject;
            var currentPoint = touch.LocationInView(this);

            if (Math.Abs(currentPoint.X - _previousPoint.X) >= 4 ||
                Math.Abs(currentPoint.Y - _previousPoint.Y) >= 4)
            {
                var newPoint = new CGPoint((currentPoint.X + _previousPoint.X) / 2, (currentPoint.Y + _previousPoint.Y) / 2);

                _currentPath.AddQuadCurveToPoint(newPoint, _previousPoint);
                _previousPoint = currentPoint;
            }
            else
            {
                _currentPath.AddLineTo(currentPoint);
            }

            InvokeOnMainThread(SetNeedsDisplay);
        }

        /// <summary>
        /// Gets the rotate image.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns>UIImage.</returns>
        private static UIImage GetRotateImage(String imagePath)
        {
            UIImage image = UIImage.FromFile(imagePath);
            CGImage imgRef = image.CGImage;

            float width = imgRef.Width;
            float height = imgRef.Height;

            CGAffineTransform transform = CGAffineTransform.MakeIdentity();
            RectangleF bounds = new RectangleF(0, 0, 200, 200);
            SizeF imageSize = new SizeF(200, 200);
            float boundHeight;
            UIImageOrientation orient = image.Orientation;
            switch (orient)
            {
            case UIImageOrientation.Up:
                transform = CGAffineTransform.MakeIdentity();
                break;

            case UIImageOrientation.UpMirrored:
                transform = CGAffineTransform.MakeTranslation(imageSize.Width, 0.0f);
                transform.Scale(-1.0f, 1.0f);
                break;

            case UIImageOrientation.Down:
                transform.Rotate((float)Math.PI);
                transform.Translate(imageSize.Width, imageSize.Height);
                break;

            case UIImageOrientation.DownMirrored:
                transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Height);
                transform.Scale(1.0f, -1.0f);
                break;

            case UIImageOrientation.LeftMirrored:
                boundHeight = bounds.Size.Height;
                bounds.Height = bounds.Size.Width;
                bounds.Width = boundHeight;
                transform.Scale(-1.0f, 1.0f);
                transform.Rotate((float)Math.PI / 2.0f);
                break;

            case UIImageOrientation.Left:
                boundHeight = bounds.Size.Height;
                bounds.Height = bounds.Size.Width;
                bounds.Width = boundHeight;
                transform = CGAffineTransform.MakeRotation((float)Math.PI / 2.0f);
                transform.Translate(imageSize.Height, 0.0f);
                break;

            case UIImageOrientation.RightMirrored:
                boundHeight = bounds.Size.Height;
                bounds.Height = bounds.Size.Width;
                bounds.Width = boundHeight;
                transform = CGAffineTransform.MakeTranslation(imageSize.Height, imageSize.Width);
                transform.Scale(-1.0f, 1.0f);
                transform.Rotate(3.0f * (float)Math.PI / 2.0f);
                break;

            case UIImageOrientation.Right:
                boundHeight = bounds.Size.Height;
                bounds.Height = bounds.Size.Width;
                bounds.Width = boundHeight;
                transform = CGAffineTransform.MakeRotation(-(float)Math.PI / 2.0f);
                transform.Translate(0.0f, imageSize.Width);
                break;
            }

            UIGraphics.BeginImageContext(bounds.Size);

            CGContext context = UIGraphics.GetCurrentContext();

            context.ConcatCTM(transform);

            context = UIGraphics.GetCurrentContext();
            context.DrawImage(new RectangleF(0, 0, width, height), imgRef);
            UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return imageCopy;
        }
    }
}