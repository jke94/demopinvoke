namespace pinvoke.consoleapp
{
    #region using

    using System.Runtime.InteropServices;

    #endregion

    class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ConfigPerson
        {
            public int id;
            public int age;
            public IntPtr name; // IntPtr para el puntero char*
        }

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        public static extern IntPtr createPerson();

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        public static extern void configPerson(IntPtr person, ref ConfigPerson config_person);

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        public static extern IntPtr getPersonInfo(IntPtr person);

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        public static extern void destroyPerson(IntPtr person);

        static void Main()
        {
            IntPtr person = createPerson();

            ConfigPerson config_person = new ConfigPerson
            {
                id = 19941994,
                age = 30,
                // Convertir la cadena a un puntero a caracteres
                name = Marshal.StringToHGlobalAnsi("Javi") 
            };

            configPerson(person, ref config_person);

            IntPtr show_person_info = getPersonInfo(person);

            ConfigPerson person_info = Marshal.PtrToStructure<ConfigPerson>(show_person_info);

            // Imprime la información de la persona
            Console.WriteLine($"ID: {person_info.id}");
            Console.WriteLine($"Age: {person_info.age}");
            Console.WriteLine($"Name: {Marshal.PtrToStringAnsi(person_info.name)}");

            // Libera la memoria asignada
            Marshal.FreeHGlobal(config_person.name);
            destroyPerson(person);
        }
    }
}