using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataBasesLab1.Models;

namespace DataBasesLab1._Repositories
{
    public class EmployeeRepository :Base, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(EmployeeModel employeeModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Employee values (@surname, @name, @patronymic, @qualification, @id_activity, @addition, @salary)";
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = employeeModel.Surname;
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = employeeModel.Name;
                command.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = employeeModel.Patronymic;
                command.Parameters.Add("@qualification", SqlDbType.VarChar).Value = employeeModel.Qualification;
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employeeModel.Id_activity;
                command.Parameters.Add("@addition", SqlDbType.VarChar).Value = employeeModel.Addition;
                command.Parameters.Add("@salary", SqlDbType.Money).Value = employeeModel.Salary;
                command.ExecuteNonQuery();

                SaveInFile.SaveEmployee("Добавлен элемент:\n" + command.CommandText, employeeModel);
            }
            var model = getEmployeeId();

            AddToAgreement(model);            
        }
        public EmployeeModel getEmployeeId()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                var EmployeeModel = new EmployeeModel();
                command.Connection = connection;
                command.CommandText = "Select * from Employee order by id_employee desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                        
                        EmployeeModel.Id_employee = (int)reader[0];
                        EmployeeModel.Surname = reader[1].ToString();
                        EmployeeModel.Name = reader[2].ToString();
                        EmployeeModel.Patronymic = reader[3].ToString();
                        EmployeeModel.Qualification = reader[4].ToString();
                        EmployeeModel.Id_activity = reader.GetString(5);
                        EmployeeModel.Addition = reader[6].ToString();
                        EmployeeModel.Salary = Convert.ToInt32(reader[7]);
                        break;
                    }
                }

                return EmployeeModel;
            }
        }
        public void AddToAgreement(EmployeeModel employeeModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employer where id_activity=@id_activity";
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employeeModel.Id_activity;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_employer = reader.GetString(0);
                        AgreementModel.Id_employee = employeeModel.Id_employee.ToString();
                        AgreementModel.Post = "-";
                        AgreementModel.Commission = 0;

                        new _Repositories.AgreementRepository(connectionString).Add(AgreementModel);
                    }
                }
            }
        }

        public void Delete(int idEmployee)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                var EmployeeModel = new EmployeeModel();
                using (var command2 = new SqlCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = "Select * from Employee where id_employee=@id";

                    command2.Parameters.Add("@id", SqlDbType.Int).Value = idEmployee;
                    using (var reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployeeModel.Id_employee = (int)reader[0];
                            EmployeeModel.Surname = reader[1].ToString();
                            EmployeeModel.Name = reader[2].ToString();
                            EmployeeModel.Patronymic = reader[3].ToString();
                            EmployeeModel.Qualification = reader[4].ToString();
                            EmployeeModel.Id_activity = reader.GetString(5);
                            EmployeeModel.Addition = reader[6].ToString();
                            EmployeeModel.Salary = Convert.ToInt32(reader[7]);
                        }
                    }
                }

                command.CommandText = "delete from Employee where id_employee=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = idEmployee;
                command.ExecuteNonQuery();
                SaveInFile.SaveEmployee("Удален элемент с id:" + idEmployee + "\n" + command.CommandText, EmployeeModel);
                DeleteFromAgreement(idEmployee);
            }
        }
        public void DeleteFromAgreement(int idEmployee)
        {
            new _Repositories.AgreementRepository(connectionString).DeleteEmployee(idEmployee);
        }

        public void Edit(EmployeeModel employeeModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Employee set surname=@surname, name=@name, patronymic=@patronymic, qualification=@qualification, id_activity=@id_activity, addition=@addition, salary=@salary where id_employee=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = employeeModel.Id_employee;
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = employeeModel.Surname;
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = employeeModel.Name;
                command.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = employeeModel.Patronymic;
                command.Parameters.Add("@qualification", SqlDbType.VarChar).Value = employeeModel.Qualification;
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employeeModel.Id_activity;
                command.Parameters.Add("@addition", SqlDbType.VarChar).Value = employeeModel.Addition;
                command.Parameters.Add("@salary", SqlDbType.Money).Value = employeeModel.Salary;

                command.ExecuteNonQuery();
                SaveInFile.SaveEmployee("Изменен элемент с id:" + employeeModel.Id_employee + "\n" + command.CommandText, employeeModel);
                EditInAgreement(employeeModel);
            }

            
        }
        public void EditInAgreement(EmployeeModel employeeModel)
        {
            new _Repositories.AgreementRepository(connectionString).DeleteEmployee(employeeModel.Id_employee);

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employer where id_activity=@id_activity";
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employeeModel.Id_activity;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_employer = reader.GetString(0);
                        AgreementModel.Id_employee = employeeModel.Id_employee.ToString();
                        AgreementModel.Post = "-";
                        AgreementModel.Commission = 0;
                        
                        new _Repositories.AgreementRepository(connectionString).Add(AgreementModel);
                    }
                }
            }
        }


        public IEnumerable<EmployeeModel> GetAll()
        {
            var EmployeeList = new List<EmployeeModel>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employee order by id_employee desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployeeModel = new EmployeeModel();
                        EmployeeModel.Id_employee = (int)reader[0];
                        EmployeeModel.Surname = reader[1].ToString();
                        EmployeeModel.Name = reader[2].ToString();
                        EmployeeModel.Patronymic = reader[3].ToString();
                        EmployeeModel.Qualification = reader[4].ToString();
                        EmployeeModel.Id_activity = reader.GetString(5);
                        EmployeeModel.Addition = reader[6].ToString();
                        EmployeeModel.Salary = Convert.ToInt32(reader[7]);

                        EmployeeList.Add(EmployeeModel);
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

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employee where id_employee=@id or surname like '%'+@surname+'%' order by id_employee desc";

                command.Parameters.Add("@id", SqlDbType.Int).Value = EmployeeId;
                command.Parameters.Add("@surname", SqlDbType.VarChar).Value = Surname;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployeeModel = new EmployeeModel();
                        EmployeeModel.Id_employee = (int)reader[0];
                        EmployeeModel.Surname = reader[1].ToString();
                        EmployeeModel.Name = reader[2].ToString();
                        EmployeeModel.Patronymic = reader[3].ToString();
                        EmployeeModel.Qualification = reader[4].ToString();
                        EmployeeModel.Id_activity = reader.GetString(5);
                        EmployeeModel.Addition = reader[6].ToString();
                        EmployeeModel.Salary = Convert.ToInt32(reader[7]);

                        EmployeeList.Add(EmployeeModel);
                    }
                }
            }

            return EmployeeList;
        }
    }
}
