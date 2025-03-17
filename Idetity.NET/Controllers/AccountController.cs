using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.NET.ViewModels.AccountVM;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;

namespace Identity.NET.Controllers
{
    public class AccountController : Controller
    {
        private IValidator<SignUpRequest> _signUpValidator { get; set; }
        private IValidator<SignInRequest> _signInValidator { get; set; }
        private IMediator _mediator;

        public AccountController(IMediator mediator, IValidator<SignUpRequest> signUpValidator , IValidator<SignInRequest> signInValidator)
        {
            this._mediator = mediator;
            _signUpValidator = signUpValidator;
            _signInValidator = signInValidator;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            //Validate 
            var signInRequest = new SignInRequest
            {
                Password = model.Password,
                Email_UserName = model.Email_UserName,
                RememberMe = model.RememberMe,
            };
            var validationResult = _signInValidator.Validate(signInRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View("SignUp", model);
            }
            //Sign In
            var requst = new SignInAsyncCommand
            {
                SignInRequest = signInRequest,
            };
            var result = await _mediator.Send(requst);

            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "User name or password is not valid");
                return View(model);
            }

            return RedirectToAction("Index" ,"Home");
        }


        [HttpGet]
        public IActionResult SignUp(string returnUrl)
        {
            var model = new SignUpVM
            {
                IsConfirmEmailSend = false
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            //validate
            var SignUpRequest = new SignUpRequest
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                ConfirmEmail = false,
            };
            var validationResult = _signUpValidator.Validate(SignUpRequest);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return View("SignUp", model);
            }

            //add user
            var requst = new SignUpAsyncCommand
            {
                SignUpRequest = SignUpRequest
            };
            var result = await _mediator.Send(requst);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(model);
            }

            //generate token
            var requestToken = new GenerateEmailConfirmationAsyncCommand()
            {
                user = User
            };
            var genarateToken = await _mediator.Send(requestToken);
            string? confirmationLink = Url.Action(nameof(ConfirmEmail), "Acoount", new { userId = genarateToken.UserId, token = genarateToken.Token }, Request.Scheme);

            // send email
            var email = new EmailSenderCommand
            {
                EmailRequest = new EmailRequest
                {
                    Body = $"Ckeck on link below for Complete register " + Environment.NewLine + confirmationLink,
                    Subject = "Email Confirm",
                    To = model.Email
                }
            };
            var res = await _mediator.Send(email);

            if (res.Succeeded)
                model.IsConfirmEmailSend = true;
            else
                ModelState.AddModelError(string.Empty, res.Errors["1"]);

            return View(model);

        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) 
                return BadRequest("user not found");
            var request = new ConfirmEmailAsyncCommand()
            {
                emailConfirmationRequest = new EmailConfirmationRequest
                {
                    Token = token,
                    UserId = userId
                }
            };
            var result = await _mediator.Send(request);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
    }
}