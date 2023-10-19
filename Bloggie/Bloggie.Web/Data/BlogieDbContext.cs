using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class BlogieDbContext : DbContext
    {
        public BlogieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> Blogposts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
