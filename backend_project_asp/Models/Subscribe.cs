using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
                
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
