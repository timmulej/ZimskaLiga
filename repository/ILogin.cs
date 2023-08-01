using System.Collections.Generic;
using System.Threading.Tasks;
using ZimskaLiga.Models;

namespace ZimskaLiga.repository
{
    public interface ILogin
    {
        Task<IEnumerable<LogInModel>> getuser();

        Task<LogInModel> AuthenticateUser(string username, string passcode);

    }
}
