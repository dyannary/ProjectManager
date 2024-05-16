using MediatR;
using Microsoft.Owin.Security;
using ProjectManager.Application.User.Queries;
using ProjectManager.Presentation.Models;
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

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _mediator.Send(new GetUserByUsernameAndPasswordQuerry
                {
                    UserName = model.Username,
                    Password = model.Password,
                });

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                    return View();
                }else if (user.IsEnabled == false)
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
                    claim.AddClaim(new Claim (ClaimsIdentity.DefaultRoleClaimType, user.Role, ClaimValueTypes.String));

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

        public ActionResult Logout()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}