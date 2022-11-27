namespace QueryServiceSystem2.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using QueryServiceSystem2.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static Data.DataConstants.User;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Имейла е задължителен")]
            [EmailAddress(ErrorMessage = "Полето трябва да съдържа валиден имейл адрес")]
            [Display(Name = "Имейл:")]
            public string Email { get; set; }

            [Display(Name = "Три имена:")]
            [StringLength(FullNameMaxLength, ErrorMessage = "{0} трябва да съдържа минимум {2} и максимум {1} символа.", MinimumLength = FullNameMinLength)]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Паролата е задължителна")]
            [StringLength(PasswordMaxLength, ErrorMessage = "{0} трябва да съдържа минимум {2} и максимум {1} символа.", MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола:")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Паролата за потвърждение не съвпада с основната пароола, моля опитайте отново.")]
            [Display(Name = "Потвърждение на парола:")]

            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.FullName
                };

                var result = await this.userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
