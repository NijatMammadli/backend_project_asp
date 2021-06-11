using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
