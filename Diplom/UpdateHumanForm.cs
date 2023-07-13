using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Diplom
{
    public partial class UpdateHumanForm : Form
    {
        public static Human humanUpdate;
        public UpdateHumanForm()
        {
            InitializeComponent();
        }
        //Загрузка первоначальных данных
        private void UpdateHuman_Load(object sender, EventArgs e)
        {
            humanUpdate = new Human(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value.Date, DateTime.Parse(textBox4.Text), textBox5.Text, textBox6.Text,"");
        }
        //Обновление данных пациента
        private void updateButton_Click(object sender, EventArgs e)
        {
            Human.UpdHuman(humanUpdate,textBox1.Text.Trim(), textBox2.Text.Trim(), textBox3.Text.Trim(), dateTimePicker1.Value.Date);
            humanUpdate = new Human(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value.Date, DateTime.Parse(textBox4.Text), textBox5.Text, textBox6.Text, "");
            this.Close();
        }
    }
}
