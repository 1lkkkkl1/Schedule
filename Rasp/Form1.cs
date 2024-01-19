using Rasp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Schedule
{
    public partial class Form1 : Form
    {
        static ClassSchedule a;
        static int count;
        
        
        public Form1()
        {   
           
            InitializeComponent();

            a = new ClassSchedule();
            a.ImportFromDB();
            count = Controls.Count;
            var source = new AutoCompleteStringCollection();
            var source2 = new AutoCompleteStringCollection();
            var source3 = new AutoCompleteStringCollection();

            Groups group = new Groups();
            group.ImportFromDB();
            source.AddRange(group.GetAll().ToArray());
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Auditoriums aud = new Auditoriums();
            aud.ImportFromDB();
            source2.AddRange(aud.GetAll().ToArray());
            textBox2.AutoCompleteCustomSource = source2;
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Teachers teach = new Teachers();
            teach.ImportFromDB();
            source3.AddRange(teach.GetAll().ToArray());
            textBox3.AutoCompleteCustomSource = source3;
            textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (Controls.Count>count)
                Controls.RemoveAt(count);
            var col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "";
            DataGridView data = new DataGridView();
            data.AllowUserToAddRows = false;
            data.Width = Width-35;
            data.Height = Height-160;
            data.Columns.Add(col1);
            data.RowTemplate.Height = 80;
            data.AllowUserToDeleteRows = false;
            data.ReadOnly = true;
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Times t = new Times(); t.ImportFromDB();
            List<string> time = t.GetAll();
            for (int i=0;i<time.Count;i++)
                data.Rows.Add(time[i]);
            string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
            for (int i = 0; i < days.Length; i++)
            {
                var col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = days[i];
                data.Columns.Add(col2);
                List<Class> list = a.ByGroup(textBox1.Text);
                for (int j=0;j<data.Rows.Count;j++)
                {
                    try
                    {
                        Class c = list.Where(x => x.day == days[i] && x.time.ToString() == data[0, j].Value.ToString()).First();
                        string temp = $"{c.subject}\n{c.teacher}\n{c.auditorium}";
                        data[i + 1, j].Value = temp;
                    }
                    catch { }
                }
            }
            Controls.Add(data);
            data.Location = new Point(5,120);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Controls.Count > count)
                Controls.RemoveAt(count);
            var col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "";
            DataGridView data = new DataGridView();
            data.AllowUserToAddRows = false;
            data.Width = Width - 35;
            data.Height = Height - 160;
            data.Columns.Add(col1);
            data.RowTemplate.Height = 80;
            data.AllowUserToDeleteRows = false;
            data.ReadOnly = true;
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Times t = new Times(); t.ImportFromDB();
            List<string> time = t.GetAll();
            for (int i = 0; i < time.Count; i++)
                data.Rows.Add(time[i]);
            string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            for (int i = 0; i < days.Length; i++)
            {
                var col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = days[i];
                data.Columns.Add(col2);
                List<Class> list = a.ByTeach(textBox3.Text);
                for (int j = 0; j < data.Rows.Count; j++)
                {
                    try
                    {
                        Class c = list.Where(x => x.day == days[i] && x.time.ToString() == data[0, j].Value.ToString()).First();
                        string temp = $"{c.subject}\n{c.teacher}\n{c.auditorium}";
                        data[i + 1, j].Value = temp;
                    }
                    catch { }
                }
            }
            this.Controls.Add(data);
            data.Location = new Point(5, 120);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Controls.Count > count)
                Controls.RemoveAt(count);
            var col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "";
            DataGridView data = new DataGridView();
            data.AllowUserToAddRows = false;
            data.Width = Width - 35;
            data.Height = Height - 160;
            data.Columns.Add(col1);
            data.RowTemplate.Height = 80;
            data.AllowUserToDeleteRows = false;
            data.ReadOnly = true;
            data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            data.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Times t = new Times(); t.ImportFromDB();
            List<string> time = t.GetAll();
            for (int i = 0; i < time.Count; i++)
                data.Rows.Add(time[i]);
            string[] days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
            for (int i = 0; i < days.Length; i++)
            {
                var col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = days[i];
                data.Columns.Add(col2);
                List<Class> list = a.ByAudit(textBox2.Text);
                for (int j = 0; j < data.Rows.Count; j++)
                {
                    try
                    {
                        Class c = list.Where(x => x.day == days[i] && x.time.ToString() == data[0, j].Value.ToString()).First();
                        string temp = $"{c.subject}\n{c.teacher}\n{c.auditorium}";
                        data[i + 1, j].Value = temp;
                    }
                    catch { }
                }
            }

            Controls.Add(data);
            data.Location = new Point(5, 120);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            using (Form2 form2 = new Form2())
                form2.ShowDialog();
            Show();
        }
    }
}