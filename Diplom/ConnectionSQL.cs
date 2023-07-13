using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Diplom
{
    class ConnectionSQL
    {
        private static SqlConnection connectionbd = null;

        public static SqlConnection ConnectionBD { get => connectionbd; set => connectionbd = value; }
        /*Соединение с бд*/
        public void Connection()
        {
#if DEBUG == false
            String dpPathMyDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ConnectionBD = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={dpPathMyDocs}\\LocalDB.mdf;Integrated Security=True");
#endif
#if DEBUG == true
            ConnectionBD = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\DiplomMain\\Diplom\\LocalDB.mdf;Integrated Security=True");
#endif
            ConnectionBD.Open();
        }
    }
}
