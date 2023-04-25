using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {

        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }



        public List<Product> GetList()
        {
            return _productDal.GetListAll();
        }

        public Product GetRowWithCategoryById(int id)
        {
            return _productDal.GetRowWithCategoryById(id);
        }

        public void TAdd(Product p)
        {
            _productDal.Insert(p);
        }

        public void TDelete(Product p)
        {
            _productDal.Delete(p);
        }

        public Product TGetById(int id)
        {
            return _productDal.GetByID(id);
        }

        public void TUpdate(Product p)
        {
            _productDal.Update(p);
        }
    }
}
