using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DColor = System.Drawing.Color;
using MColor = System.Windows.Media.Color;

namespace Palette.Models
{
    class ColorTheory
    {
        //Draw ColorWheel: Gu.Wpf.Geometry

        private const int COLOR_OFFSET = 15;
        private const int SECTION = 30;

        public List<DColor> GetHarmony(DColor initial, Harmonies harmony) //TODO: function for returning sectors
        {
            List<DColor> l = new List<DColor>();
            l.Add(initial);

            switch (harmony)
            {
                case Harmonies.Monochromatic:
                    Random r = new Random();
                    l.Add(HSV2RGB(initial.GetHue(), r.NextDouble(), r.NextDouble()));
                    break;
                case Harmonies.Analog:
                    l.Add(HSV2RGB(initial.GetHue() - SECTION, initial.GetSaturation(), initial.GetBrightness()));
                    l.Add(HSV2RGB(initial.GetHue() + SECTION, initial.GetSaturation(), initial.GetBrightness()));
                    break;
                case Harmonies.Complementary:
                    l.Add(HSV2RGB(initial.GetHue() + 180, initial.GetSaturation(), initial.GetBrightness()));
                    break;
                case Harmonies.SplitComplementary:
                    l.Add(HSV2RGB(initial.GetHue() + 180 - SECTION, initial.GetSaturation(), initial.GetBrightness()));
                    l.Add(HSV2RGB(initial.GetHue() + 180 + SECTION, initial.GetSaturation(), initial.GetBrightness()));
                    break;
                case Harmonies.Tetradic:
                    l.Add(HSV2RGB(initial.GetHue() + 90, initial.GetSaturation(), initial.GetBrightness()));
                    l.Add(HSV2RGB(initial.GetHue() + 180, initial.GetSaturation(), initial.GetBrightness()));
                    l.Add(HSV2RGB(initial.GetHue() + 270, initial.GetSaturation(), initial.GetBrightness()));
                    break;
                case Harmonies.Triad:
                    l.Add(HSV2RGB(initial.GetHue() + 120, initial.GetSaturation(), initial.GetBrightness()));
                    l.Add(HSV2RGB(initial.GetHue() + 240, initial.GetSaturation(), initial.GetBrightness()));
                    break;
            }

            return l;
        }

        public enum Harmonies
        {
            Monochromatic, Analog, Complementary, SplitComplementary, Tetradic, Triad
        }

        private DColor HSV2RGB(double h, double S, double V)
        {
            double H = h;
            while (H < 0) H += 360;
            while (H > 360) H -= 360;

            double R, G, B;
            if (V <= 0) R = G = B = 0;
            else if (S <= 0) R = G = B = V;
            else
            {
                double hf = H / SECTION;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));

                switch (i)
                {
                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case 1:
                        R = qv;
                        G = V;
                        B = tv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;
                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;
                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    default:
                        R = G = B = V;
                        break;
                }
            }
            int r = (int)R * 255, g = (int)G * 255, b = (int)B * 255;
            return DColor.FromArgb(r, g, b);
        }
    }
}
