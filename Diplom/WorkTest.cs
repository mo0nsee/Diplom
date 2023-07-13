using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Diplom
{
    class WorkTest
    {
        //Добавление шкалы в бд
        public static void InsertScale(string test, string scales, string value)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = $"Insert Into ListScales values (@Test,@Scales,@Coefficient)";
            command.Parameters.AddWithValue("Test", test);
            command.Parameters.AddWithValue("Scales", scales);
            command.Parameters.AddWithValue("Coefficient", int.Parse(value));
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        //Добавление вопроса опросника без шкал в бд
        public static void InsertQuestionNoScales(string test, string question, string value)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = $"Insert Into ListQuestionsNoScales values (@Test,@Question,@Value)";
            command.Parameters.AddWithValue("Test", test);
            command.Parameters.AddWithValue("Question", question);
            command.Parameters.AddWithValue("Value", value);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        //Добавление вопроса опросника в бд
        public static void InsertQuestion(string scales, string question, string value)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = $"Insert Into ListQuestions values (@Scales,@Question,@Value)";
            command.Parameters.AddWithValue("Scales", scales);
            command.Parameters.AddWithValue("Question", question);
            command.Parameters.AddWithValue("Value", value);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        //Поиск имеющегося опросника в бд
        public static SqlCommand SearchTest(string test)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Test,Scales FROM ListTests WHERE Test=@test";
            command.Parameters.AddWithValue("test", test);
            return command;
        }
        //Поиск имеющейся шкалы в бд
        public static SqlCommand SearchScales(string scale)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Scales FROM ListScales WHERE Scales=@scale";
            command.Parameters.AddWithValue("scale", scale);
            return command;
        }
        //Поиск вопроса со шкалой в бд
        public static SqlCommand SearchQuestion(string question)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Scales,Question,Value FROM ListQuestions WHERE Question=@question";
            command.Parameters.AddWithValue("question", question);
            return command;
        }
        //Поиск вопроса без шкалы в бд
        public static SqlCommand SearchQuestionNoScales(string question)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Question,Value FROM ListQuestionsNoScales WHERE Question=@question";
            command.Parameters.AddWithValue("question", question);
            return command;
        }
        /*Добавление опросника в список*/
        public static void AddTestList(string test, bool scales)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "Insert Into ListTests values (@Test,@Scales)";
            command.Parameters.AddWithValue("Test", test);
            command.Parameters.AddWithValue("Scales", scales);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Список опросников из бд*/
        public static List<string> TestList()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Test FROM ListTests";
            MainForm.reader = command.ExecuteReader();
            List<string> data = new List<string>();
            while (MainForm.reader.Read())
            {
                data.Add(MainForm.reader.GetString(0));
            }
            MainForm.reader.Close();
            return data;
        }
        //Удаление опросника со шкалой из бд, то есть опросника,шкал,вопросов для него
        public static void DeleteTest(string test)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "DELETE ListQuestions FROM ListQuestions INNER JOIN ListScales ON ListScales.Scales=ListQuestions.Scales WHERE (ListScales.Test = @test)";
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.CommandText = "DELETE ListScales FROM ListScales INNER JOIN ListTests ON ListTests.Test = ListScales.Test WHERE (ListScales.Test = @test)";
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.CommandText = "DELETE ListTests WHERE (ListTests.Test = @test)";
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        //Удаление опросника без шкалы из бд, то есть опросника,вопросов для него
        public static void DeleteTestNoScales(string test)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "DELETE ListQuestionsNoScales FROM ListQuestionsNoScales INNER JOIN ListTests ON ListTests.Test = ListQuestionsNoScales.Test WHERE (ListQuestionsNoScales.Test = @test)";
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            command.CommandText = "DELETE ListTests WHERE (ListTests.Test = @test)";
            command.Parameters.AddWithValue("test", test);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        /*Загрузка вопросов из бд*/
        public static List<string> UploadingQuestions(string test)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ConnectionSQL.ConnectionBD;
            command.CommandText = "SELECT Question FROM ListQuestions INNER JOIN ListScales ON ListQuestions.Scales=ListScales.Scales WHERE (ListScales.Test = @test) ORDER BY ListQuestions.Id";
            command.Parameters.AddWithValue("test", test);
            SqlDataReader reader = command.ExecuteReader();
            List<string> data = new List<string>();
            while (reader.Read())
            {
                data.Add(reader.GetString(0));
            }
            reader.Close();
            return data;
        }
    }
}
