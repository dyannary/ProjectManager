using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.DTOs.User;
using ProjectManager.Application.TableParameters;
using ProjectManager.Application.User.Commands.UpdateUser;
using ProjectManager.Application.User.Queries;
using ProjectManager.Application.UserManagement.Queries;
using System;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectManager.Presentation.Controllers
{
   // [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        #region Private Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult> UserTable(DataTableParameters parameters)
        {
            var users = await _mediator.Send(new GetUsersByFilterQuery(parameters));

            return Json(new
            {
                draw = parameters.Draw,
                recordsFiltered = parameters.TotalCount,
                recordsTotal = parameters.TotalCount,
                data = users
            });
        }

        #region CRUD Operations

        #region Update User



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

        public ActionResult UpdateUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserDto model)
        {
            //var validationResult = _validator.Validate(model);
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateUserCommand { Data = model });

                return new JsonResult
                {
                    Data = new
                    {
                        success = true,
                    }
                };
            }

            //var errorList = validationResult.Errors.Select(e => new
            //{
            //    message = e.ErrorMessage,
            //    propertyName = e.PropertyName,
            //}).ToList();

            return new JsonResult
            {
                Data = new
                {
                    success = false,
                    //errors = errorList
                }
            };
        }

        #endregion
        #endregion
    }
}