using System;
using System.IO;
using System.Security;

namespace UITest.Models
{
    public class Image
    {
        public Image() { }

        public Image(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                throw new ArgumentNullException(nameof(imageUrl));
            if (!File.Exists(imageUrl))
                throw new FileNotFoundException("文件不存在", imageUrl);

            Url = imageUrl;
        }

        public string Url { get; set; }

        public string Name => Path.GetFileName(Url);

        public string NameWithOutExtension => File.Exists(Url) ? Path.GetFileNameWithoutExtension(Url) : "";

        public string ImageSizeString => GetFileSizeString(Url);

        private static string GetFileSizeString(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                return GetFileSizeString(fileInfo.Length);
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("没有权限读取文件");
            }
            catch (SecurityException)
            {
                throw new SecurityException();
            }
            catch (NotSupportedException)
            {
                throw new NotSupportedException();
            }
        }

        private static string GetFileSizeString(long fileSizeOfByte)
        {
            long length = fileSizeOfByte;
            byte i = 0;
            string[] sizeUnitTable = {"B", "KB", "MB", "GB", "T"};
            while ((length /= 1024) != 0)
                i++;
            i = i >= sizeUnitTable.Length ? (byte)(sizeUnitTable.Length - 1) : i;
            return fileSizeOfByte / Math.Pow(1024, i) + sizeUnitTable[i];
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
