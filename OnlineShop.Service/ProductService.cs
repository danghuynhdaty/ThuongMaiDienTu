using OnlineShop.Common;
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
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IProductTagRepository productTagRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
        }

        #endregion Initialize

        #region Implementation

        public Product Add(Product product)
        {
            product.CreatedDate = DateTime.Now;
            var addedProduct = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagID = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(p => p.ID == tagID) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagID,
                            Name = tags[i],
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = addedProduct.ID,
                        TagID = tagID
                    };
                    _productTagRepository.Add(productTag);
                }
            }

            return addedProduct;
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
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagID = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(p => p.ID == tagID) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagID,
                            Name = tags[i],
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(p=>p.ProductID == product.ID);
                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = product.ID,
                        TagID = tagID
                    };
                    _productTagRepository.Add(productTag);
                }
            }
        }

        #endregion Implementation
    }
}