namespace pinvoke.consoleapp.Native
{
    #region using

    using System.Runtime.InteropServices;
    using pinvoke.consoleapp.Model;

    #endregion

    public static class NativeExtensionClass
    {
        public static IPerson ToStructConfigPerson(this IntPtr p)
        {
            var struct_person = Marshal.PtrToStructure<StructBox.ConfigPerson>(p);
            string name = Marshal.PtrToStringAnsi(struct_person.name) ?? string.Empty;

            var person = new Person(struct_person.id, struct_person.age, name);

            return person;
        }

        public static IPerson ToPerson(this StructBox.ConfigPerson config_person)
        {
            string name = Marshal.PtrToStringAnsi(config_person.name) ?? string.Empty;

            var person = new Person(config_person.id, config_person.age, name);

            return person;
        }
    }
}
