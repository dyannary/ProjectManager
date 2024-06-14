using MediatR;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.Notification.Queries;
using ProjectManager.Application.Notifications.Commands;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    public class NotificationController : Controller
    {

        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> OpenNotificationModal()
        {

            var response = await _mediator.Send(new GetNotificationListQuerry
            {
                LoggedUserId = GetUserId(),
            });


            return PartialView("_NotificationModal", response);
        }

        public async Task<ActionResult> GetList(string NotificationType)
        {

            var response = await _mediator.Send(new GetNotificationListQuerry
            {
                LoggedUserId = GetUserId(),
            });

            return PartialView("_NotificationList", response);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveSingleNotification(int id)
        {
            var command = await _mediator.Send(new DeleteSingleNotificationCommand
            {
                LoggedUserId = GetUserId(),
                NotificationId = id
            });

            return Json(new {success = command});

        }

        public int GetUserId()
        {
            int id = ClaimsExtensions.GetUserId(User);
            return id;
        }

    }
}