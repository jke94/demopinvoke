namespace pinvoke.consoleapp.Native
{
    #region using

    using System.Runtime.InteropServices;

    #endregion

    public class StructBox
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct ConfigPerson
        {
            public int id;
            public int age;
            public IntPtr name; // IntPtr para el puntero char*
        }
    }
}