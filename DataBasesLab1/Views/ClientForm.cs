using DataBasesLab1.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1.Views
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string role = comboBox1.Text;
            string oracleConnection = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            IMainView view = new MainView(this);
            new MainPresenter(view, oracleConnection, role);

            this.Hide();
        }
    }
}
