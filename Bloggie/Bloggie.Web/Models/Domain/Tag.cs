namespace Bloggie.Web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }       
        
        public string DisplayName { get; set; }

        //Many to many relation
        public ICollection<BlogPost> BlogPosts { get; set; }

    }
}
