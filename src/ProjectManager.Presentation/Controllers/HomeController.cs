using MediatR;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.Notifications.Queries;
using ProjectManager.Application.UserManagement.Queries.GetUserPhoto;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetNotificationCount()
        {
            int count = await _mediator.Send(new GetNumberOfNotificationQuerry
            {
                ForUserId = GetUserId(),
            });

            return Json(count, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> getUserPhoto()
        {
            string photoPath = await _mediator.Send(new GetUserPhotoByEmailQuerry 
            { 
                Email = User.Identity.Name 
            });

            return Json(photoPath, JsonRequestBehavior.AllowGet);
        }

        public int GetUserId()
        {
            int id = ClaimsExtensions.GetUserId(User);
            return id;
        }

    }
}