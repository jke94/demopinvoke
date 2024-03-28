# demopinvoke

## A. Summary

Demo about how p/invoke works.

- ✅ Console client app (managed code - C#) consuming native code (C++).
- ✅ Lifecycle (create, edit and destroy) of native object and send it for the native manage code.
- ✅ Managed code editing values for native object.
- ✅🚀 Log based on C++ callback: communication between C++ and C#.
- ✅⭐ Event management from C# using C++ callbacks and established on runtime (C++ source code protection with mutex)

## B. Managed code execution console application example output: **C# code calling C++ library**

Example of simulation the beats per minute of a person. C++ code simulates the beats per minute with a thred that generates random numbers.

```
PS J:\Repositories\demopinvoke\pinvoke.consoleapp\bin\Release\net8.0> .\pinvoke.consoleapp.exe
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][API.cpp:setUpLogCallback:13] Initalize native logger.
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][API.cpp:createPerson:25] Creating person...
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][Person.cpp:Person::Person:39] Called constructor, object: 0000022F800BF150
info: pinvoke.consoleapp.Services.MainService[0]
      Waiting 5000 ms to display events.
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 101
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 135
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 102
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 61
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 111
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 175
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 164
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 136
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi, PPM: 160
info: pinvoke.consoleapp.Services.MainService[0]
      ID: 19941994
info: pinvoke.consoleapp.Services.MainService[0]
      Age: 29
info: pinvoke.consoleapp.Services.MainService[0]
      Name: Javi
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][Person.cpp:Person::~Person:50] Deatached thread: 11112
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][Person.cpp:Person::~Person:59] Deatached thread successfully.
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][Person.cpp:Person::~Person:66] Called destructor, object: 0000022F800BF150
info: pinvoke.consoleapp.Native.NativeWrapper[0]
      [NATIVE_CODE] [INFO][API.cpp:disposeLogCallback:18] Dispose native logger.
PS J:\Repositories\demopinvoke\pinvoke.consoleapp\bin\Release\net8.0>
```

## C. Useful documentation.

## C.1. About P/Invoke and Load Native Libraries.

- [MSDN: Native library loading](https://learn.microsoft.com/en-us/dotnet/standard/native-interop/native-library-loading)
- [Hot Examples: update](https://csharp.hotexamples.com/site/file?hash=0x5ea59faae95926824d1676d7e6534832f9b73f1c41d7e3f2622198711090b595&fullName=dd-trace-dotnet-master/tracer/src/Datadog.Trace/AppSec/Waf/WafNative.cs&project=lucaspimentel/dd-trace-dotnet)
- [Red Hat Developers: Interacting with native libraries in .NET Core 3.0](https://developers.redhat.com/blog/2019/09/06/interacting-with-native-libraries-in-net-core-3-0#)
- [Manski's Dev Log: P/Invoke Tutorial: Passing strings (Part 2)](https://manski.net/articles/dotnet/pinvoke-tutorial/part-2--passing-strings)


## C.2. C++ cross platform using CMake to create library.

- [Easily Create Shared Libraries with CMake (Part 1)](https://blog.shaduri.dev/easily-create-shared-libraries-with-cmake-part-1)

## C.3. About WFP .NET custom host.

- [WPF y netcore3 con custom host](https://www.eiximenis.dev/posts/2020-01-20-wpf-netcore3-customhost/)

## C.4. About .NET custom logger.

- [Create your own logging provider to log to text files in .NET Core](https://www.roundthecode.com/dotnet-tutorials/create-your-own-logging-provider-to-log-to-text-files-in-net-core)