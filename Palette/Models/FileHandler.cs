using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palette.Models
{
    static class FileHandler
    {
        public static String OpenFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            String si = "";
            foreach (ImageInfo.Formats x in Enum.GetValues(typeof(ImageInfo.Formats)))
                si += "*." + x + ";";
            ofd.Filter = "Images|" + si;

            if (ofd.ShowDialog() == true) return ofd.FileName;
            return "";
        }

        public static String SaveFile()
        {

            return "";
        }
    }
}
