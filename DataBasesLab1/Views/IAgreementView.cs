using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBasesLab1.Views
{
    public interface IAgreementView
    {
        string id_agreement { get; set; }
        string id_employer { get; set; }
        string id_employee { get; set; }
        string post { get; set; }
        string commission { get; set; }
        string term { get; set; }
        string SearchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler EditEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetAgreementBindingSource(BindingSource agreementList);
        void Show();
    }
}
