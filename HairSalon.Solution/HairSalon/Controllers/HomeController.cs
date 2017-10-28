using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Stylists()
      {
        List<Stylist> model = Stylist.GetAll();
        return View(model);
      }

      // [HttpPost("/stylists/new")]
      // public ActionResult Stylists()
      // {
      //   Stylist newStylist = new Stylist(Request.Form["stylist-name"], (Int32.Parse(Request.Form["rate"])), Request.Form["skills"]);
      //   newStylist.Save();
      //   List<Stylist> allStylists = Stylist.GetAll();
      //   return View(allStylists);
      }
    }
}
