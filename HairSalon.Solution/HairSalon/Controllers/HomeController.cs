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
      return View("Stylists", model);
    }

    [HttpGet("/stylists/new")]
    public ActionResult StylistForm()
    {
      return View();
    }

    [HttpPost("/stylists/new")]
    public ActionResult AddStylist()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"], Int32.Parse(Request.Form["rate"]), Request.Form["skills"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();

      return View("Stylists", allStylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      model.Add("selected-client", null);
      Stylist selectedStylist = Stylist.Find(id);
      model.Add("this-stylist", selectedStylist);
      List<Client> stylistClients = Client.GetAllClientsByStylist(selectedStylist.Id);
      model.Add("stylist-clients", stylistClients);
      return View(model);
    }

    [HttpGet("/stylists/{id}/clients/new")]
    public ActionResult ClientForm(int id)
    {
      Stylist selectedStylist = Stylist.Find(id);
      return View(selectedStylist);
    }

    [HttpPost("/stylists/{id}/clients/new")]
    public ActionResult AddClient(int id)
    {
      Client newClient = new Client(Request.Form["client-name"], Int32.Parse(Request.Form["birthday"]), Request.Form["email"], id);
      newClient.Save();

      Dictionary<string, object> model = new Dictionary<string, object>{};
      model.Add("selected-client", null);
      Stylist selectedStylist = Stylist.Find(id);
      model.Add("this-stylist", selectedStylist);
      List<Client> stylistClients = Client.GetAllClientsByStylist(selectedStylist.Id);
      model.Add("stylist-clients", stylistClients);
      return View("StylistDetail", model);
    }

    [HttpGet("/stylists/{id}/clients/{clientId}")]
    public ActionResult ClientDetails(int id, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      List<Client> allClients = Client.GetAllClientsByStylist(id);
      Stylist selectedStylist = Stylist.Find(id);
      Client selectedClient = Client.Find(clientId);
      model.Add("stylist-clients", allClients);
      model.Add("this-stylist", selectedStylist);
      model.Add("selected-client", selectedClient);
      return View("StylistDetail", model);
    }

    [HttpPost("/stylists/{id}/delete")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist selectedStylist = Stylist.Find(id);
      selectedStylist.Delete();
      Client.DeleteClientsByStylist(id);
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Stylists", allStylists);

    }

    [HttpPost("/stylists/{id}/clients/{clientId}/delete")]
    public ActionResult DeleteClient(int id, int clientId)
    {
      Client selectedClient = Client.Find(clientId);
      selectedClient.Delete();

      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("selected-client", null);

      Stylist selectedStylist = Stylist.Find(id);
      model.Add("this-stylist", selectedStylist);

      List<Client> allClientsByStylist = Client.GetAllClientsByStylist(id);
      model.Add("stylist-clients", allClientsByStylist);
      return View("StylistDetail", model);
    }
  }
}
