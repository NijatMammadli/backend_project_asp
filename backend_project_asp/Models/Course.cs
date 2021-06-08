using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public CourseDetail CourseDetail { get; set; }
       
    }
}
