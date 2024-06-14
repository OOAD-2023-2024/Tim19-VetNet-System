// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using VetNet.Data;
using VetNet.Models;
using static VetNet.Models.Korisnik;



namespace VetNet.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator, Veterinar")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Korisnik> _signInManager;
        private readonly UserManager<Korisnik> _userManager;
        private readonly IUserStore<Korisnik> _userStore;
        private readonly IUserEmailStore<Korisnik> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<Korisnik> userManager,
            IUserStore<Korisnik> userStore,
            SignInManager<Korisnik> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Tip korisnika")]
            public int Role { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Ime")]
            [Required(ErrorMessage = "Obavezna vrijednost")]
            public string ime { get; set; }

            [Required(ErrorMessage = "Obavezna vrijednost")]
            [Display(Name = "Prezime")]
            public string prezime { get; set; }

            [Required(ErrorMessage = "Obavezna vrijednost")]
            [Display(Name = "Spol")]
            public Spol spol { get; set; }

            [Required(ErrorMessage = "Obavezna vrijednost")]
            [Display(Name = "Adresa")]
            public string adresa { get; set; }

            [Required(ErrorMessage = "Obavezna vrijednost")]
            [Display(Name = "Datum rođenja")]
            public DateOnly datumRodjenja { get; set; }

            [Phone(ErrorMessage = "Neispravan broj telefona")]
            [Required(ErrorMessage = "Obavezna vrijednost")]
            [Display(Name = "Broj Telefona")]
            public string brojTelefona { get; set; }

            [Display(Name = "Poslovnica")]
            [ForeignKey("Poslovnica")]
            [AllowNull]
            public int? PoslovnicaId { get; set; }

            public Poslovnica? Poslovnica { get; set; }

            [Display(Name = "Veterinarska služba")]
            [ForeignKey("VeterinarskaSluzba")]
            [AllowNull]
            public int? VeterinarskaSluzbaId { get; set; }

            public VeterinarskaSluzba? VeterinarskaSluzba { get; set; }

            [AllowNull]
            [Display(Name = "Specijalizacija")]
            public Specijalizacija? specijalizacija { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ViewData["spol"] = new SelectList(Enum.GetValues(typeof(Spol)).Cast<Spol>());
            ViewData["specijalizacija"] = new SelectList(Enum.GetValues(typeof(Korisnik.Specijalizacija)).Cast<Korisnik.Specijalizacija>().Select(e => new {
                Value = e,
                Text = System.Text.RegularExpressions.Regex.Replace(e.ToString(), "(\\B[A-Z])", " $1")
            }), "Value", "Text");
            ViewData["PoslovnicaId"] = new SelectList(_context.Poslovnica, "id", "naziv");
            ViewData["VeterinarskaSluzbaId"] = new SelectList(_context.VeterinarskaSluzba, "id", "naziv");
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                user.ime = Input.ime;
                user.prezime = Input.prezime;
                user.adresa = Input.adresa;
                user.spol = Input.spol;
                user.datumRodjenja = Input.datumRodjenja;
                user.brojTelefona = Input.brojTelefona;
                if (Input.Role == 3)
                    user.PoslovnicaId = Input.PoslovnicaId;
                if (Input.Role == 4)
                {
                    user.VeterinarskaSluzbaId = Input.VeterinarskaSluzbaId;
                    user.specijalizacija = Input.specijalizacija;
                }
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    SqlConnection connection = new("Data Source=SQL8010.site4now.net;Initial Catalog=db_aa8743_imuslic1;User Id=db_aa8743_imuslic1_admin;Password=ismar123");
                    string sql = "INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@RoleId", Input.Role.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    } else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }



                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private Korisnik CreateUser()
        {
            try
            {
                var user = Activator.CreateInstance<Korisnik>();
                user.EmailConfirmed = true; // Set email confirmed to true by default
                return user;
            } catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Korisnik)}'. " +
                    $"Ensure that '{nameof(Korisnik)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Korisnik> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Korisnik>) _userStore;
        }
    }
}
