using backend_project_asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.ViewModels
{
    public class CourseViewModel
    {
        public CourseDetail CourseDetail { get; set; }
      public  List<Category> Categories { get; set; }
     //public   List<int> Counts { get; set; }
    }
}
