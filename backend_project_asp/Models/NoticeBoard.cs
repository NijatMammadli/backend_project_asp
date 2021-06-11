using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        public string Description { get; set; }
     
        public DateTime Date { get; set; } = DateTime.UtcNow; 
    }
}
