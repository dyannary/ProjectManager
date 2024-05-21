using MediatR;
using ProjectManager.Application.Projects.Queries;
using ProjectManager.Application.User.Queries;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var cards = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Page = 1,
                PageSize = 10,
            });

            return View(cards);
        }
    }
}