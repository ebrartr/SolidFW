using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Authorisation danied";
        public static string ProductAdded = "Product Added";
        public static string ProductNameInvalid = "Product name invalid";
        public static string ProductListed = "Product listed";
        public static string MaintenanceTime = "Maintenance time";
        public static string ProductUpdated = "Product updated";
        public static string ProductNameAlreadyExists = "prodect name alrady exists";
        public static string ProductCountOfCategoryError = "Max ... product can be add for one category";
        public static string UserRegistered = "User registered";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin ="Sucessfull login";
        public static string UserAlreadyExists="User alreadyexists";
        public static string AccessTokenCreated="Access Token created";
    }
}