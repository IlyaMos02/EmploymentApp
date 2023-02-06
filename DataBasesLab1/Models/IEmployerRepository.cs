using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasesLab1.Models
{
    public interface IEmployerRepository
    {
        void Add(EmployerModel employerModel);
        void Edit(EmployerModel employerModel);
        void Delete(int idEmployer);
        IEnumerable<EmployerModel> GetAll();
        IEnumerable<EmployerModel> GetByValue(string value);
    }
}
