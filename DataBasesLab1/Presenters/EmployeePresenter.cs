using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBasesLab1.Models;
using DataBasesLab1.Views;

namespace DataBasesLab1.Presenters
{
    public class EmployeePresenter
    {
        private IEmployeeView view;
        private IEmployeeRepository repository;
        private BindingSource employeesBindingSource;
        private IEnumerable<EmployeeModel> employeeList;
        public EmployeePresenter(IEmployeeView view, IEmployeeRepository repository)
        {
            this.employeesBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent -= this.SearchEmployee;
            this.view.AddNewEvent -= this.AddEmployee;
            this.view.EditEvent -= this.LoadToEditEmployee;
            this.view.DeleteEvent -= this.DeleteEmployee;
            this.view.SaveEvent -= this.SaveEmployee;
            this.view.CancelEvent -= this.CancelAction;

            this.view.SearchEvent += this.SearchEmployee;
            this.view.AddNewEvent += this.AddEmployee;
            this.view.EditEvent += this.LoadToEditEmployee;
            this.view.DeleteEvent += this.DeleteEmployee;
            this.view.SaveEvent += this.SaveEmployee;
            this.view.CancelEvent += this.CancelAction;

            this.view.SetEmployeeBindingSource(employeesBindingSource);

            LoadAllEmployees();

            this.view.Show();           
        }

        private void LoadAllEmployees()
        {
            employeeList = repository.GetAll();
            employeesBindingSource.DataSource = employeeList;
        }

        private void CleanViewFields()
        {
            view.id_employee = "0";
            view.surname = "";
            view.name = "";
            view.patronymic = "";
            view.qualification = "";
            view.id_activity = "0";
            view.addition = "";
            view.salary = "0";
        }

        private void SearchEmployee(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                employeeList = repository.GetByValue(this.view.SearchValue);
            else
                employeeList = repository.GetAll();
            employeesBindingSource.DataSource = employeeList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }
        private void DeleteEmployee(object sender, EventArgs e)
        {
            try
            {
                var employee = (EmployeeModel)employeesBindingSource.Current;
                repository.Delete(employee.Id_employee);
                view.isSuccessful = true;
                view.Message = "Employee deleted succefully";
                LoadAllEmployees();
            }
            catch (Exception ex)
            {
                view.isSuccessful = false;
                view.Message = "An error ocurred";
            }
            //Thread.Sleep(1000);
            return;
        }
        private void SaveEmployee(object sender, EventArgs e)
        {
            var model = new EmployeeModel();
            model.Id_employee = Convert.ToInt32(view.id_employee);
            model.Surname = view.surname;
            model.Name = view.name;
            model.Patronymic = view.patronymic;
            model.Qualification = view.qualification;
            model.Id_activity = view.id_activity;
            model.Addition = view.addition;
            model.Salary = Convert.ToInt32(view.salary);

            try
            {
                new Common.ModelValidation().Validate(model);
                if (view.isEdit)
                {
                    repository.Edit(model);
                    view.Message = "Employee edit succesfuly";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Employee added succesfuly";
                }
                view.isSuccessful = true;
                LoadAllEmployees();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.isSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void LoadToEditEmployee(object sender, EventArgs e)
        {
            var employee = (EmployeeModel)employeesBindingSource.Current;
            view.id_employee = employee.Id_employee.ToString();
            view.surname = employee.Surname;
            view.name = employee.Name;
            view.patronymic = employee.Patronymic;
            view.qualification = employee.Qualification;
            view.id_activity = employee.Id_activity.ToString();
            view.addition = employee.Addition;
            view.salary = employee.Salary.ToString();
            view.isEdit = true;
        }
        private void AddEmployee(object sender, EventArgs e)
        {
            view.isEdit = false;
        }
    }
}
