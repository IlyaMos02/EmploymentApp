using DataBasesLab1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBasesLab1._Repositories
{
    public static class SaveInFile
    {
        public static void SaveEmployer(string command, EmployerModel model)
        {
            string path = "C:\\Users\\GameMax\\source\\repos\\DataBasesLab1\\DataBasesLab1\\_Repositories\\Logs.txt";
            string text = command + "\n" + model.Title + "\t" + model.Id_activity + "\t" + model.Adress + "\t" + model.Phone + "\t" + DateTime.Now;

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(text);
            }
        }

        public static void SaveEmployee(string command, EmployeeModel model)
        {
            string path = "C:\\Users\\GameMax\\source\\repos\\DataBasesLab1\\DataBasesLab1\\_Repositories\\Logs.txt";
            string text = command + "\n" + model.Name + "\t" + model.Surname + "\t" + model.Patronymic + "\t" + model.Qualification + "\t" + model.Id_activity + "\t" + model.Addition + "\t" + model.Salary + "\t" + DateTime.Now;

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(text);
            }
        }

        public static void SaveAgreement(string command, AgreementModel model)
        {
            string path = "C:\\Users\\GameMax\\source\\repos\\DataBasesLab1\\DataBasesLab1\\_Repositories\\Logs.txt";
            string text = command + "\n" + model.Id_employer + "\t" + model.Id_employee + "\t" + model.Post + "\t" + model.Commission + "\t" + model.Id_term + "\t" + DateTime.Now;

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLineAsync(text);
            }
        }
    }
}
