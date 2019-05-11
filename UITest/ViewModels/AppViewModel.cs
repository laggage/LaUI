using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;
using UITest.Models;

namespace UITest.ViewModels
{
    class AppViewModel:NotificationObject
    {
        public AppViewModel()
        {
            _tip = "SelfDefineTest";
            if(Images == null)
                Images = new List<Image>()
                {
                    new Image(@"E:\Media\Images\壁纸\ (1).jpg"),
                    new Image(@"E:\Media\Images\壁纸\ (40).jpg"),
                    new Image(@"E:\Media\Images\壁纸\ (41).jpg")
                };
        }


        private List<Image> _images;

        public List<Image> Images
        {
            get=> _images;
            set
            {
                if (Equals(value, _images))
                    return;
                RaisePropertyChanged(nameof(Images));
                _images = value;
            }
        }

        private string _tip;

        public string Tip
        {
            get => _tip;
            set
            {
                if (_tip == value)
                    return;
                RaisePropertyChanged(nameof(Tip));
                _tip = value;
            }
        }
    }
}
