namespace pinvoke.consoleapp
{
    #region using

    using System.Runtime.InteropServices;
    using System.IO;

    #endregion

    class LibraryPath
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ConfigPerson
        {
            public int id;
            public int age;
            public IntPtr name; // IntPtr para el puntero char*
        }

        #if DEBUG
            [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        #else
            [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Release\\pinvoke.library.managed.dll")]
        #endif
        public static extern IntPtr createPerson();

        #if DEBUG
            [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        #else
                [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Release\\pinvoke.library.managed.dll")]
        #endif
        public static extern void configPerson(IntPtr person, ref ConfigPerson config_person);

        #if DEBUG
            [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        #else
            [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Release\\pinvoke.library.managed.dll")]
                
        #endif
        public static extern IntPtr getPersonInfo(IntPtr person);

        #if DEBUG
        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        #else
            [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Release\\pinvoke.library.managed.dll")]
        #endif
        public static extern void destroyPerson(IntPtr person);
    }


    class Program
    {
        static void Main()
        {
            IntPtr person = LibraryPath.createPerson();

            LibraryPath.ConfigPerson config_person = new LibraryPath.ConfigPerson
            {
                id = 19941994,
                age = 30,
                // Convertir la cadena a un puntero a caracteres
                name = Marshal.StringToHGlobalAnsi("Javi") 
            };

            LibraryPath.configPerson(person, ref config_person);

            IntPtr show_person_info = LibraryPath.getPersonInfo(person);

            LibraryPath.ConfigPerson person_info = Marshal.PtrToStructure<LibraryPath.ConfigPerson>(show_person_info);

            // Imprime la información de la persona
            Console.WriteLine($"ID: {person_info.id}");
            Console.WriteLine($"Age: {person_info.age}");
            Console.WriteLine($"Name: {Marshal.PtrToStringAnsi(person_info.name)}");

            // Libera la memoria asignada
            Marshal.FreeHGlobal(config_person.name);
            LibraryPath.destroyPerson(person);
        }
    }
}