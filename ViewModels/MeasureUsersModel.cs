using ZimskaLiga.Models;

namespace ZimskaLiga.ViewModels
{
    public class MeasureUsersModel
    {
        public IEnumerable<LogInModel> LoginModels { get; set; }

        public LogInModel UpdateLogin { get; set; }

        public MeasureUsersModel(IEnumerable<LogInModel> loginModels, LogInModel updateLogin)
        {
            LoginModels = loginModels;
            UpdateLogin = updateLogin;
        }
    }
}
