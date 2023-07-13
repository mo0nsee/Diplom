using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace Diplom
{
    public class Human
    {
        private string name;
        private string surname;
        private string patronymic;
        private DateTime dateOfBirth;
        private DateTime dateOfPassage;
        private string gender;
        private string test;
        private string testResult;
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Patronymic { get => patronymic; set => patronymic = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public DateTime DateOfPassage { get => dateOfPassage; set => dateOfPassage = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Test { get => test; set => test = value; }
        public string TestResult { get => testResult; set => testResult = value; }

        public Human(string surname, string name, string patronymic, DateTime dateOfBirth, DateTime dateOfPassage, string gender, string test, string testResult)
        {
            this.Surname = surname;
            this.Name = name;
            this.Patronymic = patronymic;
            this.DateOfBirth = dateOfBirth;
            this.DateOfPassage = dateOfPassage;
            this.Gender = gender;
            this.Test = test;
            this.TestResult = testResult;
        }
        /*Определение пола*/
        public static string DefinitionOfGender(RadioButton radioButton1, RadioButton radioButton2)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                return "мужской";
            }
            else
            {
                radioButton1.Checked = false;
                return "женский";
            }
        }
        /*Добавление пациента в бд*/
        public static void InsertHuman(Human human)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "Insert Into People values (@Surname,@Name,@Patronymic,@DateOfBirth,@DateOfPassage,@Gender)";
            command.Parameters.AddWithValue("Surname", human.Surname);
            command.Parameters.AddWithValue("Name", human.Name);
            command.Parameters.AddWithValue("Patronymic", human.Patronymic);
            command.Parameters.AddWithValue("DateOfBirth", human.DateOfBirth);
            command.Parameters.AddWithValue("DateOfPassage", human.DateOfPassage);
            command.Parameters.AddWithValue("Gender", human.Gender);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Добавление информации о прохождении опросника пациентом*/
        public static void InsertTestResult(Human human)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "Insert Into TestResultPeople values (@DateOfPassage,@Test,@Result)";
            command.Parameters.AddWithValue("DateOfPassage", human.DateOfPassage);
            command.Parameters.AddWithValue("Test", human.Test);
            command.Parameters.AddWithValue("Result", human.TestResult);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Удаление пациента из бд*/
        public static void DeleteHuman(string surname, string name, string patronymic,DateTime dateOfPassage, string test)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "DELETE People WHERE (Name = @name AND Surname = @surname AND Patronymic = @patronymic AND DateOfPassage=@dateOfPassage)";
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("patronymic", patronymic);
            command.Parameters.AddWithValue("dateOfPassage", dateOfPassage);
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.CommandText = "DELETE TestResultPeople WHERE (DateOfPassage=@dateOfPassage AND Test = @test)";
            command.Parameters.AddWithValue("dateOfPassage", dateOfPassage);
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }

        /*Поиск пациента в бд*/
        public static SqlCommand SearchHuman(string surname, string name, string patronymic, DateTime dateOfPassage, string test)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Surname,Name,Patronymic,DateOfBirth,People.DateOfPassage,Gender,Test,Result FROM People INNER JOIN TestResultPeople ON People.DateOfPassage=TestResultPeople.DateOfPassage WHERE (Surname = @surname AND Name = @name AND Patronymic = @patronymic AND People.DateOfPassage=@dateOfPassage AND Test=@test)";
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("patronymic", patronymic);
            command.Parameters.AddWithValue("dateOfPassage", dateOfPassage);
            command.Parameters.AddWithValue("test", test);
            return command;
        }
        /*Поиск пациента в бд*/
        public static SqlCommand SearchHuman(string surname, string name, string patronymic, string test, DateTime dateOfBirth)
        {
            if (MainForm.start == false)
            {
                MainForm.reader.Close();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT People.DateOfPassage,Result FROM People INNER JOIN TestResultPeople ON People.DateOfPassage=TestResultPeople.DateOfPassage WHERE (Surname = @surname AND Name = @name AND Patronymic = @patronymic AND DateOfBirth=@dateOfBirth AND Test = @test)";
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("patronymic", patronymic);
            command.Parameters.AddWithValue("DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("test", test);
            return command;
        }
        /*Изменение данных пациента в бд*/
        public static void UpdHuman(Human humanCurrent, string surnameNew, string nameNew, string patronymicNew, DateTime dateOfBirthNew)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = $"UPDATE People Set Surname=@surnameNew,Name=@nameNew,Patronymic=@patronymicNew,DateOfBirth=@dateOfBirthNew " +
                $"WHERE (Surname = @surnameCurrent AND Name = @nameCurrent AND Patronymic = @patronymicCurrent AND DateOfBirth=@dateOfBirthCurrent AND DateOfPassage=@dateOfPassageCurrent AND Gender=@genderCurrent)";
            command.Parameters.AddWithValue("surnameNew", surnameNew);
            command.Parameters.AddWithValue("nameNew", nameNew);
            command.Parameters.AddWithValue("patronymicNew", patronymicNew);
            command.Parameters.AddWithValue("dateOfBirthNew", dateOfBirthNew);
            command.Parameters.AddWithValue("surnameCurrent", humanCurrent.Surname);
            command.Parameters.AddWithValue("nameCurrent", humanCurrent.Name);
            command.Parameters.AddWithValue("patronymicCurrent", humanCurrent.Patronymic);
            command.Parameters.AddWithValue("dateOfBirthCurrent", humanCurrent.DateOfBirth);
            command.Parameters.AddWithValue("dateOfPassageCurrent", humanCurrent.DateOfPassage);
            command.Parameters.AddWithValue("genderCurrent", humanCurrent.Gender);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Запись результата в бд после прохождения пациентом опросника агрессии*/
        public static void FormationOfResult(string surname, string name, string patronymic, DateTime dateOfBirth, DateTime dateOfPassage, TestBassaDarki testBassaDarki)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = $"UPDATE TestResultPeople SET Result = @result FROM People INNER JOIN TestResultPeople ON People.DateOfPassage=TestResultPeople.DateOfPassage " +
                $"WHERE (Surname = @surname AND Name = @name AND Patronymic = @patronymic AND DateOfBirth=@dateOfBirth AND People.DateOfPassage=@dateOfPassage) ";
            command.Parameters.AddWithValue("result", $"{testBassaDarki.AggressivenessIndex},{testBassaDarki.CosAggressia},{testBassaDarki.Negativism},{testBassaDarki.HostilityIndex},{testBassaDarki.Remorse}");
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("patronymic", patronymic);
            command.Parameters.AddWithValue("dateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("dateOfPassage", dateOfPassage);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Запись результата в бд после прохождения пациентом характерологического опросника*/
        public static void FormationOfResult(string surname, string name, string patronymic, DateTime dateOfBirth, DateTime dateOfPassage, TestLeongarda testLeongarda)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = $"UPDATE TestResultPeople SET Result = @result FROM People INNER JOIN TestResultPeople ON People.DateOfPassage=TestResultPeople.DateOfPassage " +
                $"WHERE (Surname = @surname AND Name = @name AND Patronymic = @patronymic AND DateOfBirth=@dateOfBirth AND People.DateOfPassage=@dateOfPassage) ";
            command.Parameters.AddWithValue("result", $"{testLeongarda.Hypertimal},{testLeongarda.Excitable},{testLeongarda.Emotional},{testLeongarda.Pedantic},{testLeongarda.Disturbing},{testLeongarda.Cyclotic},{testLeongarda.Demonstrative},{testLeongarda.Unbalanced},{testLeongarda.Dysthymal},{testLeongarda.Exalted}");
            command.Parameters.AddWithValue("surname", surname);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("patronymic", patronymic);
            command.Parameters.AddWithValue("dateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("dateOfPassage", dateOfPassage);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Вывод списка пациентов на форму*/
        public static void ListOfPeople(DataGridView dataGridView)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Surname,Name,Patronymic,DateOfBirth,People.DateOfPassage,Gender,Test FROM People INNER JOIN TestResultPeople ON People.DateOfPassage=TestResultPeople.DateOfPassage";
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[dataGridView.Columns.Count]);
                for (int i = 0; i < dataGridView.Columns.Count; ++i)
                {
                    data[data.Count - 1][i] = reader[i].ToString();
                }
            }
            reader.Close();
            foreach (string[] s in data)
            {
                s[3] = DateTime.Parse(s[3]).ToShortDateString();
                dataGridView.Rows.Add(s);
            }
        }
    }
}
