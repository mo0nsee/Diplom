using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }
        //Загрузка резлуьтата для соотвествующего опросника
        private void ResultForm_Load(object sender, EventArgs e)
        {
            if (HistoryForm.test == "Опросник Басса-Дарки")
            {
                chart1.Series[0].Points.AddXY("Индекс агрессивности", HistoryForm.result[0]);
                chart1.Series[0].Points.AddXY("Косвенная агрессия", HistoryForm.result[1]);
                chart1.Series[0].Points.AddXY("Негативизм", HistoryForm.result[2]);
                chart1.Series[0].Points.AddXY("Индекс враждебности", HistoryForm.result[3]);
                chart1.Series[0].Points.AddXY("Чувство вины", HistoryForm.result[4]);
            }
            if (HistoryForm.test == "Опросник Леонгарда")
            {
                chart1.Series[0].Points.AddXY("Гипертимость", HistoryForm.result[0]);
                chart1.Series[0].Points.AddXY("Возбудимость", HistoryForm.result[1]);
                chart1.Series[0].Points.AddXY("Эмотивность", HistoryForm.result[2]);
                chart1.Series[0].Points.AddXY("Педантичность", HistoryForm.result[3]);
                chart1.Series[0].Points.AddXY("Тревожность", HistoryForm.result[4]);
                chart1.Series[0].Points.AddXY("Циклотивность", HistoryForm.result[5]);
                chart1.Series[0].Points.AddXY("Демонстративность", HistoryForm.result[6]);
                chart1.Series[0].Points.AddXY("Неуравновешенность", HistoryForm.result[7]);
                chart1.Series[0].Points.AddXY("Дистимность", HistoryForm.result[8]);
                chart1.Series[0].Points.AddXY("Экзальтированность", HistoryForm.result[9]);
            }
        }
        //Кнопка "Назад"
        private void backButton_Click(object sender, EventArgs e)
        {
            Array.Clear(HistoryForm.result, 0, HistoryForm.result.Length);
            this.Close();
        }
    }
}
