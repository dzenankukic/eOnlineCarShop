using eOnlineCarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.Controllers
{
    public class SMSController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(SMSMessageVM message)
        {
            var results = SMS.Send(new SMS.SMSRequest
            {
                from = "Your mobile shop" /*Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"]*/,
                to = message.To,
                text = message.ContentMsg
            });
            return View();
        }
    }
}
