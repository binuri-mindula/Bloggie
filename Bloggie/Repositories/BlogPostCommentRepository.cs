using Bloggie.Data;
using Bloggie.Models.Domain;

namespace Bloggie.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostComment;
        }
    }
}
