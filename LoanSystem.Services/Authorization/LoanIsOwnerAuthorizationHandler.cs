using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Services.Authorization
{
    /// <summary>
    /// Class to validate if the current authenticated user is the loan owner.
    /// Calls context.Succeed when the requirements are met.
    /// </summary>
    /// <returns>Returns Task.CompletedTask when requirements aren't met. 
    /// Returning Task.CompletedTask without a prior call to context.
    /// Success or context.Fail is not a success or failure, it allows other authorization handlers to run.</returns>
    public class LoanIsOwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Loan>
    {
        private readonly UserManager<User> _userManager;

        public LoanIsOwnerAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Loan resource)
        {
            if(context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.CreateOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.UserId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
