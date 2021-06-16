using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public int CommentCount { get; set; }

        
        public DateTime Date { get; set; } = DateTime.UtcNow; 
        public bool IsDeleted { get; set; }
        public BlogDetail BlogDetail { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
