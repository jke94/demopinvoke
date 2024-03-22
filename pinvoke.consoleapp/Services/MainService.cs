namespace pinvoke.consoleapp.Services
{
    #region using

    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using pinvoke.consoleapp.Native;

    #endregion

    public class MainService : IMainService
    {
        #region Fiedls

        private ILogger<MainService> _logger;
        private INativeWrapper _nativeWrapper;

        #endregion

        #region Constructor

        public MainService(
            ILogger<MainService> logger, 
            INativeWrapper nativeWrapper)
        {
            _logger = logger;
            _nativeWrapper = nativeWrapper;
        }

        #endregion

        #region Methods

        public void Run()
        {
            try
            {
                IntPtr person = _nativeWrapper.create_person();

                StructBox.ConfigPerson config_person = new StructBox.ConfigPerson
                {
                    id = 19941994,
                    age = 29,
                    // Convertir la cadena a un puntero a caracteres
                    name = Marshal.StringToHGlobalAnsi("Javi")
                };

                _nativeWrapper.config_person(person, ref config_person);

                IntPtr show_person_info = _nativeWrapper.get_person_info(person);

                StructBox.ConfigPerson person_info = Marshal.PtrToStructure<StructBox.ConfigPerson>(show_person_info);

                // Imprime la información de la persona
                _logger.LogInformation($"ID: {person_info.id}");
                _logger.LogInformation($"Age: {person_info.age}");
                _logger.LogInformation($"Name: {Marshal.PtrToStringAnsi(person_info.name)}");

                // Libera la memoria asignada
                Marshal.FreeHGlobal(config_person.name);
                _nativeWrapper.get_person_info(person);
            }
            catch (DllNotFoundException e)
            {
                _logger.LogError($"uuuuuups!: {e}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e}");
            }
        }

        #endregion
    }
}