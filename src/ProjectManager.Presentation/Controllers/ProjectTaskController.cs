using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.Projects.Queries;
using ProjectManager.Application.ProjectTasks.Queries;
using ProjectManager.Application.ProjectTasks.Queries.GetTasksByFilterQuery.cs;
using ProjectManager.Application.TableParameters;
using ProjectManager.Application.User.Commands.CreateUser;
using ProjectManager.Application.User.Queries;
using ProjectManager.Application.UserManagement.Queries.GetUsersRole;
using System.Linq;
using System.Threading;
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

            var projectTask = await _mediator.Send(new GetTasksByProjectId()
            {
                Id = ProjectId
            });

            var model = new ProjectTaskDto
            {
                Project = responseProject,
                ProjectsList = responseProjectList.ToList(),
                ProjectTasks = projectTask.Where(task => task.ProjectName.ToLower() == responseProject.Name.ToLower()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> TasksTable(DataTableParameters parameters, CancellationToken cancellationToken)
        {
            var tasks = await _mediator.Send(new GetTasksByFilterQuery(parameters), cancellationToken);

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = tasks
            });
        }

        #region Add Task
        public async Task<ActionResult> AddTask(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery
            {
                Id = id
            });

            var responseUsersRoles = await _mediator.Send(new GetUsersRoleQuery { });

            ViewBag.UserRoles = new SelectList(responseUsersRoles, "Id", "Name");

            return PartialView("_AddUserModal", response);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask(AddUserDto data)
        {
            try
            {
                var addedUser = await _mediator.Send(new AddUserCommand { Data = data });
                if (addedUser)
                {
                    return Json(new { StatusCode = 201 });
                }
                else
                {
                    return Json(new { StatusCode = 500 });
                }
            }
            catch
            {
                return Json(new { StatusCode = 500 });
            }
        }

        #endregion


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

        [HttpGet]
        public async Task<ActionResult> RefreshTable(int id)
        {
            var viewModel = await _mediator.Send(new GetTasksByProjectId
            {
                Id = id
            });

            return PartialView("_ProjectTasksTable", viewModel);
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