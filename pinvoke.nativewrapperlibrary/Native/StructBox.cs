namespace pinvoke.nativewrapperlibrary.Native
{
    #region using

    using System.Runtime.InteropServices;

    #endregion

    public class StructBox
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct PersonInfo
        {
            public int id;
            public int age;
            public IntPtr name; // IntPtr para el puntero char*
        }
    }
}