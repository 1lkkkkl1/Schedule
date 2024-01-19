namespace Schedule
{
    internal class Auditorium
    {
        public int id { get; private set; }
        private string number = "";

        public Auditorium(int id, string name)
        {
            this.id = id;
            number = name;
        }

        public Auditorium()
        { }

        public override string ToString()
        {
            return number;
        }
    }
}