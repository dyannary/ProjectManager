﻿using FluentValidation;
using MediatR;
using Microsoft.Ajax.Utilities;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.ProjectCollaborator.Commands;
using ProjectManager.Application.ProjectCollaborator.Queries;
using ProjectManager.Application.Projects.Commands.Create;
using ProjectManager.Application.Projects.Commands.Update;
using ProjectManager.Application.Projects.Queries;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    [Authorize(Roles = "user")]
    public class ProjectCollaboratorsController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IValidator<CollaboratorToCreateDto> _collaboratorToCreateValidaotr;

        public ProjectCollaboratorsController(IMediator mediator, IValidator<CollaboratorToCreateDto> collaboratorToCreateValidaotr)
        {
            _mediator = mediator;
            _collaboratorToCreateValidaotr = collaboratorToCreateValidaotr;
        }

        public int GetUserId()
        {
            int id = ClaimsExtensions.GetUserId(User);
            return id;
        }

        public async Task<ActionResult> Index(int? projectId)
        {
            int? loggedUserID = GetUserId();

            if (projectId == null || loggedUserID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var response = await _mediator.Send(new GetProjectCollaboratorsQuerry 
            {
                ProjectId = projectId.Value,
                Page = 1, PageSize = 10,
                LoggedUserId = loggedUserID.Value
            });

            if (response == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (response.LoggedUserRole == "User")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetByFilters(int projectId, string search, int page)
        {
            var response = await _mediator.Send(new GetProjectCollaboratorsQuerry
            {
                ProjectId = projectId,
                Search = search,
                Page = page,
                PageSize = 10,
                LoggedUserId = GetUserId()
            });

            if (response == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (response.LoggedUserRole == "User")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return PartialView("_CollaboratorsTable", response);
        }

        [HttpGet]
        public async Task<JsonResult> GetUserRecomandation(string collaboratorUserName, int projectId)
        {
            // TODO: Textbox from view need autocomplete
            var response = await _mediator.Send(new GetCollaboratorRecomandationQuerry
            {
                ProjectId= projectId,
                Search = collaboratorUserName
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddCollaborator(CollaboratorToCreateDto collaboratorToCreateDto)
        {
            
            var validatorResponse = _collaboratorToCreateValidaotr.Validate(collaboratorToCreateDto);
            var projectId = collaboratorToCreateDto.ProjectId;
            if (!validatorResponse.IsValid)
            {
                var errors = validatorResponse.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, projectId, errors });
            }

            try
            {
                var response = await _mediator.Send(new CreateProjectCollaboratorCommand
                {
                    collaboratorToCreateDto = collaboratorToCreateDto,
                });

                if (response)
                    return Json(new { success = true, projectId });
                else
                    return Json(new { success = false, projectId });

            }
            catch
            {
                return Json(new { success = false, projectId });
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditCollaborator(string collaboratorUserName, int collaboratorProjectRole, int projectId)
        {
            try
            {
            var response = await _mediator.Send(new UpdateProjectCollaboratorCommand 
            { 
                UserName = collaboratorUserName,
                ProjectId = projectId,
                Role = collaboratorProjectRole
            });

                if (response)
                    return Json(new { success = true, projectId });
                else
                    return Json(new { success = false, projectId });

            }
            catch
            {
                return Json(new { success = false, projectId });
            }
        }

        [HttpPost]
        public async Task<ActionResult> RemoveCollaborator(string collaboratorUserName, int projectId)
        {
            try
            {
                var response = await _mediator.Send(new RemoveProjectCollaboratorCommand
                {
                    CollaboratorUserName = collaboratorUserName,
                    ProjectId = projectId
                });

                if (response)
                    return Json(new { success = true, projectId });
                else
                    return Json(new { success = false, projectId });

            }
            catch
            {
                return Json(new { success = false, projectId });
            }
        }

        [HttpPost]
        public async Task<ActionResult> TransferProject(string collaboratorUserName, int projectId)
        {
            var response = await _mediator.Send(new TransferProjectToCollaboratorCommand
            {
                CollaboratorUserName = collaboratorUserName,
                OwnerId = GetUserId(),
                ProjectId = projectId
            });
            return View();
        }


        #region Modals
        public async Task<ActionResult> OpenAddModal(int id)
        {
            var projectUserRoles = await _mediator.Send(new GetProjectUserRolesQuerry { });

            ViewBag.ProjectUserRoles = new SelectList(projectUserRoles, "Id", "Name");
            ViewBag.ProjectId = id;
            return PartialView("_AddCollaboratorModal");
        }

        public async Task<ActionResult> OpenEditModal(int id, string collaboratorUserName)
        {
            var projectUserRoles = await _mediator.Send(new GetProjectUserRolesQuerry { });

            var model = new CollaboratorModalDataDto
            {
                ProjectId = id,
                CollaboratorUserName = collaboratorUserName,
            };

            ViewBag.ProjectUserRoles = new SelectList(projectUserRoles, "Id", "Name");
            return PartialView("_EditCollaboratorModal", model);
        }

        public ActionResult OpenRemoveModal(int id, string collaboratorUserName)
        {
            var model = new CollaboratorModalDataDto
            {
                ProjectId = id,
                CollaboratorUserName = collaboratorUserName,
            };
            return PartialView("_RemoveCollaboratorModal", model);
        }

        public ActionResult OpenTransferProjectModal(int id, string collaboratorUserName)
        {
            var model = new CollaboratorModalDataDto
            {
                ProjectId = id,
                CollaboratorUserName = collaboratorUserName,
            };
            return PartialView("_TransferProjectOwnershipModal", model);
        }

        #endregion
    }
}