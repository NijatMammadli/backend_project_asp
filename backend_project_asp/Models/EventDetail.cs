using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class EventDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DetailedVenue { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("Event")]

        public int EventId { get; set; }

        public Event Event { get; set; }
    }
}
