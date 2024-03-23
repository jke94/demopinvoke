namespace pinvoke.consoleapp.Model
{
    public interface IPerson
    {
        #region Properties

        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }

        #endregion
    }
}
