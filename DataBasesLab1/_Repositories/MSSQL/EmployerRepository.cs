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
    public class EmployerRepository : Base, IEmployerRepository
    {
        public EmployerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(EmployerModel employerModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Employer values (@title, @id_activity, @adress, @phone)";
                command.Parameters.Add("@title", SqlDbType.VarChar).Value = employerModel.Title;
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employerModel.Id_activity;
                command.Parameters.Add("@adress", SqlDbType.VarChar).Value = employerModel.Adress;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = employerModel.Phone;

             
                command.ExecuteNonQuery();
                SaveInFile.SaveEmployer("Добавлен элемент:\n" + command.CommandText, employerModel);                
            }
            var model = getEmployerID();

            AddToAgreement(model);
        }
        public EmployerModel getEmployerID()
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                var EmployerModel = new EmployerModel();
                command.Connection = connection;
                command.CommandText = "Select * from Employer order by id_employer desc";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployerModel.Id_employer = (int)reader[0];
                        EmployerModel.Title = reader[1].ToString();
                        EmployerModel.Id_activity = reader.GetString(2);
                        EmployerModel.Adress = reader[3].ToString();
                        EmployerModel.Phone = reader[4].ToString();
                        break;
                    }
                }

                return EmployerModel;
            }
        }
        public void AddToAgreement(EmployerModel employerModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employee where id_activity=@id_activity";
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employerModel.Id_activity;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_employer = employerModel.Id_employer.ToString();
                        AgreementModel.Id_employee = reader.GetString(0);
                        AgreementModel.Post = "-";
                        AgreementModel.Commission = 0;

                        new _Repositories.AgreementRepository(connectionString).Add(AgreementModel);                        
                    }
                }
            }
        }


        public void Delete(int idEmployer)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                var EmployerModel = new EmployerModel();
                using (var command2 = new SqlCommand())
                {
                    command2.Connection = connection;
                    command2.CommandText = "Select * from Employer where id_employer=@id";

                    command2.Parameters.Add("@id", SqlDbType.Int).Value = idEmployer;                   
                    using (var reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EmployerModel.Id_employer = (int)reader[0];
                            EmployerModel.Title = reader[1].ToString();
                            EmployerModel.Id_activity = reader.GetString(2);
                            EmployerModel.Adress = reader[3].ToString();
                            EmployerModel.Phone = reader[4].ToString();
                        }
                    }
                }                   

                command.CommandText = "delete from Employer where id_employer=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = idEmployer;              
                command.ExecuteNonQuery();
                SaveInFile.SaveEmployer("Удален элемент с id:" + idEmployer + "\n" + command.CommandText, EmployerModel);
                DeleteFromAgreement(idEmployer);
            }
        }
        public void DeleteFromAgreement(int idEmployer)
        {
            new _Repositories.AgreementRepository(connectionString).DeleteEmployer(idEmployer);
        }


        public void Edit(EmployerModel employerModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Employer set title=@title, id_activity=@id_activity, adress=@adress, phone=@phone where id_employer=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = employerModel.Id_employer;
                command.Parameters.Add("@title", SqlDbType.VarChar).Value = employerModel.Title;
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employerModel.Id_activity;
                command.Parameters.Add("@adress", SqlDbType.VarChar).Value = employerModel.Adress;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = employerModel.Phone;

                command.ExecuteNonQuery();
                SaveInFile.SaveEmployer("Изменен элемент с id:" + employerModel.Id_employer + "\n" + command.CommandText, employerModel);
                EditInAgreement(employerModel);
            }          
        }
        public void EditInAgreement(EmployerModel employerModel)
        {
            new _Repositories.AgreementRepository(connectionString).DeleteEmployer(employerModel.Id_employer);

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employee where id_activity=@id_activity";
                command.Parameters.Add("@id_activity", SqlDbType.Int).Value = employerModel.Id_activity;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_employer = employerModel.Id_employer.ToString();
                        AgreementModel.Id_employee = reader.GetString(0);
                        AgreementModel.Post = "-";
                        AgreementModel.Commission = 0;
                        
                        new _Repositories.AgreementRepository(connectionString).Add(AgreementModel);                        
                    }
                }
            }
        }

        public IEnumerable<EmployerModel> GetAll()
        {
            var EmployerList = new List<EmployerModel>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employer order by id_employer desc";

                using (var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var EmployerModel = new EmployerModel();
                        EmployerModel.Id_employer = (int)reader[0];
                        EmployerModel.Title = reader[1].ToString();
                        EmployerModel.Id_activity = reader.GetString(2);
                        EmployerModel.Adress = reader[3].ToString();
                        EmployerModel.Phone = reader[4].ToString();

                        EmployerList.Add(EmployerModel);
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
        
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Employer where id_employer=@id or title like '%'+@title+'%' order by id_employer desc";

                command.Parameters.Add("@id", SqlDbType.Int).Value = EmployerId;
                command.Parameters.Add("@title", SqlDbType.VarChar).Value = Title;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployerModel = new EmployerModel();
                        EmployerModel.Id_employer = (int)reader[0];
                        EmployerModel.Title = reader[1].ToString();
                        EmployerModel.Id_activity = reader.GetString(2);
                        EmployerModel.Adress = reader[3].ToString();
                        EmployerModel.Phone = reader[4].ToString();

                        EmployerList.Add(EmployerModel);
                    }
                }
            }

            return EmployerList;
        }
    }
}
