using System.Data;
using Microsoft.Data.SqlClient;

namespace FirstMvcApp.Models
{
    public class Person
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Person()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static List<Person> FetchAllPersons()
        {
            List<Person> allPersons = new List<Person>();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = Program.Configuration.GetConnectionString("PrimaryConnectionString");

            try
            {
                using (conn)
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT id, first_name, last_name, date_of_birth FROM Person WHERE is_deleted = 0", conn);


                    Person person = null;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            person = new Person();

                            person.Id = Convert.ToInt32(reader["id"]);
                            person.FirstName = reader["first_name"].ToString();
                            person.LastName = reader["last_name"].ToString();
                            person.DateOfBirth = (DateTime)reader["date_of_birth"];

                            allPersons.Add(person);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //todo
            }

            return allPersons;
        }

        public static int DeletePersonById(int personId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Program.Configuration.GetConnectionString("PrimaryConnectionString");

            int rows = 0;

            try
            {
                using (conn)
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UPDATE Person SET is_deleted=1 WHERE id=@Id", conn);

                    SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                    myParam.Value = personId;

                    command.Parameters.Add(myParam);

                    rows = command.ExecuteNonQuery();


                }
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
            }

            return rows;

        }

        public static Person FetchPersonById(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Program.Configuration.GetConnectionString("PrimaryConnectionString");

            Person person = null;

            try
            {
                using (conn)
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT first_name, last_name, date_of_birth FROM Person WHERE id =" + id, conn);


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        person = new Person();

                        person.Id = id;
                        person.FirstName = (string)reader["first_name"];
                        person.LastName = (string)reader["last_name"];
                        person.DateOfBirth = (DateTime)reader["date_of_birth"];
                    }
                }
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
            }

            return person;

        }


        public int UpdatePerson()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Program.Configuration.GetConnectionString("PrimaryConnectionString");

            int rows = 0;

            try
            {
                using (conn)
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("UPDATE Person SET first_name=@FirstName, last_name=@LastName, date_of_birth=@DateOfBirth WHERE id=@Id", conn);

                    SqlParameter firstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                    firstNameParam.Value = FirstName;

                    SqlParameter lastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar);
                    lastNameParam.Value = LastName;

                    SqlParameter dateOfBirthParam = new SqlParameter("@DateOfBirth", SqlDbType.Date);
                    dateOfBirthParam.Value = DateOfBirth;

                    SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                    myParam.Value = Id;

                    command.Parameters.Add(firstNameParam);
                    command.Parameters.Add(lastNameParam);
                    command.Parameters.Add(dateOfBirthParam);
                    command.Parameters.Add(myParam);

                    rows = command.ExecuteNonQuery();

                }
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
            }

            return rows;

        }


        public int InsertPerson()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Program.Configuration.GetConnectionString("PrimaryConnectionString");

            int rows = 0;

            try
            {
                using (conn)
                {

                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Person(first_name, last_name, date_of_birth, is_deleted) VALUES(@FirstName, @LastName, @DateOfBirth, 0);", conn);

                    SqlParameter firstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar);
                    firstNameParam.Value = FirstName;

                    SqlParameter lastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar);
                    lastNameParam.Value = LastName;
                    SqlParameter dateOfBirthParam = new SqlParameter("@DateOfBirth", SqlDbType.Date);
                    dateOfBirthParam.Value = DateOfBirth;

                    command.Parameters.Add(firstNameParam);
                    command.Parameters.Add(lastNameParam);
                    command.Parameters.Add(dateOfBirthParam);

                    rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception err)
            {
                // Handle an error by displaying the information.
            }

            return rows;
        }
    }

}