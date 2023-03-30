using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DFBlazor.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet() {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync() {
            ReturnUrl = Url.Content("~/");

            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, isPersistent: false, lockoutOnFailure: false);
                var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                var roles = await _signInManager.UserManager.GetRolesAsync(user);
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, Input.Email));

                foreach(var role in roles) {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                if (result.Succeeded) {
                    return LocalRedirect(ReturnUrl);
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
        }
    }
}
