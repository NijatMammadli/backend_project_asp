using backend_project_asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewModels
{
    public class GlobalSearchViewModel
    {
       public List<Course> Courses { get; set; }
       public List<Teacher> Teachers { get; set; }
       public List<Event> Events { get; set; }
       public List<Blog> Blogs { get; set; }
    }
}
