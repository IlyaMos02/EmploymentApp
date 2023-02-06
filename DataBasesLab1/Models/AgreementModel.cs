using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataBasesLab1.Models
{
    public class AgreementModel
    {
        private int id_agreement;
        private string id_employer;
        private string id_employee;
        private string post;
        private int commission;
        private string id_term;

        public int Id_agreement
        {
            get => id_agreement;
            set => id_agreement = value;
        }
        public string Id_employer
        {
            get => id_employer;
            set => id_employer = value;
        }
        public string Id_employee
        {
            get => id_employee;
            set => id_employee = value;
        }
        public string Post
        {
            get => post;
            set => post = value;
        }
        public int Commission
        {
            get => commission;
            set => commission = value;
        }
        public string Id_term { get => id_term; set => id_term = value; }
    }
}
