using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Web;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace IdentityManagement.Controllers;

public class AdminController : Controller
{
   
     [HttpGet]
    public IActionResult AdminLogin()
    {
       
        return View();
    }
    
    [HttpPost]
     public IActionResult AdminLogin(User user)
    {
       string result= Database.AdminLogin(user);
       Console.WriteLine(result);
       if(result=="success")
       {
         Log.Information("Employee Login Triggered");
          HttpContext.Session.SetString("EmailId ", user.EmailId);
          return View("Success",user);
       }
      
       return View("AdminLogin",user);
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
