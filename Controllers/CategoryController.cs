using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    // "return view" ile geriye Views/Home klasörü altındaki .cshtml uzantılı dosyaları döndürüyoruz
    // Eğer  view'i parametresiz kullancaksak yani "return View()" diyorsak buradaki metotların isimleri
    // ile Views/Home klasörü altındaki .cshtml uzantılı dosyaların isimleri aynı olmak zorunda!
    // Ya da return View("ViewName"); ile döndürmek istediğimiz viewi verebiliriz
    // Ayrıca Dönrdürmek istediğin view'ler Controller ile aynı isimdeki dosyanın altında olmak zorundadır yani Views/Home.
    // HomeController için view'ler Views/Home klasöründe olmalıdır. Ancak tam yol belirterek bu varsayılanı aşabilirsiniz.

    // Controllerin çalışma mantığı : "https://localhost:44346/Category/GetAllCategory" bu url'deki "Category" ismi
    // "CategoryController : Controller" isminden geliyor.
    // "GetAllCategory" ifadesi ise CategoryController içerisinde yani burada oluşturduğumuz metot ismini alıyor.
    // Yani web sitesindeki url Controller/Metot oluyor kısaca.
    public class CategoryController : Controller
    {
        ProductsCustomersEntities1 context = new ProductsCustomersEntities1();

        [HttpGet]
        public ActionResult GetAllCategory(int page=1)
        {
            // @model ifadesi yalnızca return View(model) ile gönderilen belirli bir veri modeline erişim sağlar

            var log = context.Categories.ToList().ToPagedList(page,10);

            return View(log); 

            /*  "return View(log);" ifadesi ile GetAllCategory.cshtml View'ne Categories Sınıfı tipinde
                List<> gönderiyoruz ve aynı zamanda geriye GetAllCategory View sayfasını döndürüyoruz döndürürkende 
                bu sayfaya List<Categories> gönderiyoruz.

             */

            /*

             Model: Veritabanı tablosunu temsil eden sınıftır (Categories).
             Controller: Veritabanından verileri alır ve view'e gönderir (CategoryController).
             View: Controller'dan gelen verileri kullanıcıya gösterir (GetAll.cshtml).

            */
        }
        //[HttpGet]
        //public ActionResult GetAllCategory(string searchWord)
        //{
        //    var allCategory = (from x in context.Categories
        //                          select x).ToList();

        //    if (!string.IsNullOrEmpty(searchWord)) // parametre olarak alınan değer boş ya da null değilse
        //    {
        //        var searchWordList = allCategory.Where(c => c.CategoryName.Contains(searchWord)).ToList();
        //    }
        //    return View(searchWord);
        //}


        [HttpGet]

        public ActionResult InsertCategory() 
        {
            // Category/InsertCategory(URL) View sayfası geldiği zaman sadece GET işlemi(listeleme) yap.
            // Category/InsertCategory URL adresine gidildiği zaman sadece listeme(GET) olsun diyoruz.
            // Get isteği olunca bu metodu çalıştır.
            return View();
        }

        [HttpPost] 
        public ActionResult InsertCategory(Categories entity) // InsertCategory View sayfasındaki <form> dan gelen verileri alır.
        {
            //ModelState.IsValid özelliği tüm doğruma işlemlerinin başarılı olup-olmadığını kontrol etmek için kullanılır ve True-False döner
            if (!ModelState.IsValid)
            {
                return View("InsertCategory");
            }
            // Category/InsertCategory View  sayfası gelip daha sonrasında butona tıklanırsa
            // yani bir POST isteği oldugunda veri tabanına ekleme yapabilirsin diyoruz.
            // Buton tıklanması post isteği olduğu için Post isteği olunca bu metodu çalıştır.
            context.Categories.Add(entity);
            context.SaveChanges();
            return RedirectToAction("GetAllCategory");
        }
        [HttpGet]
        public ActionResult GetCategoryToUpdate(int id) // Güncellenecek category'i GetCategoryToUpdate view sayfasına getir.
        {
            var categoryToUpdate = context.Categories.Find(id);

            return View(categoryToUpdate); // GetCategoryToUpdate View sayfasında "categoryToUpdate" entity'sine
                                                                  // erişebilmek için @model kullanıyoruz.
        }

        [HttpPost]

        public ActionResult UpdateCategory(Categories entity) // buradaki entity formdan buraya gönderilen entity'dir.
        {
            var categoryToUpdate = context.Categories.Find(entity.CategoryID);
            categoryToUpdate.CategoryName = entity.CategoryName;
            context.SaveChanges();
            return RedirectToAction("GetAllCategory"); // burada Parametre olarak "GetCategoryToUpdate" ifadesini verme sebebimiz
                                                // UpdateCategory için bir view sayfası olusturmadıgımız ve sadece update işlemini
                                                // yapmak istediğimizden dolayı.
        }

        public ActionResult DeleteCategory(int id)
        {
            var deleteToCategory = context.Categories.Find(id);
            if (deleteToCategory != null)
            {
                context.Categories.Remove(deleteToCategory);
                context.SaveChanges();
            }
            return RedirectToAction("GetAllCategory");
        }
    }
}