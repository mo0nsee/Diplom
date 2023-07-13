using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class DeleteTestForm : Form
    {
        public DeleteTestForm()
        {
            InitializeComponent();
        }

        private void DeleteTest_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            //загрузка вопросов в combobox1
            List<String> listTest = WorkTest.TestList();
            foreach (string test in listTest)
            {
                comboBox1.Items.Add(test);
            }
            comboBox1.SelectedIndex = -1;
        }
        //Кнопка "Удаление"
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = WorkTest.SearchTest(comboBox1.Text.Trim());
            SqlDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    if (reader.GetBoolean(1) == true)
                    {
                        reader.Close();
                        WorkTest.DeleteTest(comboBox1.Text);
                        break;
                    }
                    else
                    {
                        reader.Close();
                        WorkTest.DeleteTestNoScales(comboBox1.Text);
                        break;
                    }
                }
            }
            comboBox1.Items.Remove(comboBox1.Text);
        }
        //Кнопка "Назад"
        private void button2_Click(object sender, EventArgs e)
        {
            Form mainform = Application.OpenForms[0];
            mainform.Show();
            this.Close();
        }
    }
}
