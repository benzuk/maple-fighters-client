﻿using Authenticator.API.Datas;
using Authenticator.API.Validators;
using Authenticator.Domain.Aggregates.User.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Authenticator.Domain.Aggregates.User;
using Authenticator.API.Converters;
using Authenticator.API.Constants;

namespace Authenticator.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService loginService;
        private readonly IRegistrationService registrationService;
        private readonly RegistrationDataValidator registrationDataValidator;
        private readonly LoginDataValidator loginDataValidator;

        public AuthController(
            ILoginService loginService,
            IRegistrationService registrationService)
        {
            this.loginService = loginService;
            this.registrationService = registrationService;

            loginDataValidator = new LoginDataValidator();
            registrationDataValidator = new RegistrationDataValidator();
        }

        [HttpPost]
        [Route("/login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AccountData> Login(LoginData loginData)
        {
            ActionResult<AccountData> actionResult;

            var validationResult = loginDataValidator.Validate(loginData);
            if (validationResult.IsValid)
            {
                var email = loginData.Email;
                var password = loginData.Password;
                var account = loginService.FindAccount(email);
                if (account != null)
                {
                    var isVerified =
                        BCrypt.Net.BCrypt.Verify(password, account.Password);
                    if (isVerified)
                    {
                        actionResult = Ok(new AccountData()
                        {
                            Id = account.Id
                        });
                    }
                    else
                    {
                        actionResult = BadRequest(new ErrorData()
                        {
                            ErrorMessages = new string[] { ErrorMessages.WrongPassword }
                        });
                    }
                }
                else
                {
                    actionResult = NotFound(new ErrorData()
                    {
                        ErrorMessages = new string[] { ErrorMessages.AccountNotFound }
                    });
                }
            }
            else
            {
                actionResult = BadRequest(new ErrorData()
                {
                    ErrorMessages = validationResult.Errors.ConvertToErrorMessages()
                });
            }

            return actionResult;
        }

        [HttpPost]
        [Route("/register")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Register(RegistrationData registrationData)
        {
            ActionResult actionResult;

            var validationResult =
                registrationDataValidator.Validate(registrationData);
            if (validationResult.IsValid)
            {
                var email = registrationData.Email;
                var password = registrationData.Password;
                var firstName = registrationData.FirstName;
                var lastName = registrationData.LastName;

                if (registrationService.CheckIfEmailExists(email))
                {
                    actionResult = BadRequest(new ErrorData()
                    {
                        ErrorMessages = new string[] { ErrorMessages.EmailAlreadyExists }
                    });
                }
                else
                {
                    var hashedPassword =
                        BCrypt.Net.BCrypt.HashPassword(password);
                    var account =
                        Account.Create(email, hashedPassword, firstName, lastName);

                    registrationService.CreateAccount(account);

                    actionResult = Ok();
                }
            }
            else
            {
                actionResult = BadRequest(new ErrorData()
                {
                    ErrorMessages = validationResult.Errors.ConvertToErrorMessages()
                });
            }

            return actionResult;
        }
    }
}