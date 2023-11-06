using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogieDbContext _blogieDbContext;

        public BlogPostRepository(BlogieDbContext blogieDbContext)
        {
           _blogieDbContext = blogieDbContext;
        }
       //look what this does! -  public IQueryable<BlogPost> GetAll() { }
        
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _blogieDbContext.AddAsync(blogPost);
            await _blogieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
