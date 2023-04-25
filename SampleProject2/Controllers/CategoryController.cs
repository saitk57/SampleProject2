using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject2.Controllers
{
    public class CategoryController : Controller
    {

        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {
            var values = cm.GetCategoryListByDURUM();
            return View(values);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AddCategory(Category category)
        {
            cm.TAdd(category);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCategory(int id)
        {
            var update = cm.TGetById(id);

            return View(update);
        }

        [HttpPost]

        public IActionResult UpdateCategory(Category category)
        {
            var update = cm.TGetById(category.Id);

            update.Name = category.Name;
            update.Durum = category.Durum;

            cm.TUpdate(update);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var deger = cm.TGetById(id);
            deger.Durum = false;
            cm.TUpdate(deger);

            return RedirectToAction("Index");
        }

    }
}
