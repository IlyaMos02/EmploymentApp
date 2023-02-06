using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataBasesLab1.Models
{
    public class EmployeeModel
    {
        private int id_employee;
        private string surname;
        private string name;
        private string patronymic;
        private string qualification;
        private string id_activity;
        private string addition;
        private int salary;

        //Properties
        [DisplayName("Employee ID")]
        public int Id_employee
        {
            get => id_employee;
            set => id_employee = value;
        }
        [DisplayName("Employee Surname")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Surname length must be between 2 and 30")]
        public string Surname
        {
            get => surname;
            set => surname = value;
        }
        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name length must be between 2 and 30")]
        public string Name
        {
            get => name;
            set => name = value;
        }
        [DisplayName("Employee Patronymic")]
        [Required(ErrorMessage = "Patronymic is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Patronymic length must be between 2 and 30")]
        public string Patronymic
        {
            get => patronymic;
            set => patronymic = value;
        }
        [DisplayName("Employee Qualification")]
        [Required(ErrorMessage = "Qualification is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Qualification length must be between 3 and 30")]
        public string Qualification
        {
            get => qualification;
            set => qualification = value;
        }
        [DisplayName("Employee Activity")]
        public string Id_activity
        {
            get => id_activity;
            set => id_activity = value;
        }
        [DisplayName("Employee Addition")]        
        public string Addition
        {
            get => addition;
            set => addition = value;
        }
        [DisplayName("Employee Salary")]
        public int Salary
        {
            get => salary;
            set => salary = value;
        }
    }
}
