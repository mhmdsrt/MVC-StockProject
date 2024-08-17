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
    public class CustomerController : Controller
    {
        ProductsCustomersEntities1 context = new ProductsCustomersEntities1();

        [HttpGet]
        public ActionResult GetAllCustomer(int page=1)
        {
            var log = context.Customers.ToList().ToPagedList(page,10);
            return View(log);
        }

        [HttpGet]
        public ActionResult InsertCustomer()
        {
            return View();
        }


        [HttpPost] // InsertCustomer View sayfasındaki <form>dan gelen verileri alır.
        public ActionResult InsertCustomer(Customers entity)
        {
            if (!ModelState.IsValid)
            {
                return View("InsertCustomer");
            }
            context.Customers.Add(entity);
            context.SaveChanges();
            return RedirectToAction("GetAllCustomer"); // Redirect -> Yönlendir , Parametre olarak bu Metotdan sonra  yani
                                                       // Post işleminden sonra yönlendireceğimiz action(eylemi) belirtiyoruz.
        }

        [HttpGet]

        public ActionResult DeleteCustomer(int id)
        {
            /*
            GetAllCustomer View sayfasındaki "/Customer/DeleteCustomer/@customer.CustormerID" ifadesi ile 
            buradaki DeleteCustomer Action'una parametre olarak CustormerID gönderiyoruz
            */
            var deleteToEntity = context.Customers.Find(id);
            if (deleteToEntity != null)
            {
                context.Customers.Remove(deleteToEntity);
                context.SaveChanges();
            }
            return RedirectToAction("GetAllCustomer");
        }

        [HttpGet]
        public ActionResult GetCustomerToUpdate(int id) // GetCustomerToUpdate View sayfasına verileri taşımak için kullanulan metot
        {
            var customerToUpdate = context.Customers.Find(id);

            return View(customerToUpdate);
        }

        [HttpPost]


        public ActionResult UpdateCustomer(Customers entity)
        {
            //Formdan gönderilen entity'nin id numarasına ait veri tabanındaki gerçek veriyi bulalım.
            //Sonra formdan gönderilen entity'nin property'lerini gerçek verinin propertylerini atayalım.
            var customerToUpdate = context.Customers.Find(entity.CustormerID); 
            customerToUpdate.CustomerFirstName = entity.CustomerFirstName;
            customerToUpdate.CustomerSurName = entity.CustomerSurName;
            customerToUpdate.CustomerAge = entity.CustomerAge;
            customerToUpdate.CustomerCity = entity.CustomerCity;
            customerToUpdate.CustomerCountry = entity.CustomerCountry;
            context.SaveChanges();
            return RedirectToAction("GetAllCustomer");
        }
    }
}