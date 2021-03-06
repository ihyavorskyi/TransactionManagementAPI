using System.ComponentModel.DataAnnotations;

namespace TransactionManagementAPI.Data.Models
{
    /// <summary>
    /// User login model
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}