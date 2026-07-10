using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter the username of user")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "You must enter the password of user")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
