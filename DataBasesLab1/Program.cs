using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBasesLab1.Models;
using DataBasesLab1.Presenters;
using DataBasesLab1.Views;
using DataBasesLab1._Repositories;
using System.Configuration;

namespace DataBasesLab1
{
    static class Program
    {        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnection = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;            
            Application.Run(/*(Form)view*/new ClientForm());
        }
    }
}
