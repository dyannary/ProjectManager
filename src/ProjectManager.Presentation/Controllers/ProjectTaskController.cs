using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.Extensionms;
using ProjectManager.Application.Projects.Queries;
using ProjectManager.Application.ProjectTasks.Queries;
using ProjectManager.Application.ProjectTasks.Queries.GetTasksByFilterQuery.cs;
using ProjectManager.Application.TableParameters;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProjectManager.Application.ProjectTasks.Commands;
using ProjectManager.Application.ProjectTasks.Commands.UpdateTask;
using ProjectManager.Application.ProjectTasks.Commands.DeleteTask;
using System.Net;
using ProjectManager.Application.Notifications.Commands.Send;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using FluentValidation;

namespace ProjectManager.Presentation.Controllers
{
    public class ProjectTaskController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IValidator<AddTaskDto> _addTaskValidator;

        public ProjectTaskController(IMediator mediator,
            IValidator<AddTaskDto> addTaskValidator)
        {
            _mediator = mediator;
            _addTaskValidator = addTaskValidator;
        }

        #region CRUD Operations
        public async Task<ActionResult> Index(int? Id)
        {

            var responseProjectList = await _mediator.Send(new GetProjectsForDropDownQuerry() { UserID = GetUserId() });

            if (!responseProjectList.Any())
            {
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

            var projectTaskList = new ProjectTaskList
            {
                ProjectId = ProjectId
            };

            var model = new ProjectTaskDto
            {
                Project = responseProject,
                ProjectsList = responseProjectList.ToList(),
                ProjectTaskList = projectTaskList
            };

            return View(model);
        }

        public async Task<ActionResult> GetTaskDetails(int id)
        {
            var response = await _mediator.Send(new GetTaskByIdQuery
            {
                Id = id
            });

            return PartialView("_GetProjectTaskDetailsModal", response);
        }

        [HttpPost]
        public async Task<ActionResult> TasksTable(int projectId, DataTableParameters parameters, CancellationToken cancellationToken)
        {
            var tasks = await _mediator.Send(new GetTasksByFilterQuery(projectId, parameters), cancellationToken);

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
            var addTaskDto = new AddTaskDto
            {
                ProjectId = id
            };

            var resposeTaskPriorities = await _mediator.Send(new GetTaskPriorityQuery { });

            var responseTaskType = await _mediator.Send(new GetTaskTypeQuery() { });

            var responseTaskState = await _mediator.Send(new GetTaskStateQuery() { });

            var responseUsers = await _mediator.Send(new GetProjectUsersQuery() { Id = id });


            ViewBag.Priorities = new SelectList(resposeTaskPriorities, "Id", "Name");

            ViewBag.TaskType = new SelectList(responseTaskType, "Id", "Name");

            ViewBag.TaskState = new SelectList(responseTaskState, "Id", "Name");

            ViewBag.UsersInProject = new SelectList(responseUsers, "Id", "Name");

            return PartialView("_AddProjectTaskModal", addTaskDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask(AddTaskDto data)
        {
            var validationResult = _addTaskValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, errors });
            }

            try
            {
                var addedUser = await _mediator.Send(new CreateTaskCommand { Data = data });
                if (addedUser)
                {

                    // Send Notification
                    await _mediator.Send(new SendNotificationCommand
                    {
                        ForUser_Username = data.AssignedTo.ToString(),
                        ProjectId = data.ProjectId,
                        Message = "You are assigned to a new task for project: ",
                        NotificationType = Application.Enums.NotificationTypeEnum.Task
                    });

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

        #region Update Task

        [HttpGet]
        public async Task<ActionResult> UpdateTask(int id)
        {
            var response = await _mediator.Send(new GetTaskByIdQuery
            {
                Id = id
            });

            var resposeTaskPriorities = await _mediator.Send(new GetTaskPriorityQuery { });

            var responseTaskType = await _mediator.Send(new GetTaskTypeQuery() { });

            var responseTaskState = await _mediator.Send(new GetTaskStateQuery() { });


            ViewBag.Priorities = new SelectList(resposeTaskPriorities, "Id", "Name");

            ViewBag.TaskType = new SelectList(responseTaskType, "Id", "Name");

            ViewBag.TaskState = new SelectList(responseTaskState, "Id", "Name");

            return PartialView("_UpdateProjectTaskModal", response);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTask(UpdateTaskDto data)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { StatusCode = 500 });
            }
            try
            {
                var addedUser = await _mediator.Send(new UpdateTaskCommand { Data = data });
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

        #region Delete Task

        [HttpGet]
        public ActionResult DeleteTask(int id)
        {
            return PartialView("_DeleteTaskModal", id);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTasks(int id)
        {
            var taskToDelete = await _mediator.Send(new DeleteTaskCommand { TaskId = id });

            if (taskToDelete)
                return Json(new { StatusCode = 201 });
            else
                return Json(new { StatusCode = 500 });
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

            if (viewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return PartialView("_CardInfoPartial", viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> RefreshTable(int id)
        {
            
            var tasks = await _mediator.Send(new GetTasksByProjectId
            {
                Id = id
            });

            if (tasks is null)
                return Json(new { StatusCode = 500 });

            var model = new ProjectTaskList
            {
                ProjectId = id
            };

            return PartialView("_ProjectTaskTable", model);
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
#endregion