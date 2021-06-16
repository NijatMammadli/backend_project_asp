using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class BlogDetail
    {
        public int Id { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;



        [ForeignKey("Blog")]

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}
