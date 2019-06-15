using System;
using System.IO;
using FormsPoc.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosSign))]
namespace FormsPoc.iOS
{
    public class IosSign : ISign
    {
            public string Sign()
            {
                string savedFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/temp_" + DateTime.Now.ToString("yyyy_mm_dd_hh_mm_ss") + ".jpg";
                return savedFileName;
            }

            public bool IsFileExists(string path)
            {
                return File.Exists(path);
            }
        }
}
