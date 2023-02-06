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
    public partial class EmployeeView : Form, IEmployeeView
    {
        public EmployeeView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabEmployeeEdit);
            btnClose.Click += delegate { this.Close(); };

            if (Role == "User")
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
                tabControl1.TabPages.Remove(tabEmployeeList);
                tabControl1.TabPages.Add(tabEmployeeEdit);
                tabEmployeeEdit.Text = "Add new Employee";
            };
            btnEdit.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabEmployeeList);
                tabControl1.TabPages.Add(tabEmployeeEdit);
                tabEmployeeEdit.Text = "Edit Employee";
            };
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    tabControl1.TabPages.Remove(tabEmployeeEdit);
                    tabControl1.TabPages.Add(tabEmployeeList);
                }
                MessageBox.Show(Message);
            };
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabEmployeeEdit);
                tabControl1.TabPages.Add(tabEmployeeList);
            };
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        public string id_employee
        {
            get { return txtId.Text; }
            set { txtId.Text = value; }
        }
        public string surname
        {
            get { return txtSurname.Text; }
            set { txtSurname.Text = value; }
        }
        public string name
        {
            get { return txtName.Text; }
            set { txtName.Text = value; }
        }
        public string patronymic
        {
            get { return txtPatronymic.Text; }
            set { txtPatronymic.Text = value; }
        }
        public string qualification
        {
            get { return txtQualification.Text; }
            set { txtQualification.Text = value; }
        }
        public string id_activity
        {
            get { return txtAct.Text; }
            set { txtAct.Text = value; }
        }
        public string addition
        {
            get { return txtAddition.Text; }
            set { txtAddition.Text = value; }
        }
        public string salary
        {
            get { return txtSalary.Text; }
            set { txtSalary.Text = value; }
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

        public void SetEmployeeBindingSource(BindingSource employeeList)
        {
            dataGridView.DataSource = employeeList;
        }

        private static EmployeeView instance;
        public static EmployeeView GetInstance(Form parentContainer, string role)
        {
            Role = role;

            if (instance == null || instance.IsDisposed)
            {
                instance = new EmployeeView();
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
