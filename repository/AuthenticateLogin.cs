using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZimskaLiga.Models;
using ZimskaLiga.repository;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace ZimskaLiga.repository
{
    public class AuthenticateLogin : ILogin
    {
        private readonly LoginDbcontext _context;

        static List<string> sqlConnStr { get; set; }

        static int IdUser { get; set; }

        public AuthenticateLogin(LoginDbcontext context)
        {
            _context = context;

            sqlConnStr = new List<string>
            { "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"};
        }
        public async Task<LogInModel> AuthenticateUser(string username, string passcode)
        {
            var succeeded = await _context.UserLogin.FirstOrDefaultAsync(authUser => authUser.UserName == username && authUser.passcode == passcode);
            return succeeded;
        }


        public static Int32 GetUserId(string userName, string passcode)
        {
            IdUser = 0;
            SqlConnection connection;

            using (connection = new SqlConnection(sqlConnStr.First()))
            {
                using (SqlCommand command = new SqlCommand(
                    $"select id From [master].[dbo].[UserLogin] Where UserName  = '{userName}' and passcode = '{passcode}'" , connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            IdUser = reader.GetInt32(0);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
              ;
            }
            return IdUser;

        }

        public async Task<IEnumerable<LogInModel>> getuser()
        {
            return await _context.UserLogin.ToListAsync();
        }
    }
}