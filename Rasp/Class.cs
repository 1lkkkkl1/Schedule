using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasp
{
    class Class
    {
        public int id { get; private set; }
        public string day { get; private set; }
        public Teacher teacher { get; private set; }
        public Group group { get; private set; }
        public Subject subject { get; private set; }
        public Auditorium auditorium { get; private set; }
        public Time time { get; private set; }

        public Class(int id, string day, Teacher teacher, Group group, Subject subject, Auditorium auditorium, Time time)
        {
            this.id = id;
            this.day = day;
            this.teacher = teacher;
            this.group = group;
            this.subject = subject;
            this.auditorium = auditorium;
            this.time = time;
        }
    }
}
