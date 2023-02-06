using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1.Views
{
    public interface IEmployeeView
    {
        string id_employee { get; set; }
        string surname { get; set; }
        string name { get; set; }
        string patronymic { get; set; }
        string qualification { get; set; }
        string id_activity { get; set; }
        string addition { get; set; }
        string salary { get; set; }

        string SearchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetEmployeeBindingSource(BindingSource employeeList);
        void Show();
    }
}
