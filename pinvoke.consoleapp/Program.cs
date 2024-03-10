namespace pinvoke.consoleapp
{
    #region using

    using System.Runtime.InteropServices;

    #endregion

    [StructLayout(LayoutKind.Sequential)]
    public struct ConfigPerson
    {
        public int id;
        public int age;
        public IntPtr name; // IntPtr para el puntero char*
    }

    class LibraryPath
    {
        /**
         *  UPDATE!!!! Before to run, update with absolute paths:
         *      
         *      - Example: 
         *          NativeLibrayDebugAbsPath:       "C:\\Users\\the_name_of_user\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll"
         *          NativeLibrayReleaseAbsPath:     "C:\\Users\\the_name_of_user\\demopinvoke\\x64\\Release\\pinvoke.library.managed.dll"
         */

        public const string NativeLibrayDebugAbsPath = "C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll";
        public const string NativeLibrayReleaseAbsPath = "C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Release\\pinvoke.library.managed.dll";
        
        #if DEBUG
            [DllImport(NativeLibrayDebugAbsPath)]
        #else
            [DllImport(NativeLibrayReleaseAbsPath)]
        #endif
        public static extern IntPtr createPerson();

        #if DEBUG
            [DllImport(NativeLibrayDebugAbsPath)]
        #else
            [DllImport(NativeLibrayReleaseAbsPath)]
        #endif
        public static extern void configPerson(IntPtr person, ref ConfigPerson config_person);

        #if DEBUG
            [DllImport(NativeLibrayDebugAbsPath)]
        #else
            [DllImport(NativeLibrayReleaseAbsPath)]
        #endif
        public static extern IntPtr getPersonInfo(IntPtr person);

        #if DEBUG
            [DllImport(NativeLibrayDebugAbsPath)]
        #else
            [DllImport(NativeLibrayReleaseAbsPath)]
        #endif
        public static extern void destroyPerson(IntPtr person);
    }

    class Program
    {
        static void Main()
        {
            try
            {
                IntPtr person = LibraryPath.createPerson();

                ConfigPerson config_person = new ConfigPerson
                {
                    id = 19941994,
                    age = 29,
                    // Convertir la cadena a un puntero a caracteres
                    name = Marshal.StringToHGlobalAnsi("Javi")
                };

                LibraryPath.configPerson(person, ref config_person);

                IntPtr show_person_info = LibraryPath.getPersonInfo(person);

                ConfigPerson person_info = Marshal.PtrToStructure<ConfigPerson>(show_person_info);

                // Imprime la información de la persona
                Console.WriteLine($"ID: {person_info.id}");
                Console.WriteLine($"Age: {person_info.age}");
                Console.WriteLine($"Name: {Marshal.PtrToStringAnsi(person_info.name)}");

                // Libera la memoria asignada
                Marshal.FreeHGlobal(config_person.name);
                LibraryPath.destroyPerson(person);
            }
            catch (DllNotFoundException e)
            {
                Console.WriteLine($"uuuuuups!: {e}");
            }
            catch( Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }
        }
    }
}