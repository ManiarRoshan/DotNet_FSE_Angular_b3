using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace WebApplication4.Controllers
{
    [Route("feedback")]
    public class FeedbackController : Controller
    {
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("submit")]
        public IActionResult Submit(string Name,string Comments,int Rating)
        {
            if (Rating >= 4)
            {
                ViewData["StatusMessage"] = "Thank you..."; 

            }
            else
            {
                ViewData["StatusMessage"] = "We appreciate your input and will work to improve.";
            }

            ViewData["UserName"] = Name;
            ViewData["UserComments"] = Comments;
            ViewData["UserRating"] = Rating;
            return View("Index");
        }
    }
}
