using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GZTimeServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;

namespace GZTimeServer.Controllers
{
    public class TestController : Controller
    {
        [Route("Test")]
        public IActionResult Index()
        {
            TestViewModel TestData = new TestViewModel();
            TestData.Title = "Test View";
            TestData.ViewRow = 4;
            TestData.Info = new List<string>(new string[] {
                "Your IP: \t" + HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + "\n" +
                "Your Token: \t" + Request.Cookies["token"] + "\n" + 
                "Your UA: \t" + Request.Headers["User-Agent"]
        });

            return View("TestView",TestData);
        }
    }
}
