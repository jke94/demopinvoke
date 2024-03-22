namespace pinvoke.consoleapp.Native
{
    public interface INativeWrapper
    {
        #region Methods

        public IntPtr create_person();

        public void config_person(IntPtr person, ref StructBox.ConfigPerson config_person);

        public IntPtr get_person_info(IntPtr person);

        public void destroy_person(IntPtr person);

        #endregion
    }
}
