using Employee.Model;
using EmployeeManagement.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeManagement.Controllers
{
    public class DepartmentViewsController : Controller
    {
        // GET: DepartmentViewsController
        /// <summary>
        /// Get all department's list using API call
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> DepartmentIndex()
        {
            List<Department> departmentList = new();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetAllDept}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Department>>(apiResponse);
                        if (result != null && result.Count > 0)
                        {
                            departmentList = result;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return View(departmentList);
        }

        // GET: DepartmentViewsController/Details/5
        /// <summary>
        /// Get department using API call by department id when click on view detail
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task<ActionResult> DepartmentDetails(int? departmentId)
        {
            Department? department = null;
            try
            {
                ViewBag.Pagename = departmentId == null ? "Create Department" : "Edit Department";
                ViewBag.IsEdit = departmentId != null;
                if (departmentId == null)
                {
                    return View();
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetDeptById}{departmentId}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<Department>(apiResponse);
                            if (result != null && result.Department_Id > 0)
                            {
                                department = result;
                            }
                        }
                    }
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: DepartmentViewsController/Edit/5
        /// <summary>
        /// Get department using API call by department id when click on Add or Edit
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public async Task<ActionResult> DepartmentAddEdit(int? departmentId)
        {
            Department? department = null;
            try
            {
                ViewBag.Pagename = departmentId == null ? "Create Department" : "Edit Department";
                ViewBag.IsEdit = departmentId == null ? false : true;
                if (departmentId == null)
                {
                    return View();
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetDeptById}{departmentId}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<Department>(apiResponse);
                            if (result != null && result.Department_Id > 0)
                            {
                                department = result;
                            }
                        }
                    }
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: DepartmentViewsController/Edit/5
        /// <summary>
        /// Add new or Update existing department using API call
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="departmentData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DepartmentAddEdit(int? departmentId, Department departmentData)
        {
            bool IsDepartmentExist = false;
            Department? department = new();
            try
            {
                if (departmentId != null)
                {
                    IsDepartmentExist = true;
                    department.Department_Id = departmentId.Value;
                }

                if (ModelState.IsValid)
                {
                    department.Department_Name = departmentData.Department_Name;
                    department.IsActvie = true;

                    if (IsDepartmentExist)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PutAsync($"{Common.BaseUrl}{Common.UpdateDept}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                department = JsonConvert.DeserializeObject<Department>(apiResponse);
                            }
                        }
                    }
                    else
                    {
                        using (var httpClient = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PostAsync($"{Common.BaseUrl}{Common.AddDept}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                department = JsonConvert.DeserializeObject<Department>(apiResponse);
                            }
                        }
                    }
                    return RedirectToAction(nameof(DepartmentIndex));
                }
                return View(department);
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentViewsController/Delete/5
        /// <summary>
        /// Delete department by id using API call
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DepartmentDelete(int? departmentId)
        {
            if (departmentId == null) { return NotFound(); }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{Common.BaseUrl}{Common.DeleteDeptById}{departmentId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(apiResponse);
                    if (result)
                    {

                    }
                }
            }
            return RedirectToAction(nameof(DepartmentIndex));
        }
    }
}
