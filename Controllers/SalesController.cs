using MvcStock.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStock.Controllers
{
    public class SalesController : Controller
    {
        ProductsCustomersEntities1 context = new ProductsCustomersEntities1();
        public ActionResult GetAllSales()
        {
            List<SelectListItem> valueListProduct = (from x in context.Products
                                             select new SelectListItem
                                             {
                                                 Text = x.ProductName,
                                                 Value = x.ProductID.ToString()
                                             }).ToList();

            ViewBag.valueListProduct = valueListProduct;

            List<SelectListItem> valueListCustomer = (from c in context.Customers
                                                      select new SelectListItem
                                                      {
                                                          Text = c.CustomerFirstName + " " + c.CustomerSurName,
                                                          Value = c.CustormerID.ToString()
                                                      }).ToList();
            ViewBag.valueListCustomer = valueListCustomer;
            /*
             ViewBag sayesinde return view() de birşey göndermeden view tarafına DropDownList için istediğimiz listeleri gönderebiliyoruz.
             */

            return View();
        }

        [HttpPost]
        public ActionResult InsertSales(Saleses entity)
        {
            context.Saleses.Add(entity);
            context.SaveChanges();
            return RedirectToAction("GetAllSales");
        }
    }
}