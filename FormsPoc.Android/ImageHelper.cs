using System;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.Provider;

namespace myApp.Droid
{
    public class ImageHelper
    {
        public static byte[] BitmapToBytes(Android.Graphics.Bitmap bitmap)
        {
            byte[] data = new byte[0];
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 90, stream);
                stream.Close();
                data = stream.ToArray();
            }
            return data;
        }

        public static int CalculateInSampleSize(Android.Graphics.BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            var height = (float)options.OutHeight;
            var width = (float)options.OutWidth;
            var inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                inSampleSize = width > height
                ? height / reqHeight
                : width / reqWidth;
            }

            return (int)inSampleSize;
        }

        public static Android.Graphics.Bitmap DecodeSampledBitmapFromResource(String filename, int reqWidth, int reqHeight)
        {
            // First decode with inJustDecodeBounds=true to check dimensions
            var options = new Android.Graphics.BitmapFactory.Options
            {
                InJustDecodeBounds = true,
            };
            using (var dispose = Android.Graphics.BitmapFactory.DecodeFile(filename, options))
            {
            }

            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;
            return Android.Graphics.BitmapFactory.DecodeFile(filename, options);
        }

        /**
		* Rotate an image if required.
		* @param img
		* @param selectedImage
		* @return 
*/
        public static Bitmap rotateImageIfRequired(Android.Content.Context context, Bitmap img, Uri selectedImage)
        {

            // Detect rotation
            int rotation = getRotation(context, selectedImage);
            if (rotation != 0)
            {
                Matrix matrix = new Matrix();
                matrix.PostRotate(rotation);
                Bitmap rotatedImg = Bitmap.CreateBitmap(img, 0, 0, img.Width, img.Height, matrix, true);
                img.Recycle();
                return rotatedImg;
            }
            else
            {
                return img;
            }
        }

        /**
		* Get the rotation of the last image added.
		* @param context
		* @param selectedImage
		* @return
*/
        private static int getRotation(Android.Content.Context context, Uri selectedImage)
        {
            int rotation = 0;
            ContentResolver content = context.ContentResolver;


            ICursor mediaCursor = content.Query(MediaStore.Images.Media.ExternalContentUri,
            new String[] { "orientation", "date_added" }, null, null, "date_added desc");

            if (mediaCursor != null && mediaCursor.Count != 0)
            {
                while (mediaCursor.MoveToNext())
                {
                    rotation = mediaCursor.GetInt(0);
                    break;
                }
            }
            mediaCursor.Close();
            return rotation;
        }
    }
}