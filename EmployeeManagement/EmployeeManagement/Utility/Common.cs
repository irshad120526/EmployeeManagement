namespace EmployeeManagement.Utility
{
    public static class Common
    {
        /// <summary>
        /// Base URL for API
        /// </summary>
        public static string BaseUrl = "http://localhost:5098";

        /// <summary>
        /// Employee API calls with controllers and methods
        /// </summary>
        public static string GetAllEmp = "/api/Employee/GetAllEmployee?skip=";
        public static string GetEmpById = "/api/Employee/GetEmployeeById?Id=";
        public static string AddEmp = "/api/Employee/AddEmployee";
        public static string UpdateEmp = "/api/Employee/UpdateEmployee";
        public static string DeleteEmpById = "/api/Employee/DeleteEmployeebyId?Id=";

        /// <summary>
        /// Department API calls with controllers and methods
        /// </summary>
        public static string GetAllDept = "/api/Department/GetAllDepartment";
        public static string GetDeptById = "/api/Department/GetDepartmentById?Id=";
        public static string AddDept = "/api/Department/AddDepartment";
        public static string UpdateDept = "/api/Department/UpdateDepartment";
        public static string DeleteDeptById = "/api/Department/DeleteDepartmentById?Id=";

        /// <summary>
        /// AdditionalInfo API calls with controllers and methods
        /// </summary>
        public static string GetAdditionalInfoById = "/api/AdditionalInfo/GetAdditionalInfoById?Id=";
        public static string GetInfoById = "/api/AdditionalInfo/GetInfoById?Id=";
        public static string AddAdditionalInfo = "/api/AdditionalInfo/AddAdditionalInfo";
        public static string UpdateAdditionalInfo = "/api/AdditionalInfo/UpdateAdditionalInfo";
        public static string DeleteAdditionalInfoById = "/api/Department/DeleteAdditionalInfoById?Id=";
    }
}
