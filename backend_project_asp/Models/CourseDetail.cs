﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }

        public string AboutCourse { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }
        public DateTime Starts { get; set; } = DateTime.UtcNow; 
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assesments { get; set; }
        public double CourseFee { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
