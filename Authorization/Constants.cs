using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketReservationSystem.Authorization
{
    public static class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";

        public static readonly string User = "User";
        public static readonly string Cashier = "Cashier";
        public static readonly string Bookkeeper = "Bookkeeper";
        public static readonly string Administrator = "Administrator";
    }

    public static class ManagementOperations
    {
        public static IAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static IAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static IAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static IAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
    }
}
