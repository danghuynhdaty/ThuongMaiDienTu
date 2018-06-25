using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IPostCategoryService
    {
        void Add(PostCategory postCategory);
        void Update(PostCategory postCategory);
        void Delete(int ID);
        IEnumerable<PostCategory> GetAll();
        IEnumerable<PostCategory> GetAllByParentID(int parentID);
        PostCategory GetByID(int ID);
    }
    public class PostCategoryService : IPostCategoryService
    {
        #region Properties

        IPostCategoryRepository _postCategoryRepository;
        IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            _postCategoryRepository = postCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion
        public void Add(PostCategory postCategory)
        {
            _postCategoryRepository.Add(postCategory);
        }

        public void Delete(int ID)
        {
            _postCategoryRepository.Delete(ID);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentID(int parentID)
        {
            return _postCategoryRepository.GetMulti(p => p.Status && p.ParentID == parentID);
        }

        public PostCategory GetByID(int ID)
        {
            return _postCategoryRepository.GetSingleByID(ID);
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }
    }
}
