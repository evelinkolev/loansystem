using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Services.Authorization
{
    /// <summary>
    /// Class to verifies the user acting on the resource is an agent. Only agents can approve or reject content changes.
    /// </summary>
    public class LoanAgentAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Loan>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Loan resource)
        {
            if(context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if(requirement.Name != Constants.ApproveOperationName &&
                requirement.Name != Constants.RejectOperationName)
            {
                return Task.CompletedTask;
            }

            // Agents can approve or reject.
            if(context.User.IsInRole(Constants.LoanAgentsRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
