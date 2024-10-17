using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using mytecsys.Models;

namespace mytecsys.Controllers
{
    public class UserInfoController : Controller
    {
        private YourDbContext db = new YourDbContext();

        // Display the list of users and the form
        public ActionResult Index()
        {
            var users = db.UserInfoes.ToList(); // Fetch all users from the database
            return View(users); // Return the view with the list of users
        }

        // Fetch cities based on the selected state
        [HttpGet]
        public JsonResult GetCities(string state)
        {
            var cities = new List<string>(); // Initialize an empty list of cities
            if (state == "Maharashtra")
            {
                cities = new List<string> { "Mumbai", "Pune", "Nagpur" }; // Add cities for Maharashtra
            }

            return Json(cities, JsonRequestBehavior.AllowGet); // Return the cities as JSON
        }

        // Save or update user info
        [HttpPost]
        public JsonResult SaveUserInfo(UserInfo model)
        {
            if (ModelState.IsValid) // Check if the model is valid
            {
                if (model.Id == 0) // If Id is 0, add new user
                {
                    db.UserInfoes.Add(model); // Add new user to the database
                }
                else // Otherwise, update the existing user
                {
                    var user = db.UserInfoes.FirstOrDefault(x => x.Id == model.Id); // Find the user by Id
                    if (user != null)
                    {
                        user.Name = model.Name; // Update name
                        user.MobileNo = model.MobileNo; // Update mobile number
                        user.State = model.State; // Update state
                        user.City = model.City; // Update city
                        user.Address = model.Address; // Update address
                    }
                }
                db.SaveChanges(); // Commit changes to the database
                return Json(new { success = true }); // Return success response
            }
            return Json(new { success = false }); // Return failure response
        }

        // Get user data by ID for editing
        [HttpGet]
        public JsonResult GetUserById(int id)
        {
            var user = db.UserInfoes.FirstOrDefault(x => x.Id == id); // Find the user by Id
            return Json(user, JsonRequestBehavior.AllowGet); // Return user data as JSON
        }

        // Delete a user
        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            var user = db.UserInfoes.FirstOrDefault(x => x.Id == id); // Find the user by Id
            if (user != null)
            {
                db.UserInfoes.Remove(user); // Remove the user from the database
                db.SaveChanges(); // Commit the changes
                return Json(new { success = true }); // Return success response
            }
            return Json(new { success = false }); // Return failure response if the user is not found
        }
    }
}
