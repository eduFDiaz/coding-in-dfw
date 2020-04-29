using Microsoft.EntityFrameworkCore;
using coding.API.Models.Products;
using coding.API.Models.WorkExperiences;
using coding.API.Models.Skills;
using coding.API.Models.Educations;
using coding.API.Models.Awards;
using coding.API.Models.Languages;
using coding.API.Models.Projects;

namespace coding.API.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PostTag> PostTags {get; set;}
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Skill> Skills {get; set; }
        public DbSet<Education> Educations {get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder builder){

            builder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

            // builder.Entity<PostTag>()
            // .HasOne(pt => pt.Post)
            // .WithMany(p => p.PostTags)
            // .HasForeignKey(pt => pt.PostId);  
            // builder.Entity<PostTag>()
            // .HasOne(pt => pt.Tag)
            // .WithMany(t => t.PostTags)
            // .HasForeignKey(pt => pt.TagId);
            
        }
    }
}