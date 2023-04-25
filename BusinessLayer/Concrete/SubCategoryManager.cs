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
    public class SubCategoryManager : ISubCategoryService
    {
        ISubCategoryDal _subcategoryDal;

        public SubCategoryManager(ISubCategoryDal subcategoryDal)
        {
            _subcategoryDal = subcategoryDal;
        }

        public List<SubCategory> GetList()
        {
            return _subcategoryDal.GetListAll();
        }

        public List<SubCategory> GetSubCategoryListByDURUM()
        {
            return _subcategoryDal.GetSubCategoryListByDurum();
        }

        public void TAdd(SubCategory s)
        {
            _subcategoryDal.Insert(s);
        }

        public void TDelete(SubCategory s)
        {
            _subcategoryDal.Delete(s);
        }

        public SubCategory TGetById(int id)
        {
            return _subcategoryDal.GetByID(id);
        }

        public void TUpdate(SubCategory s)
        {
            _subcategoryDal.Update(s);
        }
    }
}
