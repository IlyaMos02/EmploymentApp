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
    public class AgreementRepository : Base, IAgreementRepository
    {
        public AgreementRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(AgreementModel agreementModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Agreement values (@id_employer, @id_employee, @post, @commission)";
                command.Parameters.Add("@id_employer", SqlDbType.Int).Value = agreementModel.Id_employer;
                command.Parameters.Add("@id_employee", SqlDbType.Int).Value = agreementModel.Id_employee;
                command.Parameters.Add("@post", SqlDbType.VarChar).Value = agreementModel.Post;
                command.Parameters.Add("@commission", SqlDbType.Money).Value = agreementModel.Commission;
                command.ExecuteNonQuery();
                SaveInFile.SaveAgreement("Добавлен элемент:\n" + command.CommandText, agreementModel);
            }
        }

        public void Edit(AgreementModel agreementModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "update Agreement set post=@post, commission=@commission where id_agreement=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = agreementModel.Id_agreement;
                command.Parameters.Add("@post", SqlDbType.VarChar).Value = agreementModel.Post;
                command.Parameters.Add("@commission", SqlDbType.Money).Value = agreementModel.Commission;
                command.ExecuteNonQuery();
                SaveInFile.SaveAgreement("Изменен элемент с id:" + agreementModel.Id_agreement + "\n" + command.CommandText, agreementModel);
            }
        }       

        public void DeleteEmployer(int idEmployer)
        {
            //var AgreementList = new List<AgreementModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Agreement where id_employer=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = idEmployer;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = (int)reader[0];
                        AgreementModel.Id_employer = reader.GetString(1);
                        AgreementModel.Id_employee = reader.GetString(2);
                        AgreementModel.Post = reader[3].ToString();
                        AgreementModel.Commission = Convert.ToInt32(reader[4]);

                        SaveInFile.SaveAgreement("Удален элемент с id:" + AgreementModel.Id_agreement + "\n" + command.CommandText, AgreementModel);
                    }
                }

                command.CommandText = "delete from Agreement where id_employer=@id";               
                command.ExecuteNonQuery();                
            }
        }

        public void DeleteEmployee(int idEmployee)
        {           
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Agreement where id_employee=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = idEmployee;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = (int)reader[0];
                        AgreementModel.Id_employer = reader.GetString(1);
                        AgreementModel.Id_employee = reader.GetString(2);
                        AgreementModel.Post = reader[3].ToString();
                        AgreementModel.Commission = Convert.ToInt32(reader[4]);

                        SaveInFile.SaveAgreement("Удален элемент с id:" + AgreementModel.Id_agreement + "\n" + command.CommandText, AgreementModel);
                    }
                }

                command.CommandText = "delete from Agreement where id_employee=@id";                
                command.ExecuteNonQuery();               
            }
        }

        public IEnumerable<AgreementModel> GetAll()
        {
            var AgreementList = new List<AgreementModel>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {           
                var command2 = new SqlCommand();                
                connection.Open();
                command.Connection = connection;
                command2.Connection = connection;
                
                command.CommandText = "Select * from Agreement order by id_agreement desc";
                command2.CommandText = "Select Employer.title, Employee.name From Employer Inner Join Agreement On Agreement.id_employer = Employer.id_employer Inner Join Employee On Agreement.id_employee = Employee.id_employee";

                using (var reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = (int)reader[0];                       
                        AgreementModel.Post = reader[3].ToString();
                        AgreementModel.Commission = Convert.ToInt32(reader[4]);

                        AgreementList.Add(AgreementModel);
                    }
                }

                using (var reader2 = command2.ExecuteReader())
                {
                    for (int i = 0; reader2.Read(); i++) 
                    {
                        AgreementList[i].Id_employer = reader2.GetString(0);
                        AgreementList[i].Id_employee = reader2.GetString(1);
                    }
                }
            }

            return AgreementList;
        }       

        public IEnumerable<AgreementModel> GetByValue(string value)
        {
            var AgreementList = new List<AgreementModel>();
            int AgreementId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string Post = value;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from Agreement where id_agreeement=@id or post like '%'+@post+'%' order by id_agreeement desc";

                command.Parameters.Add("@id", SqlDbType.Int).Value = AgreementId;
                command.Parameters.Add("@post", SqlDbType.VarChar).Value = Post;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = (int)reader[0];
                        AgreementModel.Id_employer = reader.GetString(1);
                        AgreementModel.Id_employee = reader.GetString(2);
                        AgreementModel.Post = reader[3].ToString();
                        AgreementModel.Commission = Convert.ToInt32(reader[4]);

                        AgreementList.Add(AgreementModel);
                    }
                }
            }
            return AgreementList;
        }
    }
}
