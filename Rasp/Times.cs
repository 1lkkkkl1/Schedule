using MySql.Data.MySqlClient;
using Rasp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schedule
{
    internal class Times
    {
        private List<Time> list = new List<Time>();

        public void Add(Time tim) //метод для добавления  в список
        {
            list.Add(tim);
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand($"INSERT INTO `timeslots`(`timeslot_id`, `start_time`, `end_time`) VALUES ({list.Last().id+1},'{tim.start}','{tim.stop}')", dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        public Time Get(int id) //получение экземпляра по ид
        {
            return list.Where(x => x.id == id).First();
        }

        public int Find(Time a) //поиск экземпляра класса по списку 
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (a.ToString() == list[i].ToString())
                    return i;
            }
            return -1;
        }

        public void ImportFromDB() //чтение списка из файла
        {
            list.Clear();
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand("SELECT * FROM `timeslots`", dataBase.GetConnection());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    list.Add(new Time(reader.GetInt32("timeslot_id"), reader.GetString("start_time"), reader.GetString("end_time")));
            }
        }

        public List<string> GetAll()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                res.Add(list[i].ToString());
            }
            return res;
        }
    }
}