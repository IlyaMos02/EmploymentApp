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
    public class EmployerPresenter 
    {
        private IEmployerView view;
        private IEmployerRepository repository;
        private BindingSource employersBindingSource;
        private IEnumerable<EmployerModel> employerList;

        public EmployerPresenter(IEmployerView view, IEmployerRepository repository)
        {
            this.employersBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent -= this.SearchEmployer;
            this.view.AddNewEvent -= this.AddEmployer;
            this.view.EditEvent -= this.LoadToEditEmployer;
            this.view.DeleteEvent -= DeleteEmployer;
            this.view.SaveEvent -= this.SaveEmployer;
            this.view.CancelEvent -= this.CancelAction;

            this.view.SearchEvent += this.SearchEmployer;
            this.view.AddNewEvent += this.AddEmployer;
            this.view.EditEvent += this.LoadToEditEmployer;
            this.view.DeleteEvent += this.DeleteEmployer;
            this.view.SaveEvent += this.SaveEmployer;
            this.view.CancelEvent += this.CancelAction;

            this.view.SetEmployerBindingSource(employersBindingSource);

            LoadAllEmployers();

            this.view.Show();            
        }

        private void LoadAllEmployers()
        {
            employerList = repository.GetAll();
            employersBindingSource.DataSource = employerList;
        }

        private void CleanViewFields()
        {
            view.id_employer = "0";
            view.title = "";
            view.id_activity = "0";
            view.adress = "";
            view.phone = "";
        }

        private void SearchEmployer(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                employerList = repository.GetByValue(this.view.SearchValue);
            else
                employerList = repository.GetAll();
            employersBindingSource.DataSource = employerList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }       
        private void DeleteEmployer(object sender, EventArgs e)
        {
            try
            {
                var employer = (EmployerModel)employersBindingSource.Current;
                repository.Delete(employer.Id_employer);
                view.isSuccessful = true;
                view.Message = "Employer deleted succefully";
                LoadAllEmployers();
            }
            catch (Exception ex)
            {
                view.isSuccessful = false;
                view.Message = "An error ocurred";
            }
            //Thread.Sleep(1000);
            return;
        }
        private void SaveEmployer(object sender, EventArgs e)
        {
            var model = new EmployerModel();
            model.Id_employer = Convert.ToInt32(view.id_employer);
            model.Title = view.title;            
            model.Adress = view.adress;
            model.Phone = view.phone;
            model.Id_activity = view.id_activity;

            try
            {
                new Common.ModelValidation().Validate(model);
                if(view.isEdit)
                {
                    repository.Edit(model);
                    view.Message = "Employer edit succesfuly";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Employer added succesfuly";
                }
                view.isSuccessful = true;
                LoadAllEmployers();
                CleanViewFields();
            }
            catch(Exception ex)
            {
                view.isSuccessful = false;
                view.Message = ex.Message;
            }
        }        

        private void LoadToEditEmployer(object sender, EventArgs e)
        {
            var employer = (EmployerModel)employersBindingSource.Current;
            view.id_employer = employer.Id_employer.ToString();
            view.title = employer.Title;
            view.id_activity = employer.Id_activity.ToString();
            view.adress = employer.Adress;
            view.phone = employer.Phone;
            view.isEdit = true;
        }
        private void AddEmployer(object sender, EventArgs e)
        {
            view.isEdit = false;
        }
       
    }
}
