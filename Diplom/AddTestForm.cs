using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Diplom
{
    public partial class AddTestForm : Form
    {
        public AddTestForm()
        {
            InitializeComponent();

        }
        //Добавление названия теста в бд
        private void button1_Click(object sender, EventArgs e)
        {
            //проверка на пустоту
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Имя опросника пустое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand command = WorkTest.SearchTest(textBox1.Text.Trim());
                SqlDataReader reader = command.ExecuteReader();
                command.Parameters.Clear();
                //чтение из списка с тестами
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        if(reader.GetValue(0).ToString() == textBox1.Text.Trim())
                            MessageBox.Show("Опросник с таким названием уже существует. \n Но есть возможность добавить новые вопросы и шкалы.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if(reader.GetBoolean(1)==true)
                        {
                            textBox3.Visible = true;
                            textBox4.Visible = true;
                            label5.Visible = true;
                            label3.Visible = true;
                            button4.Visible = true;
                        }
                        else
                        {
                            label2.Visible = true;
                            label4.Visible = true;
                            textBox2.Visible = true;
                            radioButton1.Visible = true;
                            radioButton2.Visible = true;
                            button2.Visible = true;
                        }
                    }
                    reader.Close();
                    button1.Enabled = false;
                }
                //если тест не найден
                else
                {
                    reader.Close();
                    DialogResult dialogResult = MessageBox.Show("Добавить опросник со шкалами?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        WorkTest.AddTestList(textBox1.Text.Trim(),true);
                        textBox3.Visible = true;
                        textBox4.Visible = true;
                        label5.Visible = true;
                        label3.Visible = true;
                        button4.Visible = true;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        WorkTest.AddTestList(textBox1.Text.Trim(), false);
                        label2.Visible = true;
                        label4.Visible = true;
                        textBox2.Visible = true;
                        radioButton1.Visible = true;
                        radioButton2.Visible = true;
                        button2.Visible = true;
                    }
                    button1.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
        }
        //кнопка "Назад"
        private void button3_Click(object sender, EventArgs e)
        {
            Form mainform = Application.OpenForms[0];
            mainform.Show();
            textBox1.Clear();
            this.Close();
        }
        //Кнопка "Добавление вопроса"
        private void button2_Click(object sender, EventArgs e)
        {
            //определение значения
            string value="";
            if(radioButton1.Checked==true)
            {
                radioButton2.Checked = false;
                value = "True";
            }
            else if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                value = "False";
            }
            //если со шкалами
            if (textBox3.Visible == true || textBox4.Visible == true || button4.Visible == true)
            {
                if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Одно из полей не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand command = WorkTest.SearchQuestion(textBox2.Text.Trim());
                    SqlDataReader reader = command.ExecuteReader();
                    command.Parameters.Clear();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            if (reader.GetValue(0).ToString() == textBox3.Text.Trim() || reader.GetValue(1).ToString() == textBox2.Text.Trim() || reader.GetValue(2).ToString() == value)
                            {
                                MessageBox.Show("Вопрос существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                reader.Close();
                                break;
                            }
                            else
                            {
                                reader.Close();
                                WorkTest.InsertQuestion(textBox3.Text.Trim(), textBox2.Text.Trim(), value);
                                break;
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        WorkTest.InsertQuestion(textBox3.Text.Trim(), textBox2.Text.Trim(), value);
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    radioButton1.Checked = false;
                    button4.Enabled = true;
                    radioButton2.Checked = false;
                    label2.Visible = false;
                    label4.Visible = false;
                    textBox2.Visible = false;
                    radioButton1.Visible = false;
                    radioButton2.Visible = false;
                    button2.Visible = false;
                }
            }
            //если без с шкал
            else
            {
                if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Одно из полей не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand command = WorkTest.SearchQuestionNoScales(textBox2.Text.Trim());
                    SqlDataReader reader = command.ExecuteReader();
                    command.Parameters.Clear();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            if (reader.GetValue(0).ToString() == textBox2.Text.Trim() || reader.GetValue(1).ToString() == value)
                            {
                                MessageBox.Show("Вопрос существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                reader.Close();
                                break;
                            }
                            else
                            {
                                reader.Close();
                                WorkTest.InsertQuestionNoScales(textBox1.Text.Trim(), textBox2.Text.Trim(), value);
                                break;
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        WorkTest.InsertQuestionNoScales(textBox1.Text.Trim(), textBox2.Text.Trim(), value);
                    }
                    textBox2.Clear();
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
            }
        }
        // Кнопка "Добавить шкалу"
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Visible == true)
            {
                if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("Одно из полей не заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand command = WorkTest.SearchScales(textBox3.Text.Trim());
                    SqlDataReader reader = command.ExecuteReader();
                    command.Parameters.Clear();
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            if (reader.GetValue(0).ToString() == textBox3.Text.Trim())
                            {
                                MessageBox.Show("Шкала с таким названием уже существует", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                reader.Close();
                                break;
                            }
                            else
                            {
                                reader.Close();
                                WorkTest.InsertScale(textBox1.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim());
                                break;
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        WorkTest.InsertScale(textBox1.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim());
                    }
                    button4.Enabled = false;
                    label2.Visible = true;
                    label4.Visible = true;
                    textBox2.Visible = true;
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    button2.Visible = true;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                }
            }
        }
    }
}
