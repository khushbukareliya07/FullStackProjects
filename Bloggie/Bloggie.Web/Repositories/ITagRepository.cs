using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;

namespace Bloggie.Web.Repositories
{
    public interface ITagRepository
    {
        //dependency Injection for bloggieDBContext

        Task<IEnumerable<Tag>> GetAllAsync();


        Task<Tag?> GetAsync(Guid id);


        Task<Tag?> AddAsync(Tag tag);


        Task<Tag?> UpdateAsync(Tag tag);


        Task<Tag?> DeleteAsync(Guid id);
      
    }
}
