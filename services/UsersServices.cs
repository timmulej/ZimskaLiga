using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ZimskaLiga.Models;

namespace ZimskaLiga.services
{
    public class UsersServices : Controller
    {
        static List<LogInModel> LoginModels { get; }

        static List<string> sqlConnStr { get; }

        static List<TimeSpan> TimeUsers { get; }

        static UsersServices()
        {
            LoginModels = new List<LogInModel>
            { };

            TimeUsers = new List<TimeSpan>
            { };
            sqlConnStr = new List<string>
            { "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"};
        }

  
        public static void Add(LogInModel LogInModel)
        {
            LoginModels.Add(LogInModel);
        }

        public static LogInModel? Get(int? id) => LoginModels.FirstOrDefault(p => p.id == id);

        public static TimeSpan GetTime(Int32 Iduser)
        {
            TimeUsers.Clear();
            SqlConnection connection;

            using (connection = new SqlConnection(sqlConnStr.First()))
            {
                using (SqlCommand command = new SqlCommand(
                    $"select castekmovalca From [master].[dbo].[UserLogin] Where Id = {Iduser}", connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            TimeUsers.Add(reader.GetTimeSpan(0));
                            //TimeUsers.Add(reader.GetTimeSpan(0));
                        }
                        reader.Close();
                    }
                    connection.Close();
                }

            }
            return TimeUsers.First();

        }

        public static Int32 GetPlacment(Int32 IdUser)
        {
            Int32 Placment = 0;
            SqlConnection connection;

            using (connection = new SqlConnection(sqlConnStr.First()))
            {
                using (SqlCommand command = new SqlCommand(
                    $"select uvrstitev From [master].[dbo].[UserLogin] Where Id = {IdUser}", connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            Placment = reader.GetInt32(0);

                        }
                        reader.Close();
                    }
                    connection.Close();
                }

            }
            return Placment;

        }

        public static List<LogInModel> GetAllUsers()
        {
            LoginModels.Clear();
            SqlConnection connection;

            using (connection = new SqlConnection(sqlConnStr.First()))
            {
                using (SqlCommand command = new SqlCommand(
                    "select * From [master].[dbo].[UserLogin] Where isActive = 1 Order by uvrstitev ASC", connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Add(
                                    new LogInModel
                                    {
                                        id = reader.GetInt32(0),
                                        UserName = reader.GetString(1),
                                        passcode = reader.GetString(2),
                                        isActive = reader.GetInt32(3),
                                        castekmovalca = reader.GetTimeSpan(4),
                                        uvrstitev = reader.GetInt32(5)
                                    }
                                );
                            }

                        }
                        reader.Close();
                    }
                    connection.Close();
                }

            }
            return LoginModels;

        }

        public static void Update(LogInModel User, int id)
        {

            SqlConnection connection;
            using (connection = new SqlConnection(sqlConnStr.First()))
            {
                connection.Open();

                string queryString = " UPDATE [master].[dbo].[UserLogin] SET " +
                  $"Name ='{User.UserName}', passcode = '{User.passcode}', casTekmovalca = {User.castekmovalca}," +
                  $" isActive = 1" +
                  $"WHERE id = '{id}';";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    Int32 recordsAffected = command.ExecuteNonQuery();
                }



                connection.Close();
            }
        }


    }
}
