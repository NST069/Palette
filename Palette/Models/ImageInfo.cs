using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DColor = System.Drawing.Color;
using MColor = System.Windows.Media.Color;

namespace Palette.Models
{
    class ImageInfo
    {
        public bool isValid;
        public String Path { get; set; }
        private BitmapImage image;
        public int Height;
        public int Width;

        public ImageInfo(String path)
        {
            isValid = false;
            this.Path = path;
            image = new BitmapImage(new Uri(path));
            Height = image.PixelHeight;
            Width = image.PixelWidth;

            isValid = true;
        }


        public List<MColor> GetPrimaryColors(int top) {
            List<MColor> l = new List<MColor>();
            Dictionary<DColor, int> colors = new Dictionary<DColor, int>();

            var bm = BitmapImage2Bitmap(image);
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    DColor c = bm.GetPixel(x, y);

                    if (colors.ContainsKey(c)) colors[c]++;
                    else colors.Add(c, 0);
                }
            }

            foreach (var item in colors.OrderByDescending(key => key.Value)) {
                if (l.Count < top)
                {
                    MColor col = MColor.FromArgb(item.Key.A, item.Key.R, item.Key.G, item.Key.B);
                    l.Add(col);
                }
            }

            return l;
        }

        private static Bitmap BitmapImage2Bitmap(BitmapSource bitmapImage) {
            using (var outstream = new System.IO.MemoryStream()) {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outstream);
                return new Bitmap(outstream);
            }
        }

        public enum Formats
        {
            png, jpg
        }
    }
}
