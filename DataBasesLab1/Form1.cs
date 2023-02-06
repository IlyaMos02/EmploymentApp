using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1
{
    public partial class Form1 : Form
    {

        string con;
        public Form1(string con)
        {
            InitializeComponent();
            this.con = con;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OracleConnection connection = new OracleConnection();
            connection.ConnectionString = con;

            connection.Open();
            OracleCommand command = new OracleCommand();
            command.CommandText = "Select name from Activity where id_activity = 9";
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            OracleDataReader reader = command.ExecuteReader();

            reader.Read();
            label1.Text = reader.GetString(0);
            connection.Close();
        }
    }
}
