using MySql.Data.MySqlClient;
using Rasp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schedule
{
    internal class Subjects
    {
        private List<Subject> list = new List<Subject>();

        public void Add(Subject sub)
        {
            list.Add(sub);
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand($"INSERT INTO `subjects`(`subject_id`, `subject_name`) VALUES ({list.Last().id+1},'{sub}')", dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        public void ImportFromDB()
        {
            list.Clear();
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand("SELECT * FROM `subjects`", dataBase.GetConnection());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    list.Add(new Subject(reader.GetInt32("subject_id"), reader.GetString("subject_name")));
            }
        }

        public Subject Get(int id)
        {
            return list.Where(x => x.id == id).First();
        }

        public int Find(Subject a)
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