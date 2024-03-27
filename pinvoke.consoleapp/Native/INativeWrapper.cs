namespace pinvoke.consoleapp.Native
{
    public interface INativeWrapper
    {
        #region Methods

        public IntPtr create_person();

        public void config_person(IntPtr person, ref StructBox.PersonInfo person_info);

        public void get_person_info(IntPtr person, ref StructBox.PersonInfo person_info);

        public void destroy_person(IntPtr person);

        #endregion
    }
}
