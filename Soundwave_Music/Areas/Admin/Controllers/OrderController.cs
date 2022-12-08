using PagedList;
using Soundwave_Music.Common.Helper;
using Soundwave_Music.Common.NotificationLib;
using Soundwave_Music.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Soundwave_Music.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        SoundwaveDbContext _appdb = new SoundwaveDbContext();

        //Order index
        public ActionResult OrderIndex(string search_order, string show_order, int? _sizepage, int? _pagenumber, string sort_order)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true || User.Identity.Permiss_Edit() == true || User.Identity.Permiss_Delete() == true || User.Identity.Permiss_Access() == true)
                {
                    var pageSize = (_sizepage ?? 10);
                    var pageNum = (_pagenumber ?? 1);
                    ViewBag.Current_sort = sort_order;
                    ViewBag.Reset_sort = "";
                    ViewBag.SortByDate = sort_order == "date_asc" ? "date_desc" : "date_asc";
                    var list_order = from s in _appdb.Orders                                    
                                    orderby s.Order_id descending
                                    select s;
                    switch (sort_order)
                    {
                        case "date_desc":
                            ViewBag.sortname = "Sort by: Newest";
                            list_order = from s in _appdb.Orders                                        
                                        orderby s.Order_id descending
                                        select s;
                            break;

                        case "date_asc":
                            ViewBag.sortname = "Sort by: Oldest";
                            list_order = from s in _appdb.Orders                                        
                                        orderby s.Order_id
                                        select s;
                            break;
                    }

                    if (string.IsNullOrEmpty(search_order)) return View(list_order.ToPagedList(pageNum, pageSize));
                    switch (show_order)
                    {
                        //case 1: search all
                        case "1":
                            list_order = (IOrderedQueryable<Order>)list_order.Where(s => s.Order_id.ToString().Contains(search_order) ||
                                                                                       s.User.Email.Contains(search_order) ||
                                                                                       s.Payment.Payment_method.Contains(search_order) ||
                                                                                       s.Product.product_name.Contains(search_order));
                            break;
                        //case 2: search by id
                        case "2":
                            list_order = (IOrderedQueryable<Order>)list_order.Where(s => s.Order_id.ToString().Contains(search_order));
                            break;
                        //case 3: 
                        case "3":
                            list_order = (IOrderedQueryable<Order>)list_order.Where(s => s.User.Email.Contains(search_order));
                            break;
                        //case 4: 
                        case "4":
                            list_order = (IOrderedQueryable<Order>)list_order.Where(s => s.Payment.Payment_method.ToString().Contains(search_order));
                            break;
                        //case 5: 
                        case "5":
                            list_order = (IOrderedQueryable<Order>)list_order.Where(s => s.Product.product_name.ToString().Contains(search_order));
                            break;                        
                    }
                    return View(list_order.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                return Redirect("~/User/SignIn");
            }
        }

        //Create new Order view
        public ActionResult OrderCreate()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true)
                {
                    ViewBag.ListUser = new SelectList(_appdb.Users.Where(a => (a.User_Status == "1")).OrderBy(a => a.Email), "User_id", "Email", 0);
                    ViewBag.ListPayment = new SelectList(_appdb.Payments.Where(s => (s.Status == "1")).OrderBy(s => s.Payment_method), "Payment_id", "Payment_method", 0);
                    ViewBag.ListProduct = new SelectList(_appdb.Products.Where(c => (c.status == "1")).OrderBy(c => c.product_name), "product_id", "product_name", 0);

                    return View();
                }
                else
                {
                    Notification.set_noti("You do not have permission to use this function", "danger");
                    return RedirectToAction("OrderIndex");
                }
            }
            else
            {
                return Redirect("~/User/SignIn");
            }
        }

        //Create new Order code
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate(Order order)
        {
            ViewBag.ListUser = new SelectList(_appdb.Users.Where(a => (a.User_Status == "1")).OrderBy(a => a.Email), "User_id", "Email", 0);
            ViewBag.ListPayment = new SelectList(_appdb.Payments.Where(s => (s.Status == "1")).OrderBy(s => s.Payment_method), "Payment_id", "Payment_method", 0);
            ViewBag.ListProduct = new SelectList(_appdb.Products.Where(c => (c.status == "1")).OrderBy(c => c.product_name), "product_id", "product_name", 0);
            try
            {
                order.Order_date = DateTime.Now;
                order.Total = order.Total;
                order.User_id = order.User_id;
                order.Payment_id = order.Payment_id;
                order.product_id = order.product_id;                
                _appdb.Orders.Add(order);
                _appdb.SaveChanges();
                Notification.set_noti("Create successfully new order: " + order.Order_id + "", "success");
                return RedirectToAction("OrderIndex");
            }
            catch
            {
                Notification.set_noti("Error!!!", "danger");
            }
            return View(order);
        }

        //Order Detail
        public ActionResult OrderDetail(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.Identity.Permiss_Create() == true || User.Identity.Permiss_Update() == true || User.Identity.Permiss_Edit() == true || User.Identity.Permiss_Delete() == true || User.Identity.Permiss_Access() == true)
                {
                    var list_order = (from s in _appdb.Orders
                                      where s.Order_id == id
                                      orderby s.Order_date descending
                                      select s).FirstOrDefault();
                    if (list_order != null && id != null) return View(list_order);
                    Notification.set_noti("This order is not exist: " + list_order.Order_id + "", "warning");
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("OrderIndex");
                }
            }
            else
            {
                return Redirect("~/User/SignIn");
            }
        }

        [HttpPost]
        public JsonResult SuggestOrderSearch(string Prefix)
        {
            var search_order = (from s in _appdb.Orders
                                where s.User.Email.StartsWith(Prefix) || s.Payment.Payment_method.EndsWith(Prefix) || s.Product.product_name.EndsWith(Prefix)
                                orderby s.Order_id ascending
                                select new { s.Order_id });
            return Json(search_order, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appdb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}