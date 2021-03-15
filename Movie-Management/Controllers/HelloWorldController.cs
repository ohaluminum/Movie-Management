using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Movie_Management.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/

        public string Index()
        {
            return "This is my default action...";
        }

        // GET: /HelloWorld/Welcome/ 

        public string Welcome(string name, int id = 1)
        {
            // NOTE: The simple way to return string: return $"Hello {name}, ID is: {id}";

            // Use HtmlEncoder.Default.Encode to protect the app from malicious input (such as through JavaScript).
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {id}");
        }
    }
}
