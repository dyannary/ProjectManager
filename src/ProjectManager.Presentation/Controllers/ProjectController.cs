using MediatR;
using ProjectManager.Application.DTOs.Projects;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.Projects.Commands.Create;
using ProjectManager.Application.Projects.Commands.Update;
using ProjectManager.Application.Projects.Queries;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize(Roles = "user")]
    public class ProjectController : Controller
    {

        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public int GetUserId()
        {
            var user = User;
            int id = ClaimsExtensions.GetUserId(user);
            return id; 
        }


        public async Task<ActionResult> Index()
        {
            var response = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Page = 1,
                PageSize = 10,
                UserID = GetUserId(),
                IsDeleted = "activated"
            });
            ViewBag.pageFrom = response.FromPage;
            ViewBag.currentPage = 1;
            ViewBag.pageCount = response.MaxPage;

            return View(response.Cards);
        }

        [HttpGet]
        public async Task<ActionResult> GetByFilters(string projectType, string projectStatus, string searchProject, string ProjectEnable)
        {
            var response = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Name = searchProject,
                Owning = projectType,
                Status = projectStatus,
                IsDeleted = ProjectEnable,
                Page = 1,
                PageSize = 10,
                UserID = GetUserId()

            });

            return PartialView("_ProjectCards", response.Cards);
        }

        [HttpPost]
        public async Task<ActionResult> Pagination(int page, int pageSize)
        {
            // This mnethod is not finished. It is for pagination that will be added later.
            var response = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Page = page,
                PageSize = pageSize,
                UserID = GetUserId()
            });

            ViewBag.pageFrom = response.FromPage;
                ViewBag.currentPage = page;
            ViewBag.pageCount = response.MaxPage;

            return PartialView("_ProjectCards", response.Cards);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectToCreateDto projectDto)
        {
            var responseCreate = await _mediator.Send(new CreateProjectCommand
            {
                Project = projectDto,
                UserID = GetUserId()
            });

            var responseCards = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Page = 1,
                PageSize = 10,
                UserID = GetUserId()
            });

            return PartialView("_ProjectCards", responseCards.Cards);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProjectByIdDto projectDto)
        {
            var responseUpdate = await _mediator.Send(new UpdateProjectCommand
            {
                Project = projectDto,
                UserID = GetUserId()
            });

            var responseCards = await _mediator.Send(new GetProjectsByFilterQuery
            {
                Page = 1,
                PageSize = 10,
                UserID = GetUserId()
            });

            return PartialView("_ProjectCards", responseCards.Cards);
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

                var response = await _mediator.Send(new GetProjectsByFilterQuery
                {
                    Page = 1,
                    PageSize = 10,
                    UserID = GetUserId()
                });

                if (isDeleted)
                    return PartialView("_ProjectCards", response.Cards);
                else
                    return Json(new { StatusCode = 500 });
            }
            catch
            {
                return Json(new { StatusCode = 500 });
            }
        }

        // Modals : This methods open modals

        public ActionResult OpenCreateModal()
        {
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

            return PartialView("_UpdateProjectModal", response);
        }

    }
}