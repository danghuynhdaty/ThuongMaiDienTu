using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System.Collections.Generic;

namespace OnlineShop.Service
{
    public interface IPostService
    {
        void Add(Post post);

        void Update(Post post);

        void Delete(Post post);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalRow);

        Post GetByID(int id);

        void SaveChanges();
    }

    public class PostService : IPostService
    {
        #region Properties

        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructor

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        public void Add(Post post)
        {
            _postRepository.Add(post);
        }

        public void Delete(Post post)
        {
            _postRepository.Delete(post);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(p => p.CategoryID == categoryID && p.Status, out totalRow, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetAllByTagPaging(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(p => p.Status, out totalRow, page, pageSize);
        }

        public Post GetByID(int id)
        {
            return _postRepository.GetSingleByID(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}