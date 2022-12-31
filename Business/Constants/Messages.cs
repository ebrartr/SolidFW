using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added";
        public static string ProductNameInvalid = "Product name invalid";
        public static string ProductListed = "Product listed";
        public static string MaintenanceTime = "Maintenance time";
        public static string ProductUpdated = "Product updated";
        internal static string ProductNameAlreadyExists = "prodect name alrady exists";
        internal static string ProductCountOfCategoryError = "Max ... product can be add for one category";
    }
}
