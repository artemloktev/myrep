using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace laba14
{
    public partial class Form2 : Form
    {


        public int s = 0;
        public int g = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (s > 0)
            {

                Form1 frm = (Form1)this.Owner;

                OleDbConnection connection = new OleDbConnection(frm.conn_param);
                OleDbCommand command = connection.CreateCommand();

                command.CommandText = "UPDATE Владельцы SET Фамилия = \"" + textBoxSurname.Text + "\", Имя = \"" + textBoxName.Text + "\", Отчество = \"" + textBoxSecondname.Text + "\", Телефон = \"" + textBoxPhone.Text + "\" WHERE Код = " + s.ToString();

                frm.dataGridView1[1, g].Value = textBoxSurname.Text;
                frm.dataGridView1[2, g].Value = textBoxName.Text;
                frm.dataGridView1[3, g].Value = textBoxSecondname.Text;
                frm.dataGridView1[4, g].Value = textBoxPhone.Text;


                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                this.Close();
            }
            else
            {
                Form1 frm = (Form1)this.Owner;
                OleDbConnection connection = new OleDbConnection(frm.conn_param);
                OleDbCommand command = connection.CreateCommand();


                command.CommandText = "select max(Код) from Владельцы";
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                s = reader.GetInt32(0);
                reader.Close();
                s++;
                command.CommandText = "INSERT INTO Владельцы (Код, Фамилия, Имя, Отчество, Телефон)VALUES (" + s.ToString() + ", \"" + textBoxSurname.Text + "\", \"" + textBoxName.Text + "\", \"" + textBoxSecondname.Text + "\", \"" + textBoxPhone.Text + "\");";


                command.ExecuteNonQuery();

                int i = frm.dataGridView1.RowCount;

                frm.dataGridView1.Rows.Add();

                frm.dataGridView1[0, i].Value = s.ToString();
                frm.dataGridView1[1, i].Value = textBoxSurname.Text;
                frm.dataGridView1[2, i].Value = textBoxName.Text;
                frm.dataGridView1[3, i].Value = textBoxSecondname.Text;
                frm.dataGridView1[4, i].Value = textBoxPhone.Text;


                connection.Close();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
