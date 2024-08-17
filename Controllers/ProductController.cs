using MvcStock.Models;
using MvcStock.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class ProductController : Controller
    {
        ProductsCustomersEntities1 context = new ProductsCustomersEntities1();
        
        [HttpGet]
        public ActionResult GetAllProduct(int page=1) /* Burada parametreye değer vererek metot kullanım şekli eğer parametre gönderilmezse
                                                         varsayılan olarak her zaman page=1 olsun demek
                                                       */
        {
            var log = context.Products.ToList().ToPagedList(page,10);
            /*
                ToPagedList(2,3) ->  1.Parametre kaçıncı sayfadan başlanacağı
                                     2.Parametre her sayfada kaç öğe olacağını belirliyor.
             */

            return View(log);
        }

        [HttpGet]
        /*
             Text  -> Kullanıcının Dropdown listesinde göreceği kısım
             Value -> Kullanıcının dropdown listesinde kategori öğesi seçip ekle butona tıkladıktan sonra 
             veritabanına gönderilecek değeri temsil eder.
             
             Buradaki Value değeri form tarafında ürün ekleme butonuna tıklandığında, açılır listedeki seçilen kategori öğesinin 
             veri tabanında kaydedilecek değeridir. 
             
             */
        public ActionResult InsertProduct()
        {
            List<SelectListItem> valuesList = (from x in context.Categories
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryID.ToString()
                                          }).ToList();
            ViewBag.valueList = valuesList; // Form tarafındaki açılır listeye göndereceğimiz listeyi burada belirleyip view tarafına gönderiyoruz.
            
            return View(); //CTRL+SHIFT+SPACE ile overloads 'ları görebiliyoruz.
        }

        [HttpPost]
        public ActionResult InsertProduct(Products entity) 
        {
            if (!ModelState.IsValid)
            {
                return View("InsertProduct");
            }
            context.Products.Add(entity);
            context.SaveChanges();
            return RedirectToAction("GetAllProduct"); // Redirect -> Yönlendir , Parametre olarak bu Metotdan sonra  yani
                                                      // Post işleminden sonra yönlendireceğimiz action(eylemi) belirtiyoruz.
        }

        [HttpGet]

        public ActionResult DeleteProduct(int id)
        {
            /*
            GetAllProduct View sayfasındaki "/Product/DeleteProduct/@prod.ProductID" ifadesi ile 
            buradaki DeleteProduct Action'una parametre olarak ProductID gönderiyoruz.
                        */
            var entityToDelete = context.Products.Find(id);
            if (entityToDelete != null)
            {
                context.Products.Remove(entityToDelete);
                context.SaveChanges();
            }
            
            return RedirectToAction("GetAllProduct");
        }

        [HttpGet]
        public ActionResult GetProductToUpdate(int id)
        {
            List<SelectListItem> valueList = (from x in context.Categories
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryID.ToString()
                                             }).ToList();
            ViewBag.valueList = valueList;

            var productToUpdate = context.Products.Find(id);
            return View(productToUpdate);
        }

        [HttpPost]

        public ActionResult UpdateProduct(Products entity)
        {
            // Önce form tarafından gönderilen id ile aynı id'e sahip güncellenecek kaydı bul.
            // sonra formdan gelen propertyleri o kaydın propertlerine ata sonra veritabanındaki değişiklikleri kaydet.
            var productToUpdate = context.Products.Find(entity.ProductID);
            productToUpdate.ProductName = entity.ProductName;
            productToUpdate.ProductCategoryID = entity.ProductCategoryID;
            productToUpdate.ProductCost = entity.ProductCost;
            productToUpdate.ProductBrand = entity.ProductBrand;
            productToUpdate.ProductStock = entity.ProductStock;
            context.SaveChanges();
            return RedirectToAction("GetAllProduct");
        }
    }
}