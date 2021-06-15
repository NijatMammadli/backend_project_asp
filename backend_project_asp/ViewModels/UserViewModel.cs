using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; 

namespace backend_project_asp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Fullname { get; set; }

        [Required]

        public string Username { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
