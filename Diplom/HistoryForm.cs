using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Diplom
{
    public partial class HistoryForm : Form
    {

        private ConnectionSQL newConnection = new ConnectionSQL();
        private SqlCommand command = new SqlCommand();
        public static string[] result;
        public static string test;
        public HistoryForm()
        {
            InitializeComponent();
            newConnection.Connection();
            command.Connection = ConnectionSQL.ConnectionBD;
            //загрузка списка пациентов в dataGridView
            Human.ListOfPeople(dataGridView1);
        }
        //кнопка "Назад"
        private void backButton_Click(object sender, EventArgs e)
        {
            Form mainform = Application.OpenForms[0];
            mainform.Show();
            this.Close();
        }
        //Удаление пациента
        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                Human.DeleteHuman(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(),DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()), dataGridView1.CurrentRow.Cells[6].Value.ToString());
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }
        //Формирование заключения
        private void conclusionButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = Human.SearchHuman(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()), dataGridView1.CurrentRow.Cells[6].Value.ToString());
            SqlDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    Human human = new Human(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), DateTime.Parse(reader.GetValue(3).ToString()), DateTime.Parse(reader.GetValue(4).ToString()), reader.GetValue(5).ToString(), reader.GetValue(6).ToString(), reader.GetValue(7).ToString());
                    if (reader.GetValue(7).ToString() != "")
                    {
                        result = reader.GetValue(7).ToString().Split(',');
                        TextWord.CreateWordDesktop(human);
                    }
                    else
                    {
                        result = null;
                        break;
                    }
                }
            }
            reader.Close();
            if (result == null)
            {
                MessageBox.Show("При записи результата произошла ошибка, запись будет удалена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Human.DeleteHuman(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()), dataGridView1.CurrentRow.Cells[6].Value.ToString());
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            else
                Array.Clear(HistoryForm.result, 0, HistoryForm.result.Length);
        }
        //Изменение данных пациента
        private void updateButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Изменить запись?", "Изменение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                UpdateHumanForm updateHuman = new UpdateHumanForm();
                TextBox[] textBoxes = { updateHuman.textBox1, updateHuman.textBox2, updateHuman.textBox3 };
                for (int i = 0; i < textBoxes.Length; ++i)
                {
                    textBoxes[i].Text = dataGridView1.CurrentRow.Cells[i].Value.ToString();
                }
                updateHuman.dateTimePicker1.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                updateHuman.textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                updateHuman.textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                updateHuman.textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                updateHuman.ShowDialog();
                dataGridView1.CurrentRow.Cells[0].Value = UpdateHumanForm.humanUpdate.Surname;
                dataGridView1.CurrentRow.Cells[1].Value = UpdateHumanForm.humanUpdate.Name;
                dataGridView1.CurrentRow.Cells[2].Value = UpdateHumanForm.humanUpdate.Patronymic;
                dataGridView1.CurrentRow.Cells[3].Value = UpdateHumanForm.humanUpdate.DateOfBirth;
                dataGridView1.CurrentRow.Cells[4].Value = UpdateHumanForm.humanUpdate.DateOfPassage;
                dataGridView1.CurrentRow.Cells[5].Value = UpdateHumanForm.humanUpdate.Gender;
                dataGridView1.CurrentRow.Cells[6].Value = UpdateHumanForm.humanUpdate.Test;
            }
        }
        //просмотр результата
        private void resultButton_Click(object sender, EventArgs e)
        {
            test = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            SqlCommand command = Human.SearchHuman(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()), test);
            SqlDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    if (reader.GetValue(7).ToString() != "")
                    {
                        result = reader.GetValue(7).ToString().Split(',');
                    }
                    else
                    {
                        result = null;
                        break;
                    }
                }
            }
            reader.Close();
            if (result != null)
            {
                ResultForm resultForm = new ResultForm();
                resultForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("При записи результата произошла ошибка, запись будет удалена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Human.DeleteHuman(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()), dataGridView1.CurrentRow.Cells[6].Value.ToString());
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
        }
    }
}
