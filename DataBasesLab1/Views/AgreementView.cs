using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1.Views
{
    public partial class AgreementView : Form, IAgreementView
    {
        public AgreementView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabAgreementEdit);
            btnClose.Click += delegate { this.Close(); };

            if (Role == "User")
            {                
                btnEdit.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
            }
        }

        private static string Role;

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
           
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabAgreementList);
                tabControl1.TabPages.Add(tabAgreementEdit);
                tabAgreementEdit.Text = "Edit Agreement";
            };
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabAgreementEdit);
                    tabControl1.TabPages.Add(tabAgreementList);
                }
                MessageBox.Show(Message);
            };
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabAgreementEdit);
                tabControl1.TabPages.Add(tabAgreementList);
            };            
        }

        public string id_agreement
        {
            get { return txtId.Text; }
            set { txtId.Text = value; }
        }
        public string id_employer
        {
            get { return txtEmployerId.Text; }
            set { txtEmployerId.Text = value; }
        }
        public string id_employee
        {
            get { return txtEmployeeId.Text; }
            set { txtEmployeeId.Text = value; }
        }
        public string post
        {
            get { return txtPost.Text; }
            set { txtPost.Text = value; }
        }
        public string commission
        {
            get { return txtCommission.Text; }
            set { txtCommission.Text = value; }
        }
        public string term
        {
            get { return txtTermId.Text; }
            set { txtTermId.Text = value; }
        }
        public string SearchValue
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }
        public bool isEdit { get; set; }
        public bool isSuccessful { get; set; }
        public string Message { get; set; }

        public event EventHandler SearchEvent;
        public event EventHandler EditEvent;      
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public void SetAgreementBindingSource(BindingSource agreementList)
        {
            dataGridView.DataSource = agreementList;
        }

        private static AgreementView instance;
        public static AgreementView GetInstance(Form parentContainer, string role)
        {
            Role = role;

            if (instance == null || instance.IsDisposed)
            {
                instance = new AgreementView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
