using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.Projects.Commands.Create;
using ProjectManager.Application.Projects.Commands.Update;
using ProjectManager.Application.Projects.Queries;
using ProjectManager.Application.ProjectStates.Queries;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize(Roles = "user")]
    public class ProjectController : Controller
    {

        private readonly IMediator _mediator;
        private readonly int DefaultPageSize = 12;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public int GetUserId()
        {
            int id = ClaimsExtensions.GetUserId(User);
            return id; 
        }


        public async Task<ActionResult> Index()
        {
            var response = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Page = 1,
                PageSize = DefaultPageSize,
                UserID = GetUserId(),
                IsDeleted = "activated"
            });

            ViewBag.CurrentPage = 1;
            ViewBag.MaxPage = response.MaxPage;

            return View(response.Cards);
        }

        [HttpGet]
        public async Task<ActionResult> GetByFilters(string projectType, string projectStatus, string searchProject, string ProjectEnable, string SortBy, string SortOrd, int page)
        {
            var response = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Name = searchProject,
                Owning = projectType,
                Status = projectStatus,
                IsDeleted = ProjectEnable,
                Page = page,
                PageSize = DefaultPageSize,
                UserID = GetUserId()
            });

            ViewBag.CurrentPage = page;
            ViewBag.MaxPage = response.MaxPage;

            return PartialView("_ProjectCards", response.Cards);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectToCreateDto projectDto)
        {
            try
            {
                var responseCreate = await _mediator.Send(new CreateProjectCommand
                {
                    Project = projectDto,
                    UserID = GetUserId()
                });

                if (responseCreate)
                {
                    return Json(new { StatusCode = 201 });
                }
                else
                {
                    return Json(new { StatusCode = 500 });
                }

            } catch
            {
                return Json(new { StatusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProjectByIdDto projectDto)
        {
            try
            {
                var responseUpdate = await _mediator.Send(new UpdateProjectCommand
                    {
                        Project = projectDto,
                        UserID = GetUserId()
                    });

                if (responseUpdate)
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

        [HttpPost]
        public async Task<ActionResult> UpdateEnableStatus (int id)
        {
            try
            {
                var isDeleted = await _mediator.Send(new UpdateEnableProjectCommand
                {
                    Id = id
                });

                if (isDeleted)
                    return Json(new { StatusCode = 204 });
                else
                    return Json(new { StatusCode = 500 });
            }
            catch
            {
                return Json(new { StatusCode = 500 });
            }
        }

        // Modals : This methods open modals

        public async Task<ActionResult> OpenCreateModal()
        {

            var responseProjectStates = await _mediator.Send(new GetProjectStatesQuerry { });

            ViewBag.ProjectStates = new SelectList(responseProjectStates, "Id", "Name");
            return PartialView("_CreateProjectModal");
        }
        public ActionResult OpenDisableProjectModal(int id)
        {
            return PartialView("_ConfirmDisableModal", id);
        }

        public async Task<ActionResult> OpenUpdateModal(int id)
        {
            var response = await _mediator.Send(new GetProjectByIdQuerry
            {
                Id = id
            });

            var responseProjectStates = await _mediator.Send(new GetProjectStatesQuerry { });

            ViewBag.ProjectStates = new SelectList(responseProjectStates, "Id", "Name");

            return PartialView("_UpdateProjectModal", response);
        }

        public ActionResult GotoProjectDetails(int id)
        {
            return RedirectToAction("Index", "ProjectTask", new {Id = id});
        }

    }
}