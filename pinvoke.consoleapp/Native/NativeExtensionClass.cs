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
            var struct_person = Marshal.PtrToStructure<StructBox.PersonInfo>(p);
            string name = Marshal.PtrToStringAnsi(struct_person.name) ?? string.Empty;

            var person = new Person(struct_person.id, struct_person.age, name);

            return person;
        }

        public static IPerson ToPerson(this StructBox.PersonInfo person_info)
        {
            string name = Marshal.PtrToStringAnsi(person_info.name) ?? string.Empty;

            var person = new Person(person_info.id, person_info.age, name);

            return person;
        }
    }
}
