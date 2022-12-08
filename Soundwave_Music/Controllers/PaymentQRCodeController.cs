using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Soundwave_Music.Controllers
{
    public class PaymentQRCodeController : Controller
    {
        public ActionResult MomoQRCode()
        {
            return View();
        }

        public ActionResult ZaloPayQRCode()
        {
            return View();
        }
    }
}