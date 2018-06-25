using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow, string[] includes = null);
    }

    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page = 0, int pageSize = 50, out int totalRow, string[] includes = null)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag && p.Status
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}