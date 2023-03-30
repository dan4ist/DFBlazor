using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Security.Claims;

namespace DFBlazor.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string ErrorMsg = "";

        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync() {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid) {
                var identityUser = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(identityUser, Input.Password);

                //add claims
                var claim = new Claim("city", Input.City.ToLower());
                var claimsResult = await _userManager.AddClaimAsync(identityUser, claim);


                //add role to db
                bool roleExists = await _roleManager.RoleExistsAsync(Input.Role);
                bool roleHandled = false;

                if (!roleExists) {
                    var role = new IdentityRole(Input.Role);
                    var addRoleResult = await _roleManager.CreateAsync(role);

                    if (addRoleResult.Succeeded) {
                        roleHandled = true;
                    }
                } else {
                    roleHandled = true;
                }

                //add user to role
                bool userRoleExists = await _userManager.IsInRoleAsync(identityUser, Input.Role);
                bool userRoleHandled = false;

                if (!userRoleExists) {
                    var addUserRoleResult = await _userManager.AddToRoleAsync(identityUser, Input.Role);
                    if (addUserRoleResult.Succeeded) {
                        userRoleHandled = true;
                    }
                } else {
                    userRoleHandled = true;
                }

                if (result.Succeeded 
                    && claimsResult.Succeeded
                    && roleHandled
                    && userRoleHandled) {

                    await _signInManager.SignInAsync(identityUser, isPersistent: false);
                    return LocalRedirect(ReturnUrl);

                } else if (!result.Succeeded) {
                    ErrorMsg = result.Errors.First().Description;
                }
            }

            return Page();
        }

        public class InputModel {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string Role { get; set; }
        }
    }
}
