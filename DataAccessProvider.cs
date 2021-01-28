using PSQLConection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;

namespace PSQLConection.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        public NpgsqlConnection _conect;
        public NpgsqlCommand _cmd;
        public string _sql;

        public DataAccessProvider()
        {
            _conect = new NpgsqlConnection(GetConnectionString());
            _cmd = default(NpgsqlCommand);
            _sql = string.Empty;
        }
        public void AddPersonRecord(Models.Person person)
        {
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(GetConnectionString());
            try
            {
                using (pgsqlConnection)
                {                
                    pgsqlConnection.Open();
                    string cmdUpdate = String.Format("Insert Into person(name, date_of_birth, cpf) values('{0}','{1}','{2}')", person.Name, person.BirthDate, person.CPF );
                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdUpdate, pgsqlConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        public void UpdatePersonRecord(Person person, Person newPerson)
        {
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(GetConnectionString());
            try
            {
                using (pgsqlConnection)
                {
                    //Abra a conexão com o PgSQL                  
                    pgsqlConnection.Open();
                    string cmdUpdate = String.Format("Update person Set name = '"  + newPerson.Name + "', cpf = '" + newPerson.CPF+ "', date_of_birth = '"+ newPerson.BirthDate +"' WHERE id=" + person.Id);
                    using (NpgsqlCommand cmd = new NpgsqlCommand(cmdUpdate, pgsqlConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        public void DeletePersonRecord(Int64 id)
        {
            // var entity = _context.persons.FirstOrDefault(t => t.Id == id);  
            // _context.persons.Remove(entity);  
            // _context.SaveChanges();  
        }

        public Person GetPersonSingleRecord(Int64 id)
        {
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(GetConnectionString());
            try
            {
                using (pgsqlConnection)
                {
                    pgsqlConnection.Open();
                    using (NpgsqlCommand cmdSeleciona = new NpgsqlCommand("Select * from person Where id =" + id, pgsqlConnection))
                    {
                        NpgsqlDataReader reader = cmdSeleciona.ExecuteReader();
                        if (reader.Read())
                        {
                            Models.Person person = new Models.Person();
                            person.Name = reader[1].ToString();
                            person.CPF = int.Parse(reader[3].ToString());
                            person.BirthDate = DateTime.Parse(reader[2].ToString());
                            person.Id = Int64.Parse(reader[0].ToString());
                            //Console.WriteLine(reader[1].ToString());
                            return person;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
            return null;
        }

        public List<Person> GetPersonRecords()
        {
            List<Person> persons = new List<Person>();
            NpgsqlConnection pgsqlConnection = new NpgsqlConnection(GetConnectionString());
            try
            {
                using (pgsqlConnection)
                {
                    pgsqlConnection.Open();
                    using (NpgsqlCommand cmdSeleciona = new NpgsqlCommand("Select * from person", pgsqlConnection))
                    {
                        NpgsqlDataReader reader = cmdSeleciona.ExecuteReader();
                        while (reader.Read())
                        {
                            Models.Person person = new Models.Person();
                            person.Name = reader[1].ToString();
                            person.CPF = int.Parse(reader[3].ToString());
                            person.BirthDate = DateTime.Parse(reader[2].ToString());
                            //Console.WriteLine(reader[1].ToString());
                            persons.Add(person);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
            return persons;
        }

        public string GetConnectionString()
        {
            string host = "Host=localhost;";
            string port = "Port=5432;";
            string db = "Database=Corretora;";
            string user = "Username=user;";
            string pass = "Password=1234;";

            string config = string.Format("{0}{1}{2}{3}{4}", host, port, db, user, pass);

            return config;
        }


        public static DataTable PerformCRUD(NpgsqlCommand cmd)
        {
            NpgsqlDataAdapter da = default(NpgsqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                da = new NpgsqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e);
                dt = null;
            }

            return dt;
        }

        public void Execute(string mySql, string parameter, Models.Person person)
        {
            _cmd = new NpgsqlCommand(mySql, _conect);
            AddParameters(person);
            PerformCRUD(_cmd);
        }

        public void AddParameters(Models.Person person)
        {
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("name", person.Name);
            _cmd.Parameters.AddWithValue("date_of_birth", person.BirthDate);
            _cmd.Parameters.AddWithValue("cpf", person.CPF);
        }

    }
}