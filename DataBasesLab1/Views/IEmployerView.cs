using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1.Views
{
    public interface IEmployerView
    {
        string id_employer { get; set; }
        string title { get; set; }
        string id_activity { get; set; }
        string adress { get; set; }
        string phone { get; set; }

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
        event EventHandler RoleEvent;
        void SetEmployerBindingSource(BindingSource employerList);
        void Show();
    }
}
