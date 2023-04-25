using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject2.Controllers
{
    public class SubCategoryController : Controller
    {
        SubCategoryManager sm = new SubCategoryManager(new EfSubCategoryRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {
            var values = sm.GetSubCategoryListByDURUM();
            return View(values);
        }

        public IActionResult AddSubCategory()
        {
            List<SelectListItem> degerler = (from x in cm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.Id.ToString()

                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();

        }

        [HttpPost]
        public IActionResult AddSubCategory(SubCategory subCategory)
        {
            sm.TAdd(subCategory);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateSubCategory(int id)
        {
            List<SelectListItem> degerler = (from x in cm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.Id.ToString()

                                             }).ToList();

            ViewBag.dgr = degerler;

            var update = sm.TGetById(id);

            return View(update);

        }

        [HttpPost]
        public IActionResult UpdateSubCategory(SubCategory subCategory)
        {
            var altkategori = sm.TGetById(subCategory.Id);

            altkategori.CategoryId = subCategory.CategoryId;
            altkategori.Name = subCategory.Name;
            altkategori.Durum = subCategory.Durum;

            sm.TUpdate(altkategori);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteSubCategory(int id)
        {
            var deger = sm.TGetById(id);
            deger.Durum = false;
            sm.TUpdate(deger);

            return RedirectToAction("Index");
        }
    }
}
