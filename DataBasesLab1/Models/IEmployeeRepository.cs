using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasesLab1.Models
{
    public interface IEmployeeRepository
    {
        void Add(EmployeeModel employeeModel);
        void Edit(EmployeeModel employeeModel);
        void Delete(int idEmployee);
        IEnumerable<EmployeeModel> GetAll();
        IEnumerable<EmployeeModel> GetByValue(string value);
    }
}
