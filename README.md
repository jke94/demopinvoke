# demopinvoke

## A. Summary

Demo about how p/invoke works.

- ✅ Console client app (managed code - C#) consuming native code (C++).
- ✅ Lifecycle (create, edit and destroy) of native object and send it for the native manage code.
- ✅ Managed code editing values for native object.
- ✅🚀 Log based on C++ callback: communication between C++ and C#.
- ✅⭐ Event management from C# using C++ callbacks and established on runtime (C++ source code protection with mutex)

## B. Documentation

- [MSDN: Native library loading](https://learn.microsoft.com/en-us/dotnet/standard/native-interop/native-library-loading)
- [Hot Examples: update](https://csharp.hotexamples.com/site/file?hash=0x5ea59faae95926824d1676d7e6534832f9b73f1c41d7e3f2622198711090b595&fullName=dd-trace-dotnet-master/tracer/src/Datadog.Trace/AppSec/Waf/WafNative.cs&project=lucaspimentel/dd-trace-dotnet)
- [Red Hat Developers: tInteracting with native libraries in .NET Core 3.0](https://developers.redhat.com/blog/2019/09/06/interacting-with-native-libraries-in-net-core-3-0#)
- [Manski's Dev Log: P/Invoke Tutorial: Passing strings (Part 2)](https://manski.net/articles/dotnet/pinvoke-tutorial/part-2--passing-strings)