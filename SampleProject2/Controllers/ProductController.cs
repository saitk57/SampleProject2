using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleProject2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject2.Controllers
{
    public class ProductController : Controller
    {

        ProductManager pm = new ProductManager(new EfProductRepository());
        SubCategoryManager sm = new SubCategoryManager(new EfSubCategoryRepository());

        Context c = new Context();

        public IActionResult Index()
        { 
            var values = pm.GetList();
            return View(values);
        }

        public IActionResult ProductDetail(int id)
        {
            var values = pm.GetRowWithCategoryById(id);
            return View(values);
        }

        public IActionResult AddProduct()
        {
            List<SelectListItem> degerler = (from x in sm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.Id.ToString()

                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();

        }
        [HttpPost]

        public IActionResult AddProduct(Product product, AddProductImage p)
        {
            //var urn = c.SubCategories.Where(x => x.Id == product.SubCategory.Id).FirstOrDefault();
            //product.SubCategory = urn;

            if (p.Image != null)
            {
                var extension = Path.GetExtension(p.Image.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.Image.CopyTo(stream);
                product.Image = newimagename;

            }

            //product.Name = p.Name;
            //product.Price = p.Price;
            //product.Description = p.Description;

            //c.Products.Add(product);
            //c.SaveChanges();
            pm.TAdd(product);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateProduct(int id)
        {
            List<SelectListItem> degerler = (from x in sm.GetList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Name,
                                                 Value = x.Id.ToString()

                                             }).ToList();

            ViewBag.dgr = degerler;

            var update = pm.TGetById(id);

            return View(update);

        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product, AddProductImage p)
        {
            var urn = pm.TGetById(product.Id);

            urn.SubCategoryId = product.SubCategoryId;

            if (p.Image != null)
            {
                var extension = Path.GetExtension(p.Image.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.Image.CopyTo(stream);
                urn.Image = newimagename;

            }

            urn.Name = product.Name;
            urn.Price = product.Price;
            urn.Description = product.Description;
            urn.Stock = product.Stock;
            urn.Tarih = product.Tarih;

            pm.TUpdate(urn);
            return RedirectToAction("Index");

        }

        public IActionResult DeleteProduct(int id)
        {
            var urnbul = pm.TGetById(id);
            pm.TDelete(urnbul);
            return RedirectToAction("Index");
        }



    }
}
