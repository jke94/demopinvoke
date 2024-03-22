namespace pinvoke.consoleapp.Native
{
    #region using

    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    #endregion

    class NativeWrapper : INativeWrapper
    {
        #region Fields

        private ILogger<NativeWrapper> _logger;

        private readonly IntPtr _native_library;

        private readonly CreatePerson? _create_person;

        private readonly ConfigPerson? _config_person;

        private readonly GetPersonInfo? _get_person_info;

        private readonly DestroyPerson? _destroy_person;

        #endregion

        #region Delegates

        private delegate IntPtr CreatePerson();

        private delegate IntPtr ConfigPerson(IntPtr person, ref StructBox.ConfigPerson config_person);

        private delegate IntPtr GetPersonInfo(IntPtr person);

        private delegate IntPtr DestroyPerson(IntPtr person);

        #endregion

        #region Constructor

        public NativeWrapper(ILogger<NativeWrapper> logger)
        {
            _logger = logger;

            try
            {
                _native_library = NativeLibrary.Load(
                    "pinvoke.library.managed",
                    typeof(NativeWrapper).Assembly, 
                    DllImportSearchPath.AssemblyDirectory
                    );

                _create_person = GetDelegateForNativeFunction<CreatePerson>("createPerson");
                _config_person = GetDelegateForNativeFunction<ConfigPerson>("configPerson");
                _get_person_info = GetDelegateForNativeFunction<GetPersonInfo>("getPersonInfo");
                _destroy_person = GetDelegateForNativeFunction<DestroyPerson>("destroyPerson");
            }
            catch (DllNotFoundException e)
            {
                _logger.LogError(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        #endregion

        #region Methods

        public IntPtr create_person()
        {
            return _create_person();
        }

        public void config_person(IntPtr person, ref StructBox.ConfigPerson config_person)
        {
            _config_person(person, ref config_person);
        }

        public IntPtr get_person_info(IntPtr person)
        {
            return _get_person_info(person);
        }

        public void destroy_person(IntPtr person)
        {
            _destroy_person(person);
        }

        #endregion

        #region Templates

        private T GetDelegateForNativeFunction<T>(string functionName)
            where T : Delegate
        {
            bool result = NativeLibrary.TryGetExport(_native_library, functionName, out IntPtr function_pointer);

            if (!result)
            {
                throw new EntryPointNotFoundException(functionName);
            }

            return Marshal.GetDelegateForFunctionPointer<T>(function_pointer);
        }

        #endregion
    }
}
