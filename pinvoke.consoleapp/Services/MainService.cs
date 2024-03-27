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
                IntPtr native_person = _nativeWrapper.create_person();

                StructBox.ConfigPerson config_person = new StructBox.ConfigPerson
                {
                    id = 19941994,
                    age = 29,
                    // Convertir la cadena a un puntero a caracteres
                    name = Marshal.StringToHGlobalAnsi("Javi")
                };

                _nativeWrapper.config_person(native_person, ref config_person);

                StructBox.ConfigPerson person_info = new();

                _nativeWrapper.get_person_info(native_person, ref person_info);

                var person = person_info.ToPerson();

                // Imprime la información de la persona
                _logger.LogInformation($"ID: {person.Id}");
                _logger.LogInformation($"Age: {person.Age}");
                _logger.LogInformation($"Name: {person.Name}");

                // Libera la memoria asignada
                Marshal.FreeHGlobal(config_person.name);

                _nativeWrapper.destroy_person(native_person);
            }
            catch (DllNotFoundException e)
            {
                _logger.LogError($"Uuuuuups (DllNotFoundException)!: {e}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e}");
            }
        }

        #endregion
    }
}