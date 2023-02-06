using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasesLab1.Views
{
    public interface IMainView
    {
        event EventHandler ShowEmployerView;
        event EventHandler ShowEmployeeView;
        event EventHandler ShowAgreementView;

        void Show();
    }
}
