using FirstMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Hello from action method.");
            return View();
        }


        [HttpPost]
        public IActionResult Index(string firstName, string lastName, DateTime dateOfBirth)
        {
            Person person = new Person();
            person.FirstName = firstName;
            person.LastName = lastName;
            person.DateOfBirth = dateOfBirth;

            Attendance.AddAttendant(person);
            TempData["FirstName"] = firstName + " " + lastName;
            return RedirectToAction("Index");

        }


    }
}
