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
    public partial class EmployerForm : Form, IEmployerView
    {
        public EmployerForm()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabEmployerEdit);
            btnClose.Click += delegate { this.Close(); };

            if(Role == "User")
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
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
            
            btnAdd.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabEmployerList);
                tabControl1.TabPages.Add(tabEmployerEdit);
                tabEmployerEdit.Text = "Add new Employer";
            };
            btnEdit.Click += delegate 
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabEmployerList);
                tabControl1.TabPages.Add(tabEmployerEdit);
                tabEmployerEdit.Text = "Edit Employer";
            };
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if(isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabEmployerEdit);
                    tabControl1.TabPages.Add(tabEmployerList);
                }
                MessageBox.Show(Message);
            };
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabEmployerEdit);
                tabControl1.TabPages.Add(tabEmployerList);
            };
            btnDelete.Click += delegate 
            {            
                var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        public string id_employer
        {
            get { return txtId.Text; }
            set { txtId.Text = value; }
        }
        public string title
        {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }
        public string id_activity
        {
            get { return txtAct.Text; }
            set { txtAct.Text = value; }
        }
        public string adress
        {
            get { return txtAdress.Text; }
            set { txtAdress.Text = value; }
        }
        public string phone
        {
            get { return txtPhone.Text; }
            set { txtPhone.Text = value; }
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
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler RoleEvent;
        public void SetEmployerBindingSource(BindingSource employerList)
        {
            dataGridView.DataSource = employerList;
        }

        private static EmployerForm instance;
        public static EmployerForm GetInstance(Form parentContainer, string role)
        {
            Role = role;

            if (instance == null || instance.IsDisposed)
            {
                instance = new EmployerForm();
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
