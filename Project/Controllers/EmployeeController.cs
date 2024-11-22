using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Models;
using Serilog;
namespace IdentityManagement.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public EmployeeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


       public IActionResult Profile()
    {
       
        return View();
    }
    public new IActionResult Request()
    {
       
        return View();
    }
    public new IActionResult ViewData()
    {
       
        return View();
    }
   

   
     [HttpGet]
    public IActionResult EmployeeLogin()
    {
       
        return View();
    }
 
    [HttpPost]
     public IActionResult EmployeeLogin(User user)
    {
       string result= Database.EmployeeLogin(user);
       Console.WriteLine(result);
       if(result=="success")
       {
          Log.Information("Employee Login Triggered");  
          return View("Profile",user);

       }
      
       return View("EmployeeLogin",user);
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
