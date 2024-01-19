namespace Schedule
{
    internal class Teacher
    {
        public int id { get; private set; }
        public string name { get; private set; } = "";
        public string surname { get; private set; } = "";
        public string patronymic { get; private set; } = "";

        public Teacher(int id, string tempName, string tempSurname, string tempPatronymic)  //конструктор с параметрами
        {
            this.id = id;
            name = tempName;
            surname = tempSurname;
            patronymic = tempPatronymic;
        }

        public Teacher() //конструктор по умолчанию
        { }

        public override string ToString() // чтобы переопределить метод в классе-наследнике, этот метод определяется с модификатором override
        {
            return $"{surname} {name} {patronymic}"; //Знак доллара перед строкой указывает, что будет осуществляться интерполяция строк. Интерполяция - упрощенный механизм форматирования строк. 
        }
    }
}