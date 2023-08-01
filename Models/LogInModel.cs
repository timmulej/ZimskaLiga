using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
namespace ZimskaLiga.Models
{

    public class LogInModel
    {
        [Key]
        public int? id { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Please Enter Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Please Enter Password")]
        public string passcode { get; set; }
        public int isActive { get; set; }

        public int uvrstitev { get; set; }

        public TimeSpan castekmovalca { get; set; }

    }
}
