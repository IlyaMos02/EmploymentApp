using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBasesLab1.Models;
using DataBasesLab1.Views;
using DataBasesLab1._Repositories;
using DataBasesLab1._Repositories.Oracle;
using System.Windows.Forms;

namespace DataBasesLab1.Presenters
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnection;
        private string role;

        public MainPresenter(IMainView mainView, string sqlConnection, string role)
        {
            this.mainView = mainView;
            this.sqlConnection = sqlConnection;
            this.role = role;            
            this.mainView.ShowEmployerView += ShowEmployersView;
            this.mainView.ShowEmployeeView += ShowEmployeesView;
            this.mainView.ShowAgreementView += ShowAgreementView;

            this.mainView.Show();
        }

        private void ShowAgreementView(object sender, EventArgs e)
        {
            IAgreementView view = AgreementView.GetInstance((MainView)mainView, role);
            IAgreementRepository repository = new AgreementRepositoryOracle(sqlConnection);
            new AgreementPresenter(view, repository);
        }

        private void ShowEmployersView(object sender, EventArgs e)
        {
            IEmployerView view = EmployerForm.GetInstance((MainView)mainView, role);
            IEmployerRepository repository = new EmployerRepositoryOracle(sqlConnection);
            new EmployerPresenter(view, repository);
        }

        private void ShowEmployeesView(object sender, EventArgs e)
        {
            IEmployeeView view = EmployeeView.GetInstance((MainView)mainView, role);
            IEmployeeRepository repository = new EmployeeRepositoryOracle(sqlConnection);
            new EmployeePresenter(view, repository);
        }
    }
}
