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
        private IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            var signInRequest = new SignInRequest
            {
                Password = model.Password,
                EmailOrUsername = model.EmailOrUserName,
                RememberMe = model.RememberMe,
            };

            //Sign In
            var requst = new SignInAsyncCommand
            {
                SignInRequest = signInRequest,
            };
            var result = await _mediator.Send(requst);

            if (result.ValidationResult != null)
            {
                result.ValidationResult.AddToModelState(ModelState);
                return View(model);
            }
            else if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "User name or password is not valid");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
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


            //add user
            var requst = new SignUpAsyncCommand
            {
                SignUpRequest = SignUpRequest
            };
            var result = await _mediator.Send(requst);

            //validate add user
            if (result.ValidationResult != null)
            {
                result.ValidationResult.AddToModelState(ModelState);
            }
            else if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item);
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

            // send email confirm
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
                ModelState.AddModelError(string.Empty, res.Errors[0]);

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