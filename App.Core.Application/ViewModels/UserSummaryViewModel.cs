using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Application.ViewModels
{
    public class UserSummaryViewModel
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Role { get; set; }
    }
}
