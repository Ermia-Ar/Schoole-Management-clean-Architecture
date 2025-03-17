using Core.Application.Commands;
using Core.Application.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.NET.ViewModels.AccountVM;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.NET.Controllers
{
    public class AccountController : Controller
    {
        private IValidator<SignUpRequest> _SignInValidator { get; set; }
        private IMediator _mediator;

        public AccountController(IMediator mediator, IValidator<SignUpRequest> signInValidator)
        {
            this._mediator = mediator;
            _SignInValidator = signInValidator;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            ViewData["returnUrl"] = "ttt";

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            var SignUpRequest = new SignUpRequest
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                ConfirmEmail = false,
            };
            var validationResult = _SignInValidator.Validate(SignUpRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View("SignUp", model);
            }


            var requst = new SignUpAsyncCommand
            {
                SignUpRequest = SignUpRequest
            };
            var result = await _mediator.Send(requst);

            if (result.Succeeded)
            {
                return Redirect(Url.Content("~/"));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty , item.Description);
                }
                return View(model);
            }
            
        }
    }
}
