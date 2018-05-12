using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.DB;
using System;

namespace DAL
{
    public class HRUDbContext : IdentityDbContext<User>
    {

        public DbSet<Carousel> Carousel { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutUnion> AboutUnion { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<FaceBook> FaceBook { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Person> Person { get; set; }

        public HRUDbContext(DbContextOptions<HRUDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
