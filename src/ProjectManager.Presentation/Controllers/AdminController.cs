using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.User.Commands.UpdateUser;
using ProjectManager.Application.User.Queries;
using System;
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
        //private readonly IValidator<UserDto> _validator;

        #endregion

        #region Constructor

        public AdminController(IMediator mediator 
            //IValidator<UserDto> validator
            )
        {
            _mediator = mediator;
           // _validator = validator;
        }

        #endregion

        #region CRUD Operations

        #region Update User

        public async Task<ActionResult> Index()
        {
            try
            {
                var users = await _mediator.Send(new GetUsersQuery());
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
           // var validationResult = _validator.Validate(model);
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

            //var errorList = validationResult.Errors.Select(e=>new
            //{
            //    message = e.ErrorMessage,
            //    propertyName = e.PropertyName,
            //}).ToList();

            return new JsonResult
            {
                Data = new
                {
                    success = false,
                  //  errors = errorList
                }
            };
        }

        #endregion
        #endregion
    }
}