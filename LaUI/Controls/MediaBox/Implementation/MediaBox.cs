using Emgu.CV;
using MediaProcess.Model;
using MediaProcess.Service;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XamlAnimatedGif;

namespace LaUI.Controls
{
    /********************************************************************************

    ** 类名称： NewMsgBox

    ** 描述：自定义多媒体显示控件,能够显示图片和视频(使用emgucv,暂时无法播放声音,后期会考虑使用vlc)

    ** 作者： Laggage

    ** 创建时间：2018 12/24

    ** 最后修改人：Laggage

    ** 最后修改时间：Laggage

    ** 版权所有 (C) :Laggage

    *********************************************************************************/

    public class MediaBox:System.Windows.Controls.Control,IDisposable
    {
        #region Fields

        private Image _image = null;
        private Capture _capture = null;
        private VideoInfo? _videoInfo = null;
        private MediaType _mediaType;

        #endregion

        #region DependencyProperty

        #region Url
        //media source
        public string Url
        {
            get => (string)GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        // Using a DependencyProperty as the backing store for Url.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register(nameof(Url), typeof(string), typeof(MediaBox),
                new PropertyMetadata(OnSourceUrlChanged));

        private static void OnSourceUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MediaBox mediaBox = d as MediaBox;

            if (mediaBox == null) return;
            if (mediaBox._image != null) mediaBox._image.Source = null;
            mediaBox._mediaType = MediaService.GetMediaTypeByUrl((string)e.NewValue);
            mediaBox.ChangeVisualState(false);
            switch (mediaBox._mediaType)
            {
                case MediaType.Video:
                    mediaBox.VideoPlay = mediaBox.VideoAutoPlay;
                    mediaBox._image?.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        mediaBox._image.Source = mediaBox.GetFirstFrame();
                    }));
                    mediaBox.PlayVideo();
                    break;
                case MediaType.Image:
                    mediaBox.VideoPlay = false;
                    mediaBox._capture?.Dispose(); //如果Url从视频变为图片,则需要释放_capture资源
                    mediaBox._capture = null;
                    mediaBox._videoInfo = null;
                    mediaBox.ShowImage();
                    break;
            }
        }

        #endregion

        #region VideoPlay

        public bool VideoPlay
        {
            get => (bool)GetValue(VideoPlayProperty);
            set => SetValue(VideoPlayProperty, value);
        }

        // Using a DependencyProperty as the backing store for VideoPlay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoPlayProperty =
            DependencyProperty.Register(nameof(VideoPlay), typeof(bool), typeof(MediaBox),
                new PropertyMetadata(false, OnVideoStateChanged));

        private static void OnVideoStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaBox = d as MediaBox;
            mediaBox?.PlayVideo();
        }

        #endregion

        #region VideoAutoPlay

        public bool VideoAutoPlay
        {
            get => (bool)GetValue(VideoAutoPlayProperty);
            set => SetValue(VideoAutoPlayProperty, value);
        }

        // Using a DependencyProperty as the backing store for VideoAutoPlay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoAutoPlayProperty =
            DependencyProperty.Register(nameof(VideoAutoPlay), typeof(bool), typeof(MediaBox), new PropertyMetadata(false));

        #endregion

        #region Stretch

        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(MediaBox), new PropertyMetadata(Stretch.None));

        #endregion

        #endregion

        #region Property

        public new double ActualWidth => _image.ActualWidth;
        public new double ActualHeight => _image.ActualHeight;

        #endregion

        #region Constructor

        static MediaBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaBox),
                new FrameworkPropertyMetadata(typeof(MediaBox)));
        }

        #endregion

        #region EventHandler

        private void _capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                //play again whenever video has over
                if (_videoInfo?.FrameCount - 1 == (int)_capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames))
                    _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, 0);
                _capture.Retrieve(_videoInfo?.CurrentFrame);

                _image?.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (_videoInfo == null)
                        return;
                    _image.Source = BitmapSourceConvert.ToBitmapSource(_videoInfo?.CurrentFrame);
                }));

                if (_videoInfo?.IntervalSeconds != null)
                    Thread.Sleep((int)(_videoInfo?.IntervalSeconds * 1000));
            }
            catch
            {
                // ignored
            }
        }

        #endregion

        #region Method

        #region private

        private void PlayVideo()
        {
            ChangeVisualState(false);
            if (!VideoPlay)
            {
                _capture?.Pause();
                return;
            }

            if (_capture == null)
                _capture = new Capture(Url);

            _videoInfo = new VideoInfo()
            {
                CurrentFrame = new Mat(),
                IntervalSeconds = 1.0 / _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps),
                FrameCount = (int)_capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount)
            };

            _capture.Start();
            _capture.ImageGrabbed -= _capture_ImageGrabbed;
            _capture.ImageGrabbed += _capture_ImageGrabbed;
        }

        private void PlayGif()
        {
            _image?.Dispatcher?.BeginInvoke(new Action(() =>
            {
                AnimationBehavior.SetSourceUri(_image, new Uri(Url, UriKind.RelativeOrAbsolute));
            }));
        }

        private void ShowImage()
        {
            ImageType type = ImageService.GetImageType(Url);
            if (type == MediaProcess.Model.ImageType.Gif)
                PlayGif();
            else
                _image?.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var source = new BitmapImage();
                    source.BeginInit();
                    source.UriSource = new Uri(Url, UriKind.RelativeOrAbsolute);
                    source.EndInit();
                    _image.Source = source;
                }));
        }

        private void ChangeVisualState(bool useTransition)
        {
            VisualStateManager.GoToState(this, _mediaType == MediaType.Video ? "Video" : "Image", useTransition);
            VisualStateManager.GoToState(this, VideoPlay ? "Play" : "Stop", useTransition);
        }

        private BitmapSource GetFirstFrame()
        {
            using (Capture cp = new Capture(Url))
            {
                return BitmapSourceConvert.ToBitmapSource(cp.QueryFrame());
            }
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            ChangeVisualState(false);

            _image = (Image)GetTemplateChild("FrameDisplay");
            if (_image != null)
            {
                Binding bdStretch = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(StretchProperty),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                _image.SetBinding(Image.StretchProperty, bdStretch);
            }

            var videoCtrlBtn = (System.Windows.Controls.Primitives.ToggleButton)GetTemplateChild("VideoControlButton");
            if (videoCtrlBtn != null)
            {
                Binding bd = new Binding
                {
                    Source = this,
                    Path = new PropertyPath(VideoPlayProperty),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                videoCtrlBtn.SetBinding(ToggleButton.IsCheckedProperty, bd);
            }

            OnSourceUrlChanged(this,
                new DependencyPropertyChangedEventArgs(UrlProperty, Url, Url));
        }

        #endregion

        #region Implementation

        public void Dispose()
        {
            _capture?.Dispose();
        }

        #endregion

        #endregion
    }

    internal struct VideoInfo : IDisposable
    {
        public Mat CurrentFrame;
        public double IntervalSeconds;
        public int FrameCount;

        public void Dispose()
        {
            CurrentFrame?.Dispose();
        }
    }
}
