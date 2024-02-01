namespace pinvoke.consoleapp
{
    #region using

    using System.Runtime.InteropServices;

    #endregion

    class Program
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct Person
        {
            [MarshalAs(UnmanagedType.I4)]
            public int id;
            [MarshalAs(UnmanagedType.I4)]
            public int age;
        };

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        private static extern IntPtr createHoge();

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        private static extern void freeHoge(IntPtr instance);

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        private static extern int getResult(IntPtr instance, int a);

        [DllImport("C:\\Users\\javie\\Downloads\\demopinvoke\\x64\\Debug\\pinvoke.library.managed.dll")]
        private static extern void createPerson(IntPtr instance, ref Person person);

        public static void Main()
        {
            IntPtr dummy_a = createHoge(); 
            Console.WriteLine(dummy_a); // => Show the pointer address.
            int result = getResult(dummy_a, 10);
            Console.WriteLine(result); // => 15
            freeHoge(dummy_a);

            Console.WriteLine("-----");
            IntPtr dummy_b = createHoge();
            Person person = new();
            createPerson(dummy_b, ref person);
            freeHoge(dummy_b);

            Console.WriteLine(person.id);
            Console.WriteLine(person.age);
        }
    }
}