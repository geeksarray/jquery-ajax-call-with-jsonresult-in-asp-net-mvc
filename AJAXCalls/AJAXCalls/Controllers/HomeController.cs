using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AJAXCalls.Models;

namespace AJAXCalls.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {            
           return RedirectToAction("ShowCategoryProducts");
           
        }

        public ActionResult ShowCategoryProducts()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem
            {
                Text = "Select Category",
                Value = "0",
                Selected = true
            });

            items.Add(new SelectListItem { Text = "Beverages", Value = "1" });

            items.Add(new SelectListItem { Text = "Condiments", Value = "2" });

            items.Add(new SelectListItem { Text = "Confections", Value = "3" });

            items.Add(new SelectListItem { Text = "Dairy Products", Value = "4" });

            items.Add(new SelectListItem { Text = "Grains/Cereals", Value = "5" });

            items.Add(new SelectListItem { Text = "Meat/Poultry", Value = "6" });

            items.Add(new SelectListItem { Text = "Produce", Value = "7" });

            items.Add(new SelectListItem { Text = "Seafood", Value = "8" });

            ViewBag.CategoryType = items;

            return View();
        }

        public JsonResult GetProducts(string id)
        {
            List<Product> products = new List<Product>();

            string query = string.Format("SELECT  [ProductID], [ProductName], [QuantityPerUnit],[UnitPrice],[UnitsInStock],[ReorderLevel] " +
              " FROM [Northwind].[dbo].[Products] WHERE CategoryID = {0}",id);

            using (SqlConnection con = new SqlConnection("Data Source=AADITYA-PC;Initial Catalog=Northwind;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product 
                            { 
                                ProductID = reader.GetValue(0).ToString(),
                                ProductName = reader.GetString(1),
                                QuantityPerUnit = reader.GetString(2),
                                UnitPrice = reader.GetValue(3).ToString(),
                                UnitsInStock = reader.GetValue(4).ToString(), 
                                ReorderLevel = reader.GetValue(5).ToString()  
                            }
                        );    
                    }
                }
            }

            return Json(products, JsonRequestBehavior.AllowGet);
        }

	}
}