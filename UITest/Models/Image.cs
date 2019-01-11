using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace UITest.Models
{
    public class Image
    {
        public Image() { }

        public Image(string imageUrl)
        {
            Url = imageUrl;
        }

        public string Url { get; set; }

        public string Name => File.Exists(Url) ? Path.GetFileName(Url) : "";

        public string NameWithOutExtension => File.Exists(Url) ? Path.GetFileNameWithoutExtension(Url) : "";

        public string FileSize
        {
            get
            {
                string[] sizeUnitTable = { "B", "KB", "MB", "GB", "T" };
                try
                {
                    byte i = 0;
                    FileInfo fileInfo = new FileInfo(Url);
                    long length = fileInfo.Length;
                    while ((length /= 1024) != 0)
                    {
                        i++;
                    }
                    i = i > 4 ? (byte)4 : i;

                    return fileInfo.Length / Math.Pow(1024, i) + sizeUnitTable[i]; ;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public override string ToString()
        {
            return NameWithOutExtension;
        }

        //public override bool Equals(object obj)
        //{
        //    return obj?.GetType() == typeof(Image) &&
        //                  ((Image) obj).Url == this.Url;
        //}

        //public static bool operator == (Image imgLeft,Image imgRight)
        //{
        //    return imgLeft?.Url == imgRight?.Url;
        //}

        //public static bool operator !=(Image imgLeft, Image imgRight)
        //{
        //    return !(imgLeft == imgRight);
        //}
    }
}
