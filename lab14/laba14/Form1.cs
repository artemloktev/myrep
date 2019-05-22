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
    public partial class Form1 : Form
    {
        public string conn_param = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source = БД.mdb";


        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Owner = this;
            frm.textBoxName.Text = "";
            frm.textBoxSecondname.Text = "";
            frm.textBoxSurname.Text = "";
            frm.textBoxPhone.Text = "";
            frm.s = 0;
            frm.g = 0;
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Kod", "Код");
            dataGridView1.Columns.Add("Surname", "Фамилия");
            dataGridView1.Columns.Add("Name", "Имя");
            dataGridView1.Columns.Add("Secondname", "Отчество");
            dataGridView1.Columns.Add("Phone", "Телефон");


            
            OleDbConnection connection = new OleDbConnection(conn_param);
            OleDbCommand command = connection.CreateCommand();

            command.CommandText = "select * from Владельцы";
            connection.Open();
            OleDbDataReader reader = command.ExecuteReader();

            dataGridView1.Columns[0].Visible = true;

            int i = 0;

            try
            {

                while (reader.Read())
                {

                    dataGridView1.Rows.Add();

                    dataGridView1[0, i].Value = reader.GetInt32(0);
                    dataGridView1[1, i].Value = reader.GetString(1);
                    dataGridView1[2, i].Value = reader.GetString(2);
                    dataGridView1[3, i].Value = reader.GetString(3);
                    dataGridView1[4, i].Value = reader.GetValue(7);
                    ++i;
                }
            }

            finally
            {

                reader.Close();

                connection.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ind = dataGridView1.SelectedCells[0].RowIndex;

            Form2 frm = new Form2();
            frm.Owner = this;
            frm.textBoxName.Text = dataGridView1[2, ind].Value.ToString();
            frm.textBoxSecondname.Text = dataGridView1[3, ind].Value.ToString();
            frm.textBoxSurname.Text = dataGridView1[1, ind].Value.ToString();
            frm.textBoxPhone.Text = dataGridView1[4, ind].Value.ToString();
            frm.s = Convert.ToInt32(dataGridView1[0, ind].Value);
            frm.g = ind;
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить данную запись?", "Удалить",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                int ind = dataGridView1.SelectedCells[0].RowIndex;
                OleDbConnection connection = new OleDbConnection(conn_param);
                OleDbCommand command = connection.CreateCommand();

                command.CommandText = "DELETE FROM Владельцы WHERE Код = " + dataGridView1[0, ind].Value + ";";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                dataGridView1.Rows.RemoveAt(ind);

            }
        }
    }
}
