using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.Extensions;
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
using FluentValidation;

namespace ProjectManager.Presentation.Controllers
{
    public class ProjectTaskController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IValidator<AddTaskDto> _addTaskValidator;
        private readonly IValidator<UpdateTaskDto> _updateTaskValidator;

        public ProjectTaskController(IMediator mediator,
            IValidator<AddTaskDto> addTaskValidator,
            IValidator<UpdateTaskDto> updateTaskValidator)
        {
            _mediator = mediator;
            _addTaskValidator = addTaskValidator;
            _updateTaskValidator = updateTaskValidator;

        }

        #region CRUD Operations
        public async Task<ActionResult> Index(int? projectId)
        {

            var responseProjectList = await _mediator.Send(new GetProjectsForDropDownQuerry() { UserID = GetUserId() });

            if (!responseProjectList.Any())
            {
                return HttpNotFound();
            }

            int _projectId = responseProjectList.FirstOrDefault().Id;

            if (projectId != null)
                _projectId = projectId.Value;

            var responseProject = await _mediator.Send(new GetProjectByIdForProjectTaskQuerry
            {
                Id = _projectId,
                LoggedUserId = GetUserId()
            });

            var projectTaskList = new ProjectTaskList
            {
                ProjectId = _projectId
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

            return PartialView("GetTaskDetailsView", response);
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

                    await _mediator.Send(new SendNotificationCommand
                    {
                        ForUser_Id = data.AssignedTo,
                        ProjectId = data.ProjectId,
                        Message = "You are assigned to a new task for project: ",
                        NotificationType = Application.Enums.NotificationTypeEnum.Task
                    });

                    return Json(new { StatusCode = 201, message = "Task was successfully created." });
                }
                else
                {
                    return Json(new { StatusCode = 500, message = "A problem on the server occured. Try again" });
                }
            }
            catch
            {
                return Json(new { StatusCode = 500, message = "A problem on the server occured. Try again" });
            }
        }

        #endregion

        #region Update Task

        public async Task<ActionResult> GotoProjectTaskDetails(int id)
        {
            var response = await _mediator.Send(new GetTaskByIdQuery
            {
                Id = id
            });

            return View("_GetProjectTaskDetailsModal", response);
        }


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

            var responseUsers = await _mediator.Send(new GetProjectByTaskIdQuery() { Id = id });


            ViewBag.Priorities = new SelectList(resposeTaskPriorities, "Id", "Name");

            ViewBag.TaskType = new SelectList(responseTaskType, "Id", "Name");

            ViewBag.TaskState = new SelectList(responseTaskState, "Id", "Name");

            ViewBag.UsersInProject = new SelectList(responseUsers, "Id", "Name");

            return PartialView("_UpdateProjectTaskModal", response);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTask(UpdateTaskDto data)
        {
            var validationResult = _updateTaskValidator.Validate(data);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, errors });
            }

            try
            {
                var addedUser = await _mediator.Send(new UpdateTaskCommand { Data = data });
                if (addedUser)
                {

                    await _mediator.Send(new SendNotificationCommand
                    {
                        ForUser_Username = data.AssignedTo,
                        ProjectId = data.ProjectId,
                        Message = "You are assigned to a new task for project: ",
                        NotificationType = Application.Enums.NotificationTypeEnum.Task
                    });

                    return Json(new { StatusCode = 201, message = "Task was successfully created." });
                }
                else
                {
                    return Json(new { StatusCode = 500, message = "Task was not modified." });
                    }
            }
            catch
            {
                return Json(new { StatusCode = 500, message = "A problem on the server occured. Try again" });
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
                return Json(new { StatusCode = 201, message = "Task was deleted succesfuly!" });
            else
                return Json(new { StatusCode = 500, message = "A problem on the server occured. Try Again" });
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