namespace Schedule
{
    internal class Group
    {
        public int id { get; private set; }
        private string grpoupName = "";

        public Group(int id, string name) //конструктор с параметрами
        {
            this.id = id;
            grpoupName = name;
        }

        public Group() //конструктор по умолчанию
        {
        }

        public override string ToString()
        {
            return grpoupName;
        }

    }
}