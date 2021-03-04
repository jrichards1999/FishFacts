using JetBrains.Annotations;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FishFacts {
    class MainWindowViewModel : INotifyPropertyChanged {
        public DelegateCommand NewFish { get; set; }
        private MediaPlayer m = new MediaPlayer();

        private string title_;
        public string Title {
            get { return title_; }
            set {
                title_ = value;
                OnPropertyChanged();
            }
        }

        private string description_;
        public string Description {
            get { return description_; }
            set {
                description_ = value;
                OnPropertyChanged();
            }
        }

        private string imageLink_;
        public string ImageLink {
            get { return imageLink_; }
            set {
                imageLink_ = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel() {
            NewFish = new DelegateCommand(OnNewFishClick);

            Title = "No fish here";
            Description = "No fish here either :/";
            ImageLink = "https://i.ytimg.com/vi/4wTffjLr3GU/maxresdefault.jpg";
        }

        private void OnNewFishClick() {
            Fish f = new Fish();
            Title = f.FishName;
            Description = f.FishDesc;
            ImageLink = f.ImageLink;

            var path = Directory.GetCurrentDirectory() + "\\lefishe.mp3";
            m.Open(new Uri(path));
            m.Play();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
