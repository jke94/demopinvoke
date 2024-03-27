namespace pinvoke.wpfuiapp.ViewModel
{
    #region using

    using pinvoke.wpfuiapp.Services;
    using System.ComponentModel;

    #endregion

    public interface IMainViewModel
    {
        #region Properties

        public string Title { get; set; }

        #endregion
    }

    public class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        #region Private Fields

        private string _title;
        private readonly IMainService _mainService;

        #endregion

        public MainViewModel(IMainService mainService)
        {
            _mainService = mainService;
            _title = "Hello world! I am Javi!";
        }

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                OnPropertyChanged(nameof(Title));
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

        #endregion
    }
}
