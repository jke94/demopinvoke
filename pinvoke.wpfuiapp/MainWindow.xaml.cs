namespace pinvoke.wpfuiapp
{
    #region using

    using System;
    using System.Windows;

    #endregion

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (DataContext is IDisposable viewmodel)
            {
                viewmodel.Dispose();
            }
            base.OnClosed(e);
        }
    }
}