using MySql.Data.MySqlClient;
using Rasp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schedule
{
    internal class Teachers
    {

        private List<Teacher> list = new List<Teacher>();

        public void Add(Teacher teach) //метод для добавления преподавателя в список и в файл
        {
            list.Add(teach);

            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand($"INSERT INTO `teachers`(`teacher_id`, `post`, `first_name`, `middle_name`, `last_name`) VALUES " +
                    $"({list.Last().id + 1},'Преподаватель','{teach.name}','{teach.patronymic}','{teach.surname}')", dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        public Teacher Get(int id) //получение экземпляра по ид
        {
            return list.Where(x => x.id == id).First();
        }

        public List<string> GetAll() // для автозаполнения и для combobox с преподавателями
        {
            List<string> res = new List<string>();
            for (int i = 0; i < list.Count; i++)
                res.Add(list[i].ToString());
            return res;
        }

        public int Find(Teacher a) //поиск экземпляра класса по списку 
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
                var cmd = new MySqlCommand("SELECT * FROM `teachers`", dataBase.GetConnection());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    list.Add(new Teacher(reader.GetInt32("teacher_id"), reader.GetString("first_name"), reader.GetString("last_name"), reader.GetString("middle_name")));
            }
        }
    }
}