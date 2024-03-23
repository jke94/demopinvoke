namespace pinvoke.consoleapp.Native
{
    using System.Reflection.Metadata;
    #region using

    using System.Runtime.InteropServices;
    using Microsoft.Extensions.Logging;

    #endregion

    class NativeWrapper : INativeWrapper, IDisposable
    {
        #region Fields

        private ILogger<NativeWrapper> _logger;

        private readonly IntPtr _native_library = default!;

        private bool _disposed;

        private readonly SetUpLogCallback _setUpLogCallback = default!;

        private readonly DisposeLogCallback _disposeLogcallback = default!;

        private readonly CreatePerson _create_person = default!;

        private readonly ConfigPerson _config_person = default!;

        private readonly GetPersonInfo _get_person_info = default!;

        private readonly DestroyPerson _destroy_person = default!;

        #endregion

        #region Delegates

        private delegate void LogCallback(IntPtr message);

        private delegate void SetUpLogCallback([MarshalAs(UnmanagedType.FunctionPtr)] LogCallback logCallback);

        private delegate void DisposeLogCallback();

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

                // Native logging
                _setUpLogCallback = GetDelegateForNativeFunction<SetUpLogCallback>("setUpLogCallback");
                _disposeLogcallback = GetDelegateForNativeFunction<DisposeLogCallback>("disposeLogCallback");

                // Operations over objects.
                _create_person = GetDelegateForNativeFunction<CreatePerson>("createPerson");
                _config_person = GetDelegateForNativeFunction<ConfigPerson>("configPerson");
                _get_person_info = GetDelegateForNativeFunction<GetPersonInfo>("getPersonInfo");
                _destroy_person = GetDelegateForNativeFunction<DestroyPerson>("destroyPerson");

                // SetUp native logger.
                _setUpLogCallback(log_function);
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

        public void log_function(IntPtr message)
        {
            _logger.LogInformation($"[NATIVE_CODE] {Marshal.PtrToStringAnsi(message)}");
        }

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

        #region Dispose Methods

        public void Dispose()
        {
            // Disose native logger.
            _disposeLogcallback();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                NativeLibrary.Free(_native_library);
            }
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
