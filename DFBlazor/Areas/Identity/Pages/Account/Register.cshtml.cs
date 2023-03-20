using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace DFBlazor.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private string ErrorMsg = "";

        public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
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

                if (result.Succeeded) {
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
        }
    }
}
