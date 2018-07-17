using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string filter);

        Product GetByID(int id);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        #region Initialize

        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Initialize

        #region Implementation

        public Product Add(Product product)
        {
            product.CreatedDate = DateTime.Now;
            return _productRepository.Add(product);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return _productRepository.GetAll();
            }
            return _productRepository.GetMulti(p => p.Name.ToUpper().Contains(filter.ToUpper()) || p.Description.ToUpper().Contains(filter.ToUpper()));
        }

        public Product GetByID(int id)
        {
            return _productRepository.GetSingleByID(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            product.UpdatedDate = DateTime.Now;
            _productRepository.Update(product);
        }

        #endregion Implementation
    }
}