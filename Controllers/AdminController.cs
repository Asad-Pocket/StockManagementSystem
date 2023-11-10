using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Data;
using StockManagementSystem.Entities;
using StockManagementSystem.MembershipServices;
using StockManagementSystem.Models;

namespace StockManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AdminController(UserManager<ApplicationUser> manager,RoleManager<ApplicationRole> roleManager)
        {
            _userManager = manager;
            _roleManager = roleManager;
        }
        public IActionResult ShowUsers()
        {
            var users = _userManager.Users.ToList();
            var _roles = _roleManager.Roles.ToList();
            return View(users);
        }
        public IActionResult Edit(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.UserName).Result;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                // Update other properties as needed.
                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("ShowUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            return View(user);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var result = _userManager.DeleteAsync(user).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("ShowUsers");
            }
            return View(user);
        }
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                // Check if the role already exists
                var roleExists = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
                {
                    // Role does not exist, so create it
                    var newRole = new ApplicationRole(roleName);
                    var result = await _roleManager.CreateAsync(newRole);

                    if (result.Succeeded)
                    {
                        // Role created successfully
                        return RedirectToAction("Index"); // Redirect to a success page or your desired action
                    }
                    else
                    {
                        // Handle errors, if any
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    // Role already exists
                    ModelState.AddModelError(string.Empty, "Role already exists.");
                }
            }
            else
            {
                // Handle invalid role name
                ModelState.AddModelError(string.Empty, "Role name is required.");
            }

            // If there are errors, return to the same page with error messages
            return View();
        }

        //public IActionResult AssignRole(string id)
        //{
        //    var user = _userManager.FindByIdAsync(id).Result;

        //    if (user == null)
        //    {
        //        return NotFound(); // Handle when the user is not found
        //    }

        //    var userRoles = _userManager.GetRolesAsync(user).Result;
        //    var model = new AssignRoleViewModel
        //    {
        //        UserId = user.Id,
        //        UserName = user.UserName,
        //        UserRoles = userRoles
        //    };

        //    return View(model);
        //}
        //[HttpPost]
        //public IActionResult AssignRole(AssignRoleViewModel model)
        //{
        //    var user = _userManager.FindByNameAsync(model.UserName).Result;

        //    if (user == null)
        //    {
        //        return NotFound(); // Handle when the user is not found
        //    }

        //    // Check if the selected role is valid
        //    else
        //    {
        //        var result = _userManager.AddToRoleAsync(user, model.AddRole).Result;

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("ShowUsers");
        //        }
        //    }

        //    // Handle errors, if any
        //    ModelState.AddModelError(string.Empty, "Role assignment failed. Please try again.");
        //    return View(model);
        //}
        public IActionResult AssignRole(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = _userManager.GetRolesAsync(user).Result;
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var model = new AssignRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles,
                AllRoles = allRoles // Populate AllRoles 
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult AssignRole(AssignRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(model.UserId.ToString()).Result;
                var reolexist = _userManager.GetRolesAsync(user).Result; 
                var _result = _userManager.RemoveFromRolesAsync(user, reolexist).Result;
                var result = _userManager.AddToRolesAsync(user, model.UserRoles).Result;
                if (result.Succeeded && _result.Succeeded)
                    return RedirectToAction("ShowUsers");
            }

            // If model validation fails, return the same view with validation errors.
            return View(model);
        }


    }
}
