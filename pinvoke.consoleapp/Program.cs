namespace pinvoke.consoleapp
{
    #region using

    using System.Runtime.InteropServices;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using pinvoke.consoleapp.Native;

    #endregion

    public class StructsBox
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ConfigPerson
        {
            public int id;
            public int age;
            public IntPtr name; // IntPtr para el puntero char*
        }
    }

    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<INativeWrapper, NativeWrapper>();
            });

        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var native_wrapper = host.Services.GetRequiredService<INativeWrapper>();

            try
            {
                IntPtr person = native_wrapper.create_person();

                StructsBox.ConfigPerson config_person = new StructsBox.ConfigPerson
                {
                    id = 19941994,
                    age = 29,
                    // Convertir la cadena a un puntero a caracteres
                    name = Marshal.StringToHGlobalAnsi("Javi")
                };

                native_wrapper.config_person(person, ref config_person);

                IntPtr show_person_info = native_wrapper.get_person_info(person);

                StructsBox.ConfigPerson person_info = Marshal.PtrToStructure<StructsBox.ConfigPerson>(show_person_info);

                // Imprime la información de la persona
                Console.WriteLine($"ID: {person_info.id}");
                Console.WriteLine($"Age: {person_info.age}");
                Console.WriteLine($"Name: {Marshal.PtrToStringAnsi(person_info.name)}");

                // Libera la memoria asignada
                Marshal.FreeHGlobal(config_person.name);
                native_wrapper.get_person_info(person);
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