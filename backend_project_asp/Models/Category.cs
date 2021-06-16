using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<CourseCategory> CourseCategories { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }

    }
}
