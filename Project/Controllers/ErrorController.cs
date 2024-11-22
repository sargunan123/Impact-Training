using Microsoft.AspNetCore.Mvc;
namespace IdentityManagement.Controllers{
    public class ErrorController : Controller{
        public IActionResult Index(){
            return View();
        }
    }
}