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
    class OperationWithForms
    {
        /*Определение с какого вопроса начинать*/
        public static int DefinitionCount()
        {
            if (MainForm.readfile == true)
            {
                StreamReader streamReader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Result.txt");
                OperationWithPatient.result = streamReader.ReadToEnd().Split('\n');
                streamReader.Close();
                return (OperationWithPatient.result.Length - 1);
            }
            else
                return 0;
        }
        /*Очистка полей*/
        public static void ClearingFields(TextBox textBox1,TextBox textBox2,TextBox textBox3,RadioButton radioButton1,RadioButton radioButton2,ComboBox comboBox1)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedIndex = -1;
        }
        /*Определение инструкции*/
        public static string DefinitionInstruction(string test)
        {
            if (test == "Опросник Басса-Дарки")
                return "Отметьте «да», если вы согласны с утверждением, и «нет» - если не согласны." +
                    "Старайтесь долго над вопросами не раздумывать.";
            else
                return "Вам будут предложены утверждения, касающиеся Вашего характера. " +
                    "Если Вы согласны с утверждением, рядом с его номером поставьте знак «да», если нет — «нет»." +
                    "Над вопросами долго не думайте, правильных и неправильных ответов нет.";
        }
    }
}
