using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataBasesLab1.Models
{
    public class EmployerModel
    {
        //Fields
        private int id_employer;
        private string title;
        private string id_activity;
        private string adress;
        private string phone;

        //Properties
        [DisplayName("Employer ID")]
        public int Id_employer
        {
            get => id_employer;
            set => id_employer = value;
        }
        [DisplayName("Employer Title")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Title length must be between 3 and 30")]
        public string Title
        {
            get => title;
            set => title = value;
        }
        [DisplayName("Employer Activity")]

        public string Id_activity
        {
            get => id_activity;
            set => id_activity = value;
        }
        [DisplayName("Employer Adress")]
        [Required(ErrorMessage = "Adress is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Adress length must be between 3 and 30")]
        public string Adress
        {
            get => adress;
            set => adress = value;
        }
        [DisplayName("Employer Phone")]
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Phone length must be between 3 and 12")]
        public string Phone
        {
            get => phone;
            set => phone = value;
        }
    }
}
