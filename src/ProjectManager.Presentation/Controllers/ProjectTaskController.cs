using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.Projects.Queries;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    public class ProjectTaskController : Controller
    {

        private readonly IMediator _mediator;

        public ProjectTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: ProjectTask
        public async Task<ActionResult> Index(int? Id)
        {

            var responseProjectList = await _mediator.Send(new GetProjectsForDropDownQuerry() { UserID = GetUserId() });

            if (!responseProjectList.Any()){
                return HttpNotFound();
            }

            int ProjectId = responseProjectList.FirstOrDefault().Id;

            if (Id != null)
                ProjectId = Id.Value;

            var responseProject = await _mediator.Send(new GetProjectByIdForProjectTaskQuerry
            {
                Id = ProjectId,
                LoggedUserId = GetUserId()
            });

            var model = new ProjectTaskDto
            {
                Project = responseProject,
                ProjectsList = responseProjectList.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> RefreshDetailCard(int id)
        {
            var viewModel = await _mediator.Send(new GetProjectByIdForProjectTaskQuerry 
            {
                Id = id,
                LoggedUserId = GetUserId()
            });

            return PartialView("_CardInfoPartial", viewModel);
        }

        public ActionResult GoBackToProjects()
        {
            return RedirectToAction("Index", "Project");
        }

        public int GetUserId()
        {
            var user = User;
            int id = ClaimsExtensions.GetUserId(user);
            return id;
        }

    }
}