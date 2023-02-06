using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1.Views
{
    public partial class MainView : Form, IMainView
    {
        private Form ParentForm;
        public MainView(Form parentForm)
        {
            InitializeComponent();
            btnEmployers.Click += delegate { ShowEmployerView?.Invoke(this, EventArgs.Empty); };
            btnEmployees.Click += delegate { ShowEmployeeView?.Invoke(this, EventArgs.Empty); };
            btnAgreements.Click += delegate { ShowAgreementView?.Invoke(this, EventArgs.Empty); };
            ParentForm = parentForm;
        }

        public event EventHandler ShowEmployerView;
        public event EventHandler ShowEmployeeView;
        public event EventHandler ShowAgreementView;

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            ParentForm.Show();
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\GameMax\source\repos\DataBasesLab1\DataBasesLab1\_Repositories\Logs.txt");
        }
    }
}
