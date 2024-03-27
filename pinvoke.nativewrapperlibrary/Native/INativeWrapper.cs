namespace pinvoke.nativewrapperlibrary.Native
{
    #region using

    using System.Runtime.InteropServices;

    #endregion

    public interface INativeWrapper
    {
        #region Methods

        public IntPtr create_person();

        public void setPersonMonitor(
            IntPtr person,
            [MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.PersonMonitorCallback personMonitorCallback);

        public void config_person(IntPtr person, ref StructBox.PersonInfo person_info);

        public void get_person_info(IntPtr person, ref StructBox.PersonInfo person_info);

        public void destroy_person(IntPtr person);

        #endregion
    }
}
