namespace Schedule
{
    internal class Time
    {
        public int id { get; private set; }
        public string start { get; private set; } = "";
        public string stop { get; private set; } = "";

        public Time(int id, string st, string stp)
        {
            this.id = id;
            start = st;
            stop = stp;
        }

        public Time()
        { }

        public override string ToString()
        {
            return $"{start} - {stop}";
        }
    }
}