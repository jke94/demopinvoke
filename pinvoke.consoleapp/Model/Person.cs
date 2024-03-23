namespace pinvoke.consoleapp.Model
{
    public class Person : IPerson
    {
        #region Fields

        private int _id = default!;
        private int _age = default!;
        private string _name = default!;

        #endregion

        #region Constructor

        public Person(int id, int age, string name)
        {
            _id = id;
            _age = age;
            _name = name;
        }

        #endregion

        #region Properties

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion
    }
}
