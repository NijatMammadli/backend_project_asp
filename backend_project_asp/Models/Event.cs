using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        public string Venue { get; set; }

        public EventDetail EventDetail { get; set; }
        public ICollection<EventSpeaker> EventSpeakers { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }

        [NotMapped]

        public IFormFile Photo { get; set; }
        

    }
}
