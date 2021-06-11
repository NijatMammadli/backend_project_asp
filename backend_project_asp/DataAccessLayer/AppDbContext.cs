using backend_project_asp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBack_hw.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set;  }
        public DbSet<CourseDetail> CourseDetails { get; set;  }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherDetail> TeacherDetails { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<TestiMonial> TestiMonials { get; set; }
        public DbSet<WelcomeArea> WelcomeAreas { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }

        


    }
}
