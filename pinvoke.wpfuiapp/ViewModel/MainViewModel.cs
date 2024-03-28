namespace pinvoke.wpfuiapp.ViewModel
{
    #region using

    using pinvoke.nativewrapperlibrary.Native;
    using pinvoke.wpfuiapp.Services;
    using System.ComponentModel;
    using System;
    using Microsoft.Extensions.Logging;

    #endregion

    public interface IMainViewModel
    {
        #region Properties

        public string Name { get; set; }

        public string PPM { get; set; }

        #endregion
    }

    public class MainViewModel : IMainViewModel, IDisposable, INotifyPropertyChanged
    {
        #region Private Fields

        private string _name = default!;
        private string _ppm = default!;
        private readonly IMainService _mainService = default!;
        private readonly INativeWrapper _nativeWrapper = default!;
        private readonly ILogger<MainViewModel> _logger = default!;
        private IntPtr _native_person = default!;
        bool is_disposed = false;

        private NativeDelegates.PersonMonitorCallback personMonitorCallback = default!;

        #endregion

        #region Constructor

        public MainViewModel(
            IMainService mainService,
            INativeWrapper nativeWrapper,
            ILogger<MainViewModel> logger
            )
        {
            _mainService = mainService;
            _nativeWrapper = nativeWrapper;
            _logger = logger;
            _name = "Hello world! I am Javi!";
            _ppm = "-10";

            _native_person = _nativeWrapper.create_person();

            personMonitorCallback = update_ppm;
            _nativeWrapper.setPersonMonitor(_native_person, personMonitorCallback);
        }

        #endregion

        #region Properties

        public string Name
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                _name = value;
                OnPropertyChanged(nameof(Name));
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

        #endregion

        #region Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!is_disposed) // only dispose once!
            {
                _nativeWrapper.destroy_person(_native_person);
            }

            is_disposed = true;
        }

        #endregion

        #region Private Methods

        public void update_ppm(IntPtr name, int ppm)
        {
            PPM = ppm.ToString();
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion
    }
}
