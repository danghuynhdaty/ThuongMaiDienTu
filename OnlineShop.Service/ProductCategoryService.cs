using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);

        void Update(ProductCategory ProductCategory);

        ProductCategory Delete(int ID);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAll(string filter);

        IEnumerable<ProductCategory> GetAllByParentID(int parentID);

        ProductCategory GetByID(int ID);

        void SaveChanges();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        #region Properties

        private IProductCategoryRepository _ProductCategoryRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Properties

        #region Constructor

        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
        {
            _ProductCategoryRepository = ProductCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        public ProductCategory Add(ProductCategory ProductCategory)
        {
            ProductCategory.CreatedDate = DateTime.Now;
            return _ProductCategoryRepository.Add(ProductCategory);
        }

        public ProductCategory Delete(int ID)
        {
            return _ProductCategoryRepository.Delete(ID);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _ProductCategoryRepository.GetAll();
            }
            return _ProductCategoryRepository.GetMulti(p => p.Name.ToUpper().Contains(filter.ToUpper()) || p.Description.ToUpper().Contains(filter.ToUpper()));
        }

        public IEnumerable<ProductCategory> GetAllByParentID(int parentID)
        {
            return _ProductCategoryRepository.GetMulti(p => p.Status && p.ParentID == parentID);
        }

        public ProductCategory GetByID(int ID)
        {
            return _ProductCategoryRepository.GetSingleByID(ID);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            ProductCategory.UpdatedDate = DateTime.Now;
            _ProductCategoryRepository.Update(ProductCategory);
        }
    }
}