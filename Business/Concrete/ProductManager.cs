using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRoles.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    [SecuredOperation("admin")]
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService; 
        }

        [SecuredOperation("product.add,admin")] // yetki kontrolü
        [ValidationAspect(typeof(ProductValidator))] //
        [TransactionScopeAspect]
        [PerformanceAspect(5)]

        //todo : MemoryCacheManager deki todoyyu yapmadan bu aspecti kullanma
        //CacheRemoveAspect'de hata var  uhtemelen .net60-2.0 arasındaki farktır, 6.0 a göre fix edilecek
        //[CacheRemoveAspect("IProductService.Get")]//IProductService.Get, IProductService.GetAll, IProductService.Get...

        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId)
                ,CheckIfProductNameExists(product.ProductName), ChekIfCatetgoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId)
                , CheckIfProductNameExists(product.ProductName), ChekIfCatetgoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Update(product);

            return new SuccessResult(Messages.ProductUpdated);
        }

        //[CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        //[CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var categoryCount = _productDal.GetAll(x => x.CategoryId == categoryId).Count();

            if (categoryCount > 50)
                return new ErrorResult(Messages.ProductCountOfCategoryError);

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var isSame = _productDal.GetAll(x => x.ProductName == productName).Any();

            if (isSame)
                return new ErrorResult(Messages.ProductNameAlreadyExists);

            return new SuccessResult();
        }

        private IResult ChekIfCatetgoryLimitExceded()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count > 15)
                return new ErrorResult("Limit exceded");

            return new SuccessResult();
        }
    }
}
