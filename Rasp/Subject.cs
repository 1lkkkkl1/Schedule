namespace Schedule
{
    internal class Subject
    {
        public int id { get; private set; }
        private string subjectName = "";

        public Subject(int id, string name)
        {
            this.id = id;
            subjectName = name;
        }

        public Subject()
        { }

        public override string ToString()
        {
            return subjectName;
        }
    }
}