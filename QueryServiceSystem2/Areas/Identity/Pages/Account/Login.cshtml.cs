namespace QueryServiceSystem2.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using QueryServiceSystem2.Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LoginModel(SignInManager<User> signInManager) 
            => this.signInManager = signInManager;

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Имейла е задължителен")]
            [EmailAddress(ErrorMessage = "Полето трябва да съдържа валиден имейл адрес")]
            [Display(Name = "Имейл:")]
            public string Email { get; set; }

            [Required(ErrorMessage ="Паролата е задължителна")]
            [Display(Name = "Парола:")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Запомни ме!")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Грешен имейл или парола, моля опитайте отново.");
                    return Page();
                }
            }

            return Page();
        }
    }
}
