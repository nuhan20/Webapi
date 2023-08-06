using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<ProductModel> emp;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product").Result;
            emp = response.Content.ReadAsAsync<IEnumerable<ProductModel>>().Result;

            return View(emp);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            return View(new ProductModel());

            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Product/"+id.ToString()).Result;
                return View(response.Content.ReadAsAsync<ProductModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(ProductModel emp)
        {
            if (emp.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Product", emp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Product/"+emp.Id,emp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";

            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Product/"+id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}