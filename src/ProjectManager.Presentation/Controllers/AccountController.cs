using FluentValidation;
using MediatR;
using Microsoft.Owin.Security;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.User.Commands.LoginUser;
using ProjectManager.Application.User.Queries;
using ProjectManager.Application.UserManagement.Commands.UpdatePassword;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly IMediator _mediator;
        private IAuthenticationManager authenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IValidator<LoginUserDto> _loginValidator;
        private readonly IValidator<UserPasswordChangeDto> _changePaswordValidator;
        public AccountController(IMediator mediator, IValidator<LoginUserDto> loginValidator, IValidator<UserPasswordChangeDto> changePaswordValidator)
        {
            _mediator = mediator;
            _loginValidator = loginValidator;
            _changePaswordValidator = changePaswordValidator;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserDto model)
        {
            var validatorResponse = _loginValidator.Validate(model);

            if (!validatorResponse.IsValid)
            {
                foreach (var error in validatorResponse.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            else
            {

                var user = await _mediator.Send(new GetUserByUsernameAndPasswordQuerry
                {
                    UserName = model.UserName,
                    Password = model.Password,
                });

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                    return View();
                }
                else if (user.IsEnabled == false)
                {
                    ModelState.AddModelError("", "Your account is Deactivated.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));

                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> UserSettings(UserPasswordChangeDto entity)
        {
            var validatorResponse = _changePaswordValidator.Validate(entity);

            if (!validatorResponse.IsValid)
            {
                var errors = validatorResponse.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, errors });
            }

            try
            {
                entity.Id = GetUserId();
                var responseChange = await _mediator.Send(new UpdateUserPasswordCommand
                {
                    Passwords = entity
                });

                if (responseChange == "succes")
                    return Json(new { success = true });
                else
                {
                    ModelState.AddModelError("", responseChange);
                    return Json(new { success = false });
                }

            }
            catch
            {
                ModelState.AddModelError("", "Internal server error");
                return Json(new { success = false });
            }
        }

        public ActionResult Logout()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public int GetUserId()
        {
            int id = ClaimsExtensions.GetUserId(User);
            return id;
        }

        #region

        public ActionResult OpenChangePasswordModal()
        {
            return PartialView("_ChangUserPasswordModal", new UserPasswordChangeDto());
        }

        #endregion

    }
}