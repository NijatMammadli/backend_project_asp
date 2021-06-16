using backend_project_asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string Role { get; set; }
        public List<string> Roles { get; set; }
        public List<Course> Courses { get; set; }
        public List<int> CoursesId { get; set; }
    }
}
