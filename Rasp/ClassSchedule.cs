using MySql.Data.MySqlClient;
using Rasp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Schedule
{
    internal class ClassSchedule
    {
        string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
        Teachers teach;
        Groups grou;
        Subjects subj;
        Auditoriums audit;
        Times time;
        public List<Class> network = new List<Class>();

        public ClassSchedule(Teachers t, Groups g, Subjects s, Auditoriums a, Times tt)
        {
            teach = t;
            grou = g;
            subj = s;
            audit = a;
            time = tt;
        }

        public ClassSchedule()
        {
            teach = new Teachers();
            teach.ImportFromDB();
            audit = new Auditoriums();
            audit.ImportFromDB();
            grou = new Groups();
            grou.ImportFromDB();
            subj = new Subjects();
            subj.ImportFromDB();
            time = new Times();
            time.ImportFromDB();
        }

        public void Add(Class _class) //добавление ячейки расписания по индексам в списках
        {
            network.Add(_class);
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand($"INSERT INTO `schedule`(`schedule_id`, `group_id`, `teacher_id`, `classroom_id`, `day_id`, `timeslot_id`, `subject_id`) VALUES " +
                    $"({network.Last().id+1},{_class.group.id},{_class.teacher.id},{_class.auditorium.id},(SELECT `day_id` FROM `days` WHERE `day_name`='{_class.day}'),{_class.time.id},{_class.subject.id})", dataBase.GetConnection());
                cmd.ExecuteNonQuery();
            }
        }

        public void ImportFromDB() //инициализация расписания из файла
        {
            network.Clear();
            using (DataBase dataBase = new DataBase())
            {
                dataBase.OpenConnection();
                var cmd = new MySqlCommand("SELECT *,(SELECT `day_name` FROM `days` WHERE `days`.`day_id`=`schedule`.`day_id`) AS `day` FROM `schedule`", dataBase.GetConnection());
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    network.Add(new Class(
                        reader.GetInt32("schedule_id"),
                        reader.GetString("day"),
                        teach.Get(reader.GetInt32("teacher_id")),
                        grou.Get(reader.GetInt32("group_id")),
                        subj.Get(reader.GetInt32("subject_id")),
                        audit.Get(reader.GetInt32("classroom_id")),
                        time.Get(reader.GetInt32("timeslot_id"))
                        ));
            }
        }

        public List<Class> ByGroup(string g) //Для поиска записей расписание по номеру группы 
        {
            return network.Where(x => x.group.ToString() == g).ToList();
        }

        public List<Class> ByTeach(string g) //фио преподавателя
        {
            return network.Where(x => x.teacher.ToString() == g).ToList();
        }

        public List<Class> ByAudit(string g) //номеру аудитории
        {
            return network.Where(x => x.auditorium.ToString() == g).ToList();
        }
    }
}
