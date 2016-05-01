using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace FlightGates.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Index(string id)
        {
            if (id == null || !Regex.IsMatch(id, @"^[-\w]+$"))
                throw new ArgumentException("Illegal template name", "id");

            return View(id, "_TemplateLayout");
        }
    }
}