using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketReservationSystem.Models;

namespace TicketReservationSystem.Authorization
{
    public class PurchaseIsBuyerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Purchases>
    {
        UserManager<IdentityUser> _userManager;

        public PurchaseIsBuyerAuthorizationHandler(UserManager<IdentityUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Purchases resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If we're not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.UserId == _userManager.GetUserId(context.User)) // TODO: and is not in role ...
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
