using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Diplom
{
    public partial class MainForm : Form
    {
        public static string test;
        public static string name;
        public static string surname;
        public static string patronymic;
        public static string gender;
        public static DateTime dateOfBirth;
        public static DateTime dateOfPassage;
        public static bool readfile;
        public static bool start;
        public static SqlDataReader reader;
        public MainForm()
        {
            InitializeComponent();
            dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }
        //Добавление пациента в бд и выбор опросника
        private void furtherButton_Click(object sender, EventArgs e)
        {
            start = false;
            gender = Human.DefinitionOfGender(radioButton1, radioButton2);
            test = comboBox1.Text;
            name = textBox2.Text;
            surname = textBox1.Text;
            patronymic = textBox3.Text;
            dateOfBirth = dateTimePicker1.Value.Date;
            dateOfPassage = DateTime.Now;
            dateOfPassage = new DateTime(dateOfPassage.Year, dateOfPassage.Month, dateOfPassage.Day, dateOfPassage.Hour, dateOfPassage.Minute, dateOfPassage.Second);
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(patronymic) || string.IsNullOrEmpty(test) || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                MessageBox.Show("Одно из полей не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TestForm testform = new TestForm();
                testform.textBox1.Text = OperationWithForms.DefinitionInstruction(test);
                SqlCommand command = Human.SearchHuman(surname.Trim(), name.Trim(), patronymic.Trim(), test.Trim(), dateOfBirth);
                reader = command.ExecuteReader();
                command.Parameters.Clear();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        //если найден не законченый опрос
                        if (reader.GetValue(1).ToString() == "")
                        {
                            DialogResult dialogResult = MessageBox.Show("Опрос не закончен. Продолжить опрос?", "Предупреждение", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                StreamWriter sw = new StreamWriter(File.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Result.txt", FileMode.Append));
                                sw.Close();
                                readfile = true;
                                dateOfPassage = DateTime.Parse(reader.GetValue(0).ToString());
                                testform.Show();
                                reader.Close();
                                this.Hide();
                                OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
                                break;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                readfile = false;
                                reader.Close();
                                OperationWithPatient.NewEntry(surname, name, patronymic, dateOfBirth, dateOfPassage, gender, test);
                                testform.Show();
                                this.Hide();
                                OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    reader.Close();
                    readfile = false;
                    OperationWithPatient.NewEntry(surname.Trim(), name.Trim(), patronymic.Trim(), dateOfBirth, dateOfPassage, gender, test.Trim());
                    testform.Show();
                    this.Hide();
                    OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
                }
                if (!reader.IsClosed)
                {
                    readfile = false;
                    reader.Close();
                    OperationWithPatient.NewEntry(surname.Trim(), name.Trim(), patronymic.Trim(), dateOfBirth, dateOfPassage, gender, test.Trim());
                    testform.Show();
                    this.Hide();
                    OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
                }
            }
        }
        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
            Form form = new HistoryForm();
            form.Show();
            this.Hide();
        }

        private void создатьОпросникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
            Form form = new AddTestForm();
            form.Show();
            this.Hide();
        }

        private void удалитьОпросникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationWithForms.ClearingFields(textBox1, textBox2, textBox3, radioButton1, radioButton2, comboBox1);
            Form form = new DeleteTestForm();
            form.Show();
            this.Hide();
        }
        //Обновление списка опросников, при добавлении или удалении опросников
        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            List<String> listTest = WorkTest.TestList();
            foreach (string t in listTest)
            {
                comboBox1.Items.Add(t);
            }
        }
    }
}