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
    internal class EmployerRepositoryOracle : Base, IEmployerRepository
    {
        public EmployerRepositoryOracle(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(EmployerModel employerModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"insert into SYS.Employer (title, adress, phone, activity_id_activity) values ('{employerModel.Title}', '{employerModel.Adress}', '{employerModel.Phone}', {Convert.ToInt32(employerModel.Id_activity)})";                               
                
                command.ExecuteNonQuery();
                SaveInFile.SaveEmployer("Добавлен элемент:\n" + command.CommandText, employerModel);
            }
            var model = getEmployerID();

            AddToAgreement(model);
        }
        public EmployerModel getEmployerID()
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                var EmployerModel = new EmployerModel();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Employer order by id_employer desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployerModel.Id_employer = reader.GetInt32(0);
                        EmployerModel.Title = reader.GetString(1);
                        EmployerModel.Adress = reader.GetString(2);
                        EmployerModel.Phone = reader.GetString(3);
                        EmployerModel.Id_activity = reader.GetString(4);
                        break;
                    }
                }

                return EmployerModel;
            }
        }
        public void AddToAgreement(EmployerModel employerModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"Select * from SYS.Employee where activity_id_activity={Convert.ToInt32(employerModel.Id_activity)}";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_employer = employerModel.Id_employer.ToString();
                        AgreementModel.Id_employee = reader.GetString(0);
                        AgreementModel.Post = "-";
                        AgreementModel.Commission = 0;

                        new _Repositories.Oracle.AgreementRepositoryOracle(connectionString).Add(AgreementModel);
                    }
                }
            }
        }


        public void Delete(int idEmployer)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                var EmployerModel = new EmployerModel();
                using (var command2 = new OracleCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = $"Select * from SYS.Employer where id_employer={idEmployer}";
                  
                    using (var reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployerModel.Id_employer = reader.GetInt32(0);
                            EmployerModel.Title = reader.GetString(1);
                            EmployerModel.Adress = reader.GetString(2);
                            EmployerModel.Phone = reader.GetString(3);
                            EmployerModel.Id_activity = reader.GetString(4);
                            break;
                        }
                    }
                }

                command.CommandText = $"delete from SYS.Employer where id_employer={idEmployer}";                
                DeleteFromAgreement(idEmployer);
                command.ExecuteNonQuery();
                SaveInFile.SaveEmployer("Удален элемент с id:" + idEmployer + "\n" + command.CommandText, EmployerModel);                
            }
        }
        public void DeleteFromAgreement(int idEmployer)
        {
            new _Repositories.Oracle.AgreementRepositoryOracle(connectionString).DeleteEmployer(idEmployer);
        }


        public void Edit(EmployerModel employerModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"update SYS.Employer set title='{employerModel.Title}', adress='{employerModel.Adress}', phone='{employerModel.Phone}', activity_id_activity={Convert.ToInt32(employerModel.Id_activity)} where id_employer={employerModel.Id_employer}";                

                command.ExecuteNonQuery();
                SaveInFile.SaveEmployer("Изменен элемент с id:" + employerModel.Id_employer + "\n" + command.CommandText, employerModel);
                EditInAgreement(employerModel);
            }
        }
        public void EditInAgreement(EmployerModel employerModel)
        {
            new _Repositories.Oracle.AgreementRepositoryOracle(connectionString).DeleteEmployer(employerModel.Id_employer);

            AddToAgreement(employerModel);           
        }

        public IEnumerable<EmployerModel> GetAll()
        {
            var EmployerList = new List<EmployerModel>();

            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Employer order by id_employer desc";

                var command2 = new OracleCommand();
                command2.Connection = connection;
                command2.CommandText = "Select SYS.Activity.name from SYS.Activity Inner Join SYS.Employer On SYS.Employer.activity_id_activity = SYS.Activity.id_activity order by SYS.Employer.id_employer desc";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployerModel = new EmployerModel();
                        EmployerModel.Id_employer = reader.GetInt32(0);
                        EmployerModel.Title = reader.GetString(1);                       
                        EmployerModel.Adress = reader.GetString(2);
                        EmployerModel.Phone = reader.GetString(3);

                        EmployerList.Add(EmployerModel);
                    }
                }

                using (var reader = command2.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        EmployerList[i].Id_activity = reader.GetString(0);
                    }
                }
            }

            return EmployerList;
        }

        public IEnumerable<EmployerModel> GetByValue(string value)
        {
            var EmployerList = new List<EmployerModel>();
            int EmployerId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string Title = value;

            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Employer where id_employer=:id or title like '%'||:title||'%' order by id_employer desc";             
                command.Parameters.Add(new OracleParameter("id", EmployerId));
                command.Parameters.Add(new OracleParameter("title", Title));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployerModel = new EmployerModel();
                        EmployerModel.Id_employer = reader.GetInt32(0);
                        EmployerModel.Title = reader.GetString(1);                        
                        EmployerModel.Adress = reader.GetString(2);
                        EmployerModel.Phone = reader.GetString(3);
                        EmployerModel.Id_activity = reader.GetString(4);

                        EmployerList.Add(EmployerModel);
                    }
                }
            }

            return EmployerList;
        }
    }
}
