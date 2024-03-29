﻿namespace pinvoke.nativewrapperlibrary.Native
{
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

        private readonly SetPersonMonitor _set_person_monitor = default!;

        private readonly SetPersonInfo _set_person_info = default!;

        private readonly GetPersonInfo _get_person_info = default!;

        private readonly DestroyPerson _destroy_person = default!;

        #endregion

        #region Delegates

        private delegate void LogCallback(IntPtr message);

        // Dynamic library entry points.

        private delegate void SetUpLogCallback([MarshalAs(UnmanagedType.FunctionPtr)] LogCallback logCallback);

        private delegate void DisposeLogCallback();

        private delegate IntPtr CreatePerson();

        private delegate void SetPersonMonitor(IntPtr person, [MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.PersonMonitorCallback personMonitorCallback);

        private delegate void SetPersonInfo(IntPtr person, ref StructBox.PersonInfo person_info);

        private delegate void GetPersonInfo(IntPtr person, ref StructBox.PersonInfo person_info);

        private delegate void DestroyPerson(IntPtr person);

        #endregion

        #region Constructor

        public NativeWrapper(ILogger<NativeWrapper> logger)
        {
            _logger = logger;

            try
            {
                _native_library = NativeLibrary.Load("pinvoke.library.managed",
                    typeof(NativeWrapper).Assembly, 
                    DllImportSearchPath.AssemblyDirectory);

                // Native logging
                _setUpLogCallback = GetDelegateForNativeFunction<SetUpLogCallback>("setUpLogCallback");
                _disposeLogcallback = GetDelegateForNativeFunction<DisposeLogCallback>("disposeLogCallback");

                // Operations over objects.
                _create_person = GetDelegateForNativeFunction<CreatePerson>("createPerson");
                _set_person_monitor = GetDelegateForNativeFunction<SetPersonMonitor>("setPersonMonitor");
                _set_person_info = GetDelegateForNativeFunction<SetPersonInfo>("setPersonInfo");
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
            var person = _create_person();

            _logger.LogInformation($"Created person: {person}");

            return person;
        }

        public void setPersonMonitor(
            IntPtr person, 
            [MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.PersonMonitorCallback personMonitorCallback)
        {
            _logger.LogInformation($"Set person {person} person monitor callback.");
            _set_person_monitor(person, personMonitorCallback);
        }

        public void config_person(IntPtr person, ref StructBox.PersonInfo person_info)
        {
            _logger.LogInformation($"Config person {person} person monitor callback.");
            _set_person_info(person, ref person_info);
        }

        public void get_person_info(IntPtr person, ref StructBox.PersonInfo person_info)
        {
            _logger.LogInformation($"Getting person {person} info: {person_info.GetHashCode()}");
            _get_person_info(person, ref person_info);
        }

        public void destroy_person(IntPtr person)
        {
            _logger.LogInformation($"Destroy person: {person}");
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
