using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
    }

    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}