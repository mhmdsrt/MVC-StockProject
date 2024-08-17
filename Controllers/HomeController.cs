using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStock.Controllers
{
    // "return view" ile geriye Views/Home klasörü altındaki .cshtml uzantılı dosyaları döndürüyoruz
    // Eğer  view'i parametresiz kullancaksak yani "return View()" diyorsak buradaki metotların isimleri
    // ile Views/Home klasörü altındaki .cshtml uzantılı dosyaların isimleri aynı olmak zorunda!
    // Ya da return View("ViewName"); ile döndürmek istediğimiz viewi verebiliriz
    // Ayrıca Dönrdürmek istediğin view'ler Controller ile aynı isimdeki dosyanın altında olmak zorundadır yani Views/Home.
    // HomeController için view'ler Views/Home klasöründe olmalıdır. Ancak tam yol belirterek bu varsayılanı aşabilirsiniz.
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}