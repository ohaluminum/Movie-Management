using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MovieManagement.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld

        public IActionResult Index()
        {
            return View();
        }


        // GET: HelloWorld/Welcome 

        public IActionResult Welcome(string name, int id = 1)
        {
            // The ViewData dictionary object contains data that will be passed to the view.
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = id;

            return View();
        }
    }
}
