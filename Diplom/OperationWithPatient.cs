using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    class OperationWithPatient
    {
        public static string[] result = { };
        /*Новая запись*/
        public static void NewEntry(string surname,string name, string patronymic, DateTime dateOfBirth, DateTime dateOfPassage, string gender, string test)
        {
            Human human = new Human(surname, name, patronymic, dateOfBirth, dateOfPassage, gender, test, "");
            Human.InsertHuman(human);
            Human.InsertTestResult(human);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Result.txt");
        }
        /*Запись результата в файл*/
        public static void WriteFile()
        {
            StreamWriter sw = new StreamWriter(File.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Result.txt", FileMode.Append));
            for (int i = 0; i < 5; i++)
            {
                sw.WriteLine(result[i]);
            }
            sw.Close();
        }
        /*Запись результата в массив*/
        public static void WriteResult(RadioButton radioButton1, RadioButton radioButton2,bool readFile)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                if(readFile == false)
                    Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = true.ToString();
            }
            else
            {
                radioButton1.Checked = false;
                if (readFile == false)
                    Array.Resize(ref result,result.Length + 1);
                result[result.Length - 1] = false.ToString();
            }
        }
        /*Запись результата в массив*/
        public static void WriteResult(RadioButton radioButton1, RadioButton radioButton2)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = true.ToString();
            }
            else
            {
                radioButton1.Checked = false;
                Array.Resize(ref result, result.Length + 1);
                result[result.Length - 1] = false.ToString();
            }
        }
    }
}
