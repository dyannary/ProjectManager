using FluentValidation;
using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.TableParameters;
using ProjectManager.Application.User.Commands.CreateUser;
using ProjectManager.Application.User.Commands.UpdateUser;
using ProjectManager.Application.User.Queries;
using ProjectManager.Application.UserManagement.Commands.DeleteUser;
using ProjectManager.Application.UserManagement.Queries;
using ProjectManager.Application.UserManagement.Queries.GetUserPhoto;
using ProjectManager.Application.UserManagement.Queries.GetUsersRole;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
    //[Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        #region Private Fields

        private readonly IMediator _mediator;
        private readonly IValidator<AddUserDto> _userValidator;
        private readonly IValidator<UserDto> _updateUserValidator;

        #endregion

        #region Constructor

        public AdminController(IMediator mediator,
            IValidator<AddUserDto> userValidator,
            IValidator<UserDto> updateUserValidator)
        {
            _mediator = mediator;
            _userValidator = userValidator;
            _updateUserValidator = updateUserValidator;
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult> UserTable(DataTableParameters parameters, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetUsersByFilterQuery(parameters), cancellationToken);

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = users
            });
        }

        #region CRUD Operations

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _mediator.Send(new GetUsersQuery()
                {
               
                }, cancellationToken);

                return View(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via Mediatr" + ex);
            }
        }
        public async Task<ActionResult> GetUserDetails(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery 
            { 
                Id = id
            });

            return PartialView("_GetUserDetailsModal", response);
        }

        #region Add User
        public async Task<ActionResult> AddUser(int id)
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
        public async Task<ActionResult> AddUser(AddUserDto data)
        {
            var validationResult = _userValidator.Validate(data);


            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, errors });
            }
            try
            {
                var addedUser = await _mediator.Send(new AddUserCommand 
                { 
                    Data = data 
                });

                if (addedUser)
                {
                    return Json(new { StatusCode = 201, message = "User was successfully created." });
                }
                else
                {
                    return Json(new { StatusCode = 500, message = "A problem on the server occured. Try again" });
                }
            }
            catch
            {
                return Json(new { success = false, message = "A problem on the server occured. Try again!" });
            }
        }

        #endregion

        #region Update User
        public async Task<ActionResult> UpdateUser(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery
            {
                Id = id
            });

            var responseUsersRoles = await _mediator.Send(new GetUsersRoleQuery { });

            ViewBag.UserRoles = new SelectList(responseUsersRoles, "Id", "Name");

            return PartialView("_UpdateUserModal", response);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserDto data)
        {
            var validationResult = _updateUserValidator.Validate(data);


            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                                .GroupBy(x => x.PropertyName)
                                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, errors });
            }
            try
            {
                var addedUser = await _mediator.Send(new UpdateUserCommand { Data = data });
                if (addedUser)
                {
                    return Json(new { StatusCode = 201, message = "User was successfully updated." });
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

        #region Delete User

        public ActionResult DisableUserModal(int id)
        {
            return PartialView("_DisableUserModal", id);
        }

        [HttpPost]
        public async Task<ActionResult> DisableUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = id });
            return new JsonResult
            {
                Data = new
                {
                    success = result,
                }
            };
        }

        #endregion

        #region Modals

        public ActionResult OpenDisableUserModal(int id)
        {
            return PartialView("_DisableUserModal", id);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUserEnableStatus(int id)
        {
            try
            {
                var isDeleted = await _mediator.Send(new DeleteUserCommand
                {
                    UserId = id
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

        #endregion

        #endregion
    }
}