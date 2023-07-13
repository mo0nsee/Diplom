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
    public class TestBassaDarki
    {
        private int fisAggressia;
        private int cosAggressia;
        private int irritation;
        private int negativism;
        private int resentment;
        private int suspicion;
        private int verbAggressia;
        private int remorse;

        public int FisAggressia { get => fisAggressia; set => fisAggressia = value; }
        public int CosAggressia { get => cosAggressia; set => cosAggressia = value; }
        public int Irritation { get => irritation; set => irritation = value; }
        public int Negativism { get => negativism; set => negativism = value; }
        public int Resentment { get => resentment; set => resentment = value; }
        public int Suspicion { get => suspicion; set => suspicion = value; }
        public int VerbAggressia { get => verbAggressia; set => verbAggressia = value; }
        public int Remorse { get => remorse; set => remorse = value; }
        public int HostilityIndex { get => (Resentment + Suspicion)/2; }
        public int AggressivenessIndex { get => (FisAggressia + Irritation+ VerbAggressia)/3; }
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
                            case "Физическая агрессия":
                                FisAggressia += 1;
                                break;
                            case "Косвенная агрессия":
                                CosAggressia += 1;
                                break;
                            case "Раздражение":
                                Irritation += 1;
                                break;
                            case "Негативизм":
                                Negativism += 1;
                                break;
                            case "Обида":
                                Resentment += 1;
                                break;
                            case "Подозрительность":
                                Suspicion += 1;
                                break;
                            case "Вербальная агрессия":
                                VerbAggressia += 1;
                                break;
                            case "Чувство вины":
                                Remorse += 1;
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
            FisAggressia *= coef[0];
            CosAggressia *= coef[1];
            Irritation *= coef[2];
            Negativism *= coef[3];
            Resentment *= coef[4];
            Suspicion *= coef[5];
            VerbAggressia *= coef[6];
            Remorse *= coef[7];
        }
        /*Завершение теста*/
        public static void EndTest(TestBassaDarki testBassaDarki)
        {
            testBassaDarki.CompletionScales(OperationWithPatient.result);
            OperationWithPatient.result = new string[0];
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Result.txt");
            MessageBox.Show("Тест завершён", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Human.FormationOfResult(MainForm.surname, MainForm.name, MainForm.patronymic, MainForm.dateOfBirth, MainForm.dateOfPassage, testBassaDarki);
            Form mainform = Application.OpenForms[0];
            mainform.Show();
        }
        /*Загрузка шкал и значений для проверки из бд*/
        private static SqlCommand UploadingScalesAndValue()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT ListQuestions.Value,ListQuestions.Scales FROM ListQuestions INNER JOIN ListScales ON ListScales.Scales=ListQuestions.Scales WHERE (ListScales.Test = @test) ORDER BY ListQuestions.Id";
            command.Parameters.AddWithValue("test", "Опросник Басса-Дарки");
            return command;
        }
        /*Загрузка коэффициентов из бд для подсчёта вторичных баллов*/
        private static List<int> UploadingCoef()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Coefficient FROM ListScales WHERE (Test = @test) ORDER BY ListScales.Id";
            command.Parameters.AddWithValue("test", "Опросник Басса-Дарки");
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
