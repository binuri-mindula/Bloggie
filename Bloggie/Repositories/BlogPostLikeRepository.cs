using Bloggie.Data;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Repositories
{
    public class BlogPostLikeRepository: IBlogPostLikeRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }
        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
           return await _bloggieDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
