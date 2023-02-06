using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasesLab1.Models
{
    public interface IAgreementRepository
    {
        void Add(AgreementModel agreementModel);
        void Edit(AgreementModel agreementModel);
        void DeleteEmployer(int idEmployer);
        void DeleteEmployee(int idEmployee);
        //void RefreshAllComponents();
        IEnumerable<AgreementModel> GetAll();
        IEnumerable<AgreementModel> GetByValue(string value);
    }
}
