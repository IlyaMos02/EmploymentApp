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
    internal class AgreementRepositoryOracle : Base, IAgreementRepository
    {
        public AgreementRepositoryOracle(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(AgreementModel agreementModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                Random rand = new Random();

                connection.Open();
                command.Connection = connection;
                command.CommandText = $"insert into SYS.Agreement (post, commission, term_id_term, employee_id_employee, employer_id_employer) values ('{agreementModel.Post}', {agreementModel.Commission}, {rand.Next(1, 7)}, {agreementModel.Id_employee}, {agreementModel.Id_employer})";                
               
                command.ExecuteNonQuery();
                SaveInFile.SaveAgreement("Добавлен элемент:\n" + command.CommandText, agreementModel);
            }
        }

        public void Edit(AgreementModel agreementModel)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"update SYS.Agreement set post='{agreementModel.Post}', commission={agreementModel.Commission} where id_agreement={agreementModel.Id_agreement}";
                
                command.ExecuteNonQuery();
                SaveInFile.SaveAgreement("Изменен элемент с id:" + agreementModel.Id_agreement + "\n" + command.CommandText, agreementModel);
            }
        }

        public void DeleteEmployer(int idEmployer)
        {            
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"Select * from SYS.Agreement where employer_id_employer={idEmployer}";
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = reader.GetInt32(0);                       
                        AgreementModel.Post = reader.GetString(1);
                        AgreementModel.Commission = reader.GetInt32(2);
                        AgreementModel.Id_term = reader.GetString(3);
                        AgreementModel.Id_employee = reader.GetString(4);
                        AgreementModel.Id_employer = reader.GetString(5);

                        SaveInFile.SaveAgreement("Удален элемент с id:" + AgreementModel.Id_agreement + "\n" + command.CommandText, AgreementModel);
                    }
                }

                command.CommandText = $"delete from SYS.Agreement where employer_id_employer={idEmployer}";                
                command.ExecuteNonQuery();                
            }
        }

        public void DeleteEmployee(int idEmployee)
        {
            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = $"Select * from SYS.Agreement where employee_id_employee={idEmployee}";
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = reader.GetInt32(0);
                        AgreementModel.Post = reader.GetString(1);
                        AgreementModel.Commission = reader.GetInt32(2);
                        AgreementModel.Id_term = reader.GetString(3);
                        AgreementModel.Id_employee = reader.GetString(4);
                        AgreementModel.Id_employer = reader.GetString(5);

                        SaveInFile.SaveAgreement("Удален элемент с id:" + AgreementModel.Id_agreement + "\n" + command.CommandText, AgreementModel);
                    }
                }

                command.CommandText = $"delete from SYS.Agreement where employee_id_employee={idEmployee}";
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<AgreementModel> GetAll()
        {
            var AgreementList = new List<AgreementModel>();

            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                var command2 = new OracleCommand();
                connection.Open();
                command.Connection = connection;
                command2.Connection = connection;

                command.CommandText = "Select * from SYS.Agreement order by id_agreement desc";
                command2.CommandText = "Select Employer.title, Employee.name, Term.time From Employer Inner Join Agreement On Agreement.employer_id_employer = Employer.id_employer Inner Join Employee On Agreement.employee_id_employee = Employee.id_employee Inner Join Term On Agreement.term_id_term = Term.id_term order by Agreement.id_agreement desc";

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = reader.GetInt32(0);
                        AgreementModel.Post = reader.GetString(1);
                        AgreementModel.Commission = reader.GetInt32(2);

                        AgreementList.Add(AgreementModel);
                    }
                }

                using (var reader = command2.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        AgreementList[i].Id_employee = reader.GetString(1);
                        AgreementList[i].Id_employer = reader.GetString(0);
                        AgreementList[i].Id_term = reader.GetString(2);
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

            using (var connection = new OracleConnection(connectionString))
            using (var command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "Select * from SYS.Agreement where id_agreement=:id or post like '%'||:post||'%' order by id_agreement desc";
                command.Parameters.Add(new OracleParameter("id", AgreementId));
                command.Parameters.Add(new OracleParameter("post", Post));                

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var AgreementModel = new AgreementModel();
                        AgreementModel.Id_agreement = reader.GetInt32(0);
                        AgreementModel.Post = reader.GetString(1);
                        AgreementModel.Commission = reader.GetInt32(2);
                        AgreementModel.Id_term = reader.GetString(3);
                        AgreementModel.Id_employer = reader.GetString(4);
                        AgreementModel.Id_employee = reader.GetString(5);

                        AgreementList.Add(AgreementModel);
                    }
                }
            }
            return AgreementList;
        }
    }
}
