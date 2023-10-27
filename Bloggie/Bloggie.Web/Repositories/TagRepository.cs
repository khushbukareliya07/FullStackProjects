using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogieDbContext _bloggieDBContext;

        public TagRepository(BlogieDbContext bloggieDBContext)
        {
            _bloggieDBContext = bloggieDBContext;
        }

        async Task<Tag?> ITagRepository.AddAsync(Tag tag)
        {
            await _bloggieDBContext.Tags.AddAsync(tag);
            await _bloggieDBContext.SaveChangesAsync();
            return tag;
        }

       

        async Task<IEnumerable<Tag>> ITagRepository.GetAllAsync()
        {
            var listofTags = await _bloggieDBContext.Tags.ToListAsync();
            return listofTags;
        }

        async Task<Tag?> ITagRepository.GetAsync(Guid id) //edit method in Admintagcontroller
        {
            var tag = await _bloggieDBContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
            return tag;

        }

        async Task<Tag?> ITagRepository.UpdateAsync(Tag tag)
        {
            var existingTag = await _bloggieDBContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Id = tag.Id;
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _bloggieDBContext.SaveChangesAsync();

                return existingTag;
            }
            else
                return null;
        }

        async Task<Tag?> ITagRepository.DeleteAsync(Guid id)
        {
            Tag existingTag = await _bloggieDBContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                _bloggieDBContext.Remove(existingTag);
                //_bloggieDBContext.Remove(id);
                await _bloggieDBContext.SaveChangesAsync();

                return existingTag;
            }
            else
            {
                return null;
            }
        }
    }
}
