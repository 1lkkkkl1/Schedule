using MySql.Data.MySqlClient;
using Rasp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schedule
{
    internal class Auditoriums
    {
        private List<Auditorium> list = new List<Auditorium>();

        public void Add(Auditorium aud)
        {
            list.Add(aud);
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand($"INSERT INTO `classrooms`(`classroom_id`, `classroom_number`, `capacity`) VALUES ({list.Last().id + 1},'{aud}',50)", dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetAll()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < list.Count; i++)
                res.Add(list[i].ToString());
            return res;
        }

        public void ImportFromDB()
        {
            list.Clear();
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand("SELECT * FROM `classrooms`", dataBase.GetConnection());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    list.Add(new Auditorium(reader.GetInt32("classroom_id"), reader.GetString("classroom_number")));
            }
        }

        public Auditorium Get(int id)
        {
            return list.Where(x => x.id == id).First();
        }

        public int Find(Auditorium a)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (a.ToString() == list[i].ToString())
                    return i;
            }
            return -1;
        }
    }
}