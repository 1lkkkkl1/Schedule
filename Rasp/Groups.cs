using MySql.Data.MySqlClient;
using Rasp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schedule
{
    internal class Groups
    {
        private List<Group> list = new List<Group>();

        public void Add(Group g)
        {
            list.Add(g);
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand($"INSERT INTO `groups`(`group_id`, `group_name`, `course`) VALUES" +
                    $" ({list.Last().id + 1},'{g}',{(int)g.ToString()[g.ToString().Length - 2]})", dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetAll() //???
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
                var cmd = new MySqlCommand("SELECT * FROM `groups`", dataBase.GetConnection());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    list.Add(new Group(reader.GetInt32("group_id"), reader.GetString("group_name")));
            }
        }

        public Group Get(int id)
        {
            return list.Where(x => x.id == id).First();
        }

        public int Find(Group a) //???
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