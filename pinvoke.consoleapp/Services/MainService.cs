namespace pinvoke.consoleapp.Services
{
    #region using

    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;
    using pinvoke.nativewrapperlibrary.Native;

    #endregion

    public class MainService : IMainService
    {
        #region Fiedls

        private ILogger<MainService> _logger;
        private INativeWrapper _nativeWrapper;
        private int _wait_time_in_milliseconds = 5000;

        private NativeDelegates.PersonMonitorCallback personMonitorCallback = default!;

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

        public void simple_test(IntPtr name, int ppm)
        {
            _logger.LogInformation($"Name: {Marshal.PtrToStringAnsi(name)}, PPM: {ppm}");
        }

        public void Run()
        {
            try
            {
                personMonitorCallback = simple_test;

                IntPtr native_person = _nativeWrapper.create_person();

                _nativeWrapper.setPersonMonitor(native_person, personMonitorCallback);

                var person_info = new StructBox.PersonInfo
                {
                    id = 19941994,
                    age = 29,
                    // Convertir la cadena a un puntero a caracteres
                    name = Marshal.StringToHGlobalAnsi("Javi")
                };

                _nativeWrapper.config_person(native_person, ref person_info);

                _logger.LogInformation($"Waiting {_wait_time_in_milliseconds} ms to display events.");

                Thread.Sleep(_wait_time_in_milliseconds);

                StructBox.PersonInfo person_info_returned = new();

                _nativeWrapper.get_person_info(native_person, ref person_info);

                var person = person_info.ToPerson();

                // Imprime la información de la persona
                _logger.LogInformation($"ID: {person.Id}");
                _logger.LogInformation($"Age: {person.Age}");
                _logger.LogInformation($"Name: {person.Name}");

                // Libera la memoria asignada
                Marshal.FreeHGlobal(person_info_returned.name);

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