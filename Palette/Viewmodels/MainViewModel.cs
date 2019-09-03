using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Palette.Viewmodels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private String path;

        private String _info;
        public String Info {
            get
            {
                if (ImageInfo != null)
                    return ImageInfo.Path + " " + ImageInfo.Width + "x" + ImageInfo.Height;
                return "Picture Not Selected";
            }
            set { OnPropertyChanged(nameof(Info)); }
        }

        private Models.ImageInfo ImageInfo;

        private List<Color> col;
        public List<Color> Colors {
            get { return col; }
            set {
                col = value;
                OnPropertyChanged(nameof(Colors));
            }
        }
        

        public ICommand OpenImage
        {
            get
            {
                return new Models.DelegateCommand((obj) =>
                {
                    String Image_Path = Models.FileHandler.OpenFile();
                    if (Image_Path != "")
                    {
                        Task.Factory.StartNew(() =>
                        {
                            ImageInfo = new Models.ImageInfo(Image_Path);
                            Info += "";
                            Colors = ImageInfo.GetPrimaryColors(5);
                        });
                    }
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]String info = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
