using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public class TestLeongarda
    {
        private int hypertimal;
        private int excitable;
        private int emotional;
        private int pedantic;
        private int disturbing;
        private int cyclotic;
        private int demonstrative;
        private int unbalanced;
        private int dysthymal;
        private int exalted;

        public int Hypertimal { get => hypertimal; set => hypertimal = value; }
        public int Excitable { get => excitable; set => excitable = value; }
        public int Emotional { get => emotional; set => emotional = value; }
        public int Pedantic { get => pedantic; set => pedantic = value; }
        public int Disturbing { get => disturbing; set => disturbing = value; }
        public int Cyclotic { get => cyclotic; set => cyclotic = value; }
        public int Demonstrative { get => demonstrative; set => demonstrative = value; }
        public int Unbalanced { get => unbalanced; set => unbalanced = value; }
        public int Dysthymal { get => dysthymal; set => dysthymal = value; }
        public int Exalted { get => exalted; set => exalted = value; }
        /*Подсчёт первичных баллов*/
        private void CompletionScales(string[] result)
        {
            SqlCommand command = UploadingScalesAndValue();
            SqlDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            int i = 0;
            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    if (result[i].TrimEnd('\r', '\n') == reader.GetString(0))
                    {
                        switch (reader.GetString(1))
                        {
                            case "Гипертимость":
                                Hypertimal += 1;
                                break;
                            case "Возбудимость":
                                Excitable += 1;
                                break;
                            case "Эмоциональность":
                                Emotional += 1;
                                break;
                            case "Педантичность":
                                Pedantic += 1;
                                break;
                            case "Тревожность":
                                Disturbing += 1;
                                break;
                            case "Циклотивность":
                                Cyclotic += 1;
                                break;
                            case "Демонстративность":
                                Demonstrative += 1;
                                break;
                            case "Неуравновешенность":
                                Unbalanced += 1;
                                break;
                            case "Дистимность":
                                Dysthymal += 1;
                                break;
                            case "Экзальтированность":
                                Exalted += 1;
                                break;
                            default: break;
                        }
                    }
                    i++;
                }
            }
            reader.Close();
            SecondaryScore();
        }
        /*Перевод во вторичные баллы*/
        private void SecondaryScore()
        {
            List<int> coef = UploadingCoef();
            Hypertimal *= coef[0];
            Excitable *= coef[1];
            Emotional *= coef[2];
            Pedantic *= coef[3];
            Disturbing *= coef[4];
            Cyclotic *= coef[5];
            Demonstrative *= coef[6];
            Unbalanced *= coef[7];
            Dysthymal *= coef[8];
            Exalted *= coef[9];
        }
        /*Завершение теста*/
        public static void EndTest(TestLeongarda testLeongarda)
        {
            testLeongarda.CompletionScales(OperationWithPatient.result);
            OperationWithPatient.result = new string[0];
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Result.txt");
            MessageBox.Show("Тест завершён", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Human.FormationOfResult(MainForm.surname, MainForm.name, MainForm.patronymic, MainForm.dateOfBirth, MainForm.dateOfPassage, testLeongarda);
            Form mainform = Application.OpenForms[0];
            mainform.Show();
        }
        /*Загрузка шкал и значений для проверки из бд*/
        private static SqlCommand UploadingScalesAndValue()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT ListQuestions.Value,ListQuestions.Scales FROM ListQuestions INNER JOIN ListScales ON ListScales.Scales=ListQuestions.Scales WHERE (ListScales.Test = @test) ORDER BY ListQuestions.Id";
            command.Parameters.AddWithValue("test", "Опросник Леонгарда");
            return command;
        }
        /*Загрузка коэффициентов из бд для подсчёта вторичных баллов*/
        private static List<int> UploadingCoef()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Coefficient FROM ListScales WHERE (Test = @test) ORDER BY ListScales.Id";
            command.Parameters.AddWithValue("test", "Опросник Леонгарда");
            SqlDataReader reader = command.ExecuteReader();
            List<int> data = new List<int>();
            while (reader.Read())
            {
                data.Add(reader.GetInt32(0));
            }
            reader.Close();
            return data;
        }
    }
}
