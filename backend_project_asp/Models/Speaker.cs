using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Speaker
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<EventSpeaker> EventSpeakers { get; set; }

    }
}
