﻿using FluentValidation;
using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.Extensions;
using ProjectManager.Application.Projects.Commands.Create;
using ProjectManager.Application.Projects.Commands.Update;
using ProjectManager.Application.Projects.Queries;
using ProjectManager.Application.ProjectStates.Queries;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize(Roles = "user")]
    public class ProjectController : Controller
    {

        private readonly IMediator _mediator;
        private readonly int DefaultPageSize = 12;
        private readonly IValidator<ProjectToCreateDto> _createProjectValidator;
        private readonly IValidator<ProjectByIdDto> _updateProjectValidator;

        public ProjectController(IMediator mediator, IValidator<ProjectToCreateDto> createProjectValidator, IValidator<ProjectByIdDto> updateProjectValidator)
        {
            _mediator = mediator;
            _createProjectValidator = createProjectValidator;
            _updateProjectValidator = updateProjectValidator;
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
                IsDeleted = "activated",
                maxDescriptionCh = 50
            });

            ViewBag.CurrentPage = 1;
            ViewBag.MaxPage = response.MaxPage;

            return View(response);
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
                UserID = GetUserId(),
                maxDescriptionCh = 50
            });

            ViewBag.CurrentPage = page;
            ViewBag.MaxPage = response.MaxPage;

            return PartialView("_ProjectCards", response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProjectToCreateDto projectDto)
        {
            var validatorResponse = _createProjectValidator.Validate(projectDto);

            if (!validatorResponse.IsValid)
            {
                var errors = validatorResponse.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json (new {success = false, errors, message = "The data isn't valid"});
            }

            try
            {
                string responseCreate = await _mediator.Send(new CreateProjectCommand
                {
                    Project = projectDto,
                    UserID = GetUserId()
                });

                if (responseCreate == "success")
                {
                    return Json(new { success = true, message = "Project was successfully created." });
                }
                else
                {
                    return Json(new { success = false, message = responseCreate });
                }

            }
            catch
            {
                return Json(new { success = false, message = "A problem on the server occured. Try again!" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProjectByIdDto projectDto)
        {
            var validatorResponse = _updateProjectValidator.Validate(projectDto);

            if (!validatorResponse.IsValid)
            {
                var errors = validatorResponse.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, errors, message = "The data isn't valid" });
            }

            try
            {
                string responseCreate = await _mediator.Send(new UpdateProjectCommand
                {
                    Project = projectDto,
                    UserID = GetUserId()
                });

                if (responseCreate == "success")
                    return Json(new { success = true, message = "Project was successfully updated." });
                else
                    return Json(new { success = false, message = responseCreate });

            }
            catch
            {
                return Json(new { success = false, message = "A problem occured on the server. Try again!" });
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
                {
                    return Json(new { StatusCode = 204, message = "Project was successfully updated." });
                }
                else
                    return Json(new { StatusCode = 500, message = "A problem occured on the server. Try again!" });
            }
            catch
            {
                return Json(new { StatusCode = 500, message = "A problem occured on the server. Try again!" });
            }
        }

        // Modals : This methods open modals

        public async Task<ActionResult> OpenCreateModal()
        {

            var responseProjectStates = await _mediator.Send(new GetProjectStatesQuerry { });

            ViewBag.ProjectStates = new SelectList(responseProjectStates, "Id", "Name");

            return PartialView("_CreateProjectModal");
        }

        [HttpGet]
        public async Task<ActionResult> OpenCreateModalWithValidation(ProjectToCreateDto projectDto)
        {

            var validatorResponse = _createProjectValidator.Validate(projectDto);

            if (!validatorResponse.IsValid)
            {
                foreach (var error in validatorResponse.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

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
    }
}