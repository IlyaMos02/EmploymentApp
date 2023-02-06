using DataBasesLab1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DataBasesLab1._Repositories.Oracle
{
    internal class EmployeeRepositoryOracle : Base, IEmployeeRepository
    {
        public EmployeeRepositoryOracle(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(EmployeeModel employeeModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"insert into SYS.Employee (surname, name, patronymic, qualification, addition, salary, activity_id_activity) values ('{employeeModel.Surname}', '{employeeModel.Name}', '{employeeModel.Patronymic}', '{employeeModel.Qualification}', '{employeeModel.Addition}', {employeeModel.Salary}, {Convert.ToInt32(employeeModel.Id_activity)})";                           
                command.ExecuteNonQuery();

                SaveInFile.SaveEmployee("Добавлен элемент:\n" + command.CommandText, employeeModel);
            }
            var model = getEmployeeId();

            AddToAgreement(model);
        }
        public EmployeeModel getEmployeeId()
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                var EmployeeModel = new EmployeeModel();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Employee order by id_employee desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployeeModel.Id_employee = reader.GetInt32(0);
                        EmployeeModel.Surname = reader.GetString(1);
                        EmployeeModel.Name = reader.GetString(2);
                        EmployeeModel.Patronymic = reader.GetString(3);
                        EmployeeModel.Qualification = reader.GetString(4);                        
                        EmployeeModel.Addition = reader.GetString(5);
                        EmployeeModel.Salary = reader.GetInt32(6);
                        EmployeeModel.Id_activity = reader.GetString(7);
                        break;
                    }
                }

                return EmployeeModel;
            }
        }
        public void AddToAgreement(EmployeeModel employeeModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"Select * from SYS.Employer where activity_id_activity={Convert.ToInt32(employeeModel.Id_activity)}";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_employer = reader.GetString(0);
                        AgreementModel.Id_employee = employeeModel.Id_employee.ToString();
                        AgreementModel.Post = "-";
                        AgreementModel.Commission = 0;

                        new _Repositories.Oracle.AgreementRepositoryOracle(connectionString).Add(AgreementModel);
                    }
                }
            }
        }

        public void Delete(int idEmployee)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                var EmployeeModel = new EmployeeModel();
                using (var command2 = new OracleCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = $"Select * from SYS.Employee where id_employee={idEmployee}";

                    using (var reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeModel.Id_employee = reader.GetInt32(0);
                            EmployeeModel.Surname = reader.GetString(1);
                            EmployeeModel.Name = reader.GetString(2);
                            EmployeeModel.Patronymic = reader.GetString(3);
                            EmployeeModel.Qualification = reader.GetString(4);
                            EmployeeModel.Addition = reader.GetString(5);
                            EmployeeModel.Salary = reader.GetInt32(6);
                            EmployeeModel.Id_activity = reader.GetString(7);
                            break;
                        }
                    }
                }

                DeleteFromAgreement(idEmployee);

                command.CommandText = $"delete from SYS.Employee where id_employee={idEmployee}";           
                command.ExecuteNonQuery();
                SaveInFile.SaveEmployee("Удален элемент с id:" + idEmployee + "\n" + command.CommandText, EmployeeModel);                
            }
        }
        public void DeleteFromAgreement(int idEmployee)
        {
            new _Repositories.Oracle.AgreementRepositoryOracle(connectionString).DeleteEmployee(idEmployee);
        }

        public void Edit(EmployeeModel employeeModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;               
                command.CommandText = $"update SYS.Employee set surname='{employeeModel.Surname}', name='{employeeModel.Name}', patronymic='{employeeModel.Patronymic}', qualification='{employeeModel.Qualification}', addition='{employeeModel.Addition}', salary={employeeModel.Salary}, activity_id_activity={employeeModel.Id_activity} where id_employee={employeeModel.Id_employee}";              

                command.ExecuteNonQuery();
                SaveInFile.SaveEmployee("Изменен элемент с id:" + employeeModel.Id_employee + "\n" + command.CommandText, employeeModel);
                EditInAgreement(employeeModel);
            }
        }
        public void EditInAgreement(EmployeeModel employeeModel)
        {
            new _Repositories.Oracle.AgreementRepositoryOracle(connectionString).DeleteEmployee(employeeModel.Id_employee);

            AddToAgreement(employeeModel);            
        }


        public IEnumerable<EmployeeModel> GetAll()
        {
            var EmployeeList = new List<EmployeeModel>();

            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Employee order by id_employee desc";

                var command2 = new OracleCommand();
                command2.Connection = connection;
                command2.CommandText = "Select SYS.Activity.name from SYS.Activity Inner Join SYS.Employee On SYS.Employee.activity_id_activity = SYS.Activity.id_activity order by SYS.Employee.id_employee desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployeeModel = new EmployeeModel();
                        EmployeeModel.Id_employee = reader.GetInt32(0);
                        EmployeeModel.Surname = reader.GetString(1);
                        EmployeeModel.Name = reader.GetString(2);
                        EmployeeModel.Patronymic = reader.GetString(3);
                        EmployeeModel.Qualification = reader.GetString(4);
                        EmployeeModel.Addition = reader[5].ToString();
                        EmployeeModel.Salary = reader.GetInt32(6);                        

                        EmployeeList.Add(EmployeeModel);
                    }
                }

                using (var reader = command2.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        EmployeeList[i].Id_activity = reader.GetString(0);
                    }
                }
            }

            return EmployeeList;
        }

        public IEnumerable<EmployeeModel> GetByValue(string value)
        {
            var EmployeeList = new List<EmployeeModel>();
            int EmployeeId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string Surname = value;

            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Employee where id_employee=:id or surname like '%'||:surname||'%' order by id_employee desc";

                command.Parameters.Add(new OracleParameter("id", EmployeeId));
                command.Parameters.Add(new OracleParameter("surname", Surname));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployeeModel = new EmployeeModel();
                        EmployeeModel.Id_employee = reader.GetInt32(0);
                        EmployeeModel.Surname = reader.GetString(1);
                        EmployeeModel.Name = reader.GetString(2);
                        EmployeeModel.Patronymic = reader.GetString(3);
                        EmployeeModel.Qualification = reader.GetString(4);
                        EmployeeModel.Addition = reader.GetString(5);
                        EmployeeModel.Salary = reader.GetInt32(6);
                        EmployeeModel.Id_activity = reader.GetString(7);

                        EmployeeList.Add(EmployeeModel);
                    }
                }
            }

            return EmployeeList;
        }
    }
}
