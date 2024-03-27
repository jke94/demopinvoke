namespace pinvoke.wpfuiapp.ViewModel
{
    using pinvoke.nativewrapperlibrary.Native;
    #region using

    using pinvoke.wpfuiapp.Services;
    using System.ComponentModel;

    #endregion

    public interface IMainViewModel
    {
        #region Properties

        public string Title { get; set; }

        public string PPM { get; set; }

        #endregion
    }

    public class MainViewModel : IMainViewModel, IDisposable, INotifyPropertyChanged
    {
        #region Private Fields

        private string _title;
        private string _ppm;
        private readonly IMainService _mainService;
        private readonly INativeWrapper _nativeWrapper;

        #endregion

        public MainViewModel(
            IMainService mainService,
            INativeWrapper nativeWrapper
            )
        {
            _mainService = mainService;
            _nativeWrapper = nativeWrapper;
            _title = "Hello world! I am Javi!";
            _ppm = "-10";
        }

        public string Title
        {
            get 
            { 
                return _title; 
            }
            set 
            { 
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string PPM
        {
            get
            {
                return _ppm;
            }
            set
            {
                _ppm = value;
                OnPropertyChanged(nameof(PPM));
            }
        }
        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Methods

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
