using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public TeacherDetail TecaherDetail { get; set; }
        public ICollection<SocialMedia> socialMedias { get; set; }

        [ForeignKey("Position")]

        public int PositionId { get; set; }
        public Position Position { get; set; }

        [NotMapped]

        public IFormFile Photo { get; set; }
    }
}
