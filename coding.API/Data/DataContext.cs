using Microsoft.EntityFrameworkCore;
using coding.API.Models.Entities.Products;
using coding.API.Models.Entities.WorkExperiences;
using coding.API.Models.Entities.Users;
using coding.API.Models.Entities.Posts;
using coding.API.Models.Entities.Educations;
using coding.API.Models.Entities.Awards;
using coding.API.Models.Entities.Languages;
using coding.API.Models.Entities.Projects;
using coding.API.Models.Entities.Skills;
using coding.API.Models.Entities.Photos;
using coding.API.Models.Entities.PostTags;
using coding.API.Models.Entities.Tags;
using coding.API.Models.Entities.Products.Requirements;
using coding.API.Models.Entities.Products.ProductsRequirements;

namespace coding.API.Data
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
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<ProductRequirement> ProductRequirements { get; set; }

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