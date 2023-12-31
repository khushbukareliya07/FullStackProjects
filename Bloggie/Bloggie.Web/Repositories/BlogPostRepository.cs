﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _blogieDbContext.Blogposts.Include(x => x.Tags).ToListAsync();

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
