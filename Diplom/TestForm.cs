using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class TestForm : Form
    {
        int countAll;
        //загрузка вопросов из бд
        List<string> questions = WorkTest.UploadingQuestions(MainForm.test);
        public static TestBassaDarki testBassaDarki;
        public static TestLeongarda testLeongarda;
        int countQuestions1;
        int countQuestions2;
        int countQuestions3;
        int countQuestions4;
        int countQuestions5;
        public TestForm()
        {
            InitializeComponent();
        }
        private void Test_Load(object sender, EventArgs e)
        {
            countAll = OperationWithForms.DefinitionCount();
            if (MainForm.test == "Опросник Басса-Дарки")
            {
                testBassaDarki = new TestBassaDarki();
                countQuestions1 = countAll;
                countQuestions2 = countQuestions1 + 1;
                countQuestions3 = countQuestions2 + 1;
                countQuestions4 = countQuestions3 + 1;
                countQuestions5 = countQuestions4 + 1;
                textBox2.Text = questions[countQuestions1];
                textBox3.Text = questions[countQuestions2];
                textBox4.Text = questions[countQuestions3];
                textBox5.Text = questions[countQuestions4];
                textBox6.Text = questions[countQuestions5];
                if (countAll == 70)
                {
                    nextButton.Enabled = false;
                    countAll = 0;
                    endButton.Enabled = true;
                    return;
                }
            }
            if (MainForm.test == "Опросник Леонгарда")
            {
                testLeongarda = new TestLeongarda();
                countQuestions1 = countAll;
                countQuestions2 = countQuestions1 + 1;
                countQuestions3 = countQuestions2 + 1;
                countQuestions4 = countQuestions3 + 1;
                countQuestions5 = countQuestions4 + 1;
                textBox2.Text = questions[countQuestions1];
                textBox3.Text = questions[countQuestions2];
                textBox4.Text = questions[countQuestions3];
                if (countAll == 85)
                {
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    panel4.Visible = false;
                    panel5.Visible = false;
                    nextButton.Enabled = false;
                    countAll = 0;
                    endButton.Enabled = true;
                    return;
                }
                else
                {
                    textBox5.Text = questions[countQuestions4];
                    textBox6.Text = questions[countQuestions5];
                }
            }
        }
        //переход на следующую страницу, а также подсчёт баллов
        private void nextButton_Click(object sender, EventArgs e)
        {
            if ((radioButton1.Checked == false && radioButton2.Checked == false) || (radioButton3.Checked == false && radioButton4.Checked == false)
                || (radioButton5.Checked == false && radioButton6.Checked == false) || (radioButton7.Checked == false && radioButton8.Checked == false)
                || (radioButton9.Checked == false && radioButton10.Checked == false))
                MessageBox.Show("Отсутствует ответ на один из вопросов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (MainForm.test == "Опросник Леонгарда")
                {
                    if (countAll < 85)
                    {
                        OperationWithPatient.WriteResult(radioButton1, radioButton2,MainForm.readfile);
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        OperationWithPatient.WriteResult(radioButton3, radioButton4);
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;
                        OperationWithPatient.WriteResult(radioButton5, radioButton6);
                        radioButton5.Checked = false;
                        radioButton6.Checked = false;
                        OperationWithPatient.WriteResult(radioButton7, radioButton8);
                        radioButton7.Checked = false;
                        radioButton8.Checked = false;
                        OperationWithPatient.WriteResult(radioButton9, radioButton10);
                        radioButton9.Checked = false;
                        radioButton10.Checked = false;
                    }
                    MainForm.readfile = false;
                    countAll += 5;
                    OperationWithPatient.WriteFile();
                    countQuestions1 = countAll;
                    countQuestions2 = countQuestions1 + 1;
                    countQuestions3 = countQuestions2 + 1;
                    countQuestions4 = countQuestions3 + 1;
                    countQuestions5 = countQuestions4 + 1;
                    if (countAll == 85)
                    {
                        textBox2.Text = questions[countQuestions1];
                        textBox3.Text = questions[countQuestions2];
                        textBox4.Text = questions[countQuestions3];
                        textBox5.Visible = false;
                        textBox6.Visible = false;
                        panel4.Visible = false;
                        panel5.Visible = false;
                    }
                    if (questions.Count == countAll + 3)
                    {
                        nextButton.Enabled = false;
                        countAll = 0;
                        endButton.Enabled = true;
                        return;
                    }
                    textBox2.Text = questions[countQuestions1];
                    textBox3.Text = questions[countQuestions2];
                    textBox4.Text = questions[countQuestions3];
                    textBox5.Text = questions[countQuestions4];
                    textBox6.Text = questions[countQuestions5];
                }
                if (MainForm.test == "Опросник Басса-Дарки")
                {
                    OperationWithPatient.WriteResult(radioButton1, radioButton2, MainForm.readfile);
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    OperationWithPatient.WriteResult(radioButton3, radioButton4);
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    OperationWithPatient.WriteResult(radioButton5, radioButton6);
                    radioButton5.Checked = false;
                    radioButton6.Checked = false;
                    OperationWithPatient.WriteResult(radioButton7, radioButton8);
                    radioButton7.Checked = false;
                    radioButton8.Checked = false;
                    OperationWithPatient.WriteResult(radioButton9, radioButton10);
                    radioButton9.Checked = false;
                    radioButton10.Checked = false;
                    MainForm.readfile = false;
                    countAll += 5;
                    OperationWithPatient.WriteFile();
                    countQuestions1 = countAll;
                    countQuestions2 = countQuestions1 + 1;
                    countQuestions3 = countQuestions2 + 1;
                    countQuestions4 = countQuestions3 + 1;
                    countQuestions5 = countQuestions4 + 1;
                    textBox2.Text = questions[countQuestions1];
                    textBox3.Text = questions[countQuestions2];
                    textBox4.Text = questions[countQuestions3];
                    textBox5.Text = questions[countQuestions4];
                    textBox6.Text = questions[countQuestions5];
                    if (countAll == 70)
                    {
                        nextButton.Enabled = false;
                        countAll = 0;
                        endButton.Enabled = true;
                        return;
                    }
                }
            }
        }
        //Завершение опросника
        private void endButton_Click(object sender, EventArgs e)
        {
            if (MainForm.test == "Опросник Басса-Дарки")
            {
                if ((radioButton1.Checked == false && radioButton2.Checked == false) || (radioButton3.Checked == false && radioButton4.Checked == false)
                    || (radioButton5.Checked == false && radioButton6.Checked == false) || (radioButton7.Checked == false && radioButton8.Checked == false)
                    || (radioButton9.Checked == false && radioButton10.Checked == false))
                    MessageBox.Show("Отсутствует ответ на один из вопросов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    OperationWithPatient.WriteResult(radioButton1, radioButton2, MainForm.readfile);
                    OperationWithPatient.WriteResult(radioButton3, radioButton4);
                    OperationWithPatient.WriteResult(radioButton5, radioButton6);
                    OperationWithPatient.WriteResult(radioButton7, radioButton8);
                    OperationWithPatient.WriteResult(radioButton9, radioButton10);
                    TestBassaDarki.EndTest(testBassaDarki);
                    this.Close();
                }
            }
            if (MainForm.test == "Опросник Леонгарда")
            {
                if ((radioButton1.Checked == false && radioButton2.Checked == false) || (radioButton3.Checked == false && radioButton4.Checked == false)
                    || (radioButton5.Checked == false && radioButton6.Checked == false))
                    MessageBox.Show("Отсутствует ответ на один из вопросов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    OperationWithPatient.WriteResult(radioButton1, radioButton2, MainForm.readfile);
                    OperationWithPatient.WriteResult(radioButton3, radioButton4);
                    OperationWithPatient.WriteResult(radioButton5, radioButton6);
                    TestLeongarda.EndTest(testLeongarda);
                    this.Close();
                }
            }
        }
    }
}
