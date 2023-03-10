

namespace BlazorApp.Models.Helpers
{
    public static class ServiceConstant
    {
        public static string ListSection = "List";
        public static string AddSection = "Add";
        public static string EditSection = "Edit";
        public static string DeleteSection = "Delete";

        public static string GetEmployee = "/api/Employee/GetList";
        public static string CreateEmployee = "/api/Employee/Create";
        public static string UpdateEmployee = "/api/Employee/Update";
        public static string DeleteEmployee = "/api/Employee/Delete?id=";
        public static string SearchEmployee = "/api/Employee/Search/";
    }
}
