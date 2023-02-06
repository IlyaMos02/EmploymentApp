using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBasesLab1.Models;
using DataBasesLab1.Views;

namespace DataBasesLab1.Presenters
{
    public class AgreementPresenter
    {
        private IAgreementView view;
        private IAgreementRepository repository;
        private BindingSource agreementBindingSource;
        private IEnumerable<AgreementModel> agreementList;
        public AgreementPresenter(IAgreementView view, IAgreementRepository repository)
        {
            this.agreementBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            this.view.SearchEvent += SearchAgreement;            
            this.view.EditEvent += LoadToEditAgreement;           
            this.view.SaveEvent += SaveAgreement;
            this.view.CancelEvent += CancelAction;

            this.view.SetAgreementBindingSource(agreementBindingSource);

            LoadAllAgreements();

            
            this.view.Show();
        }

        private void LoadAllAgreements()
        {            
            agreementList = repository.GetAll();
            agreementBindingSource.DataSource = agreementList;
        }

        private void CleanViewFields()
        {
            view.id_agreement = "0";
            view.id_employer = "0";
            view.id_employee = "0";
            view.post = "";
            view.commission = "0";
            view.term = "0";
        }

        private void SearchAgreement(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                agreementList = repository.GetByValue(this.view.SearchValue);
            else
                agreementList = repository.GetAll();
            agreementBindingSource.DataSource = agreementList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveAgreement(object sender, EventArgs e)
        {
            var model = new AgreementModel();
            model.Id_agreement = Convert.ToInt32(view.id_agreement);
            model.Id_employer = view.id_employer;
            model.Id_employee = view.id_employee;
            model.Post = view.post;
            model.Commission = Convert.ToInt32(view.commission);
            model.Id_term= view.term;

            try
            {
                //new Common.ModelValidation().Validate(model);
                repository.Edit(model);
                view.Message = "Agreement edit succesfuly";
                view.isSuccessful = true;
                LoadAllAgreements();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.isSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void LoadToEditAgreement(object sender, EventArgs e)
        {
            var agreement = (AgreementModel)agreementBindingSource.Current;
            view.id_agreement = agreement.Id_agreement.ToString();
            view.id_employer = agreement.Id_employer.ToString();           
            view.id_employee = agreement.Id_employee.ToString();
            view.post = agreement.Post;
            view.commission = agreement.Commission.ToString();
            view.term = agreement.Id_term;
            view.isEdit = true;
        }
    }
}
