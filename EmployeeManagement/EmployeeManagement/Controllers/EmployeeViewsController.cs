using Employee.Model;
using EmployeeManagement.Models;
using EmployeeManagement.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeManagement.Controllers
{
    public class EmployeeViewsController : Controller
    {
        static int paggingValue = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeViewsController()
        {

        }

        // GET: EmployeeController
        /// <summary>
        /// Get all employee's list using API call
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int? val=0)
        {
            paggingValue = val.Value;
            List<EmployeeData>? employeeList = new();
            try
            {
                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetAllEmp}{paggingValue}");
                string apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<EmployeeData>>(apiResponse);
                if (result != null && result.Count > 0)
                {
                    employeeList = result;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return View(employeeList);
        }

        /// <summary>
        /// Increament pagging value
        /// </summary>
        /// <returns></returns>
        public ActionResult IncrValue()
        {
            var inc = paggingValue + 4;
            return RedirectToAction(nameof(Index), new { val = inc });
        }

        /// <summary>
        /// Decreament pagging value
        /// </summary>
        /// <returns></returns>
        public ActionResult DcrValue()
        {
            int dcr = 0;
            if (paggingValue > 0)
                dcr = paggingValue - 4;
            else
                dcr = paggingValue;
                
            return RedirectToAction(nameof(Index), new { val = dcr });
        }

        // GET: EmployeeController/Details/5
        /// <summary>
        /// Get employee's details by id using API call while click on view
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int? employeeId)
        {
            EmployeeMater? employee = null;
            try
            {
                if (employeeId == null)
                {
                    return NotFound();
                }

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetEmpById}{employeeId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<EmployeeMater>(apiResponse);
                        if (result != null && result.Employee_Id > 0)
                        {
                            employee = result;
                        }
                    }
                }

                if (employee == null) { return NotFound(); }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return View(employee);
        }

        // GET: EmployeeController/Edit/5
        /// <summary>
        /// Get employee's details by id using API call while click on Add or Edit
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddOrEdit(int? employeeId)
        {
            EmployeeMater? employee = null;
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
                            ViewBag.DeptList = result;
                        }
                    }
                }

                ViewBag.PageName = employeeId == null ? "Create Employee" : "Edit Employee";
                ViewBag.IsEdit = employeeId != null;
                if (employeeId == null)
                {
                    return View();
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetEmpById}{employeeId}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<EmployeeMater>(apiResponse);
                            if (result != null && result.Employee_Id > 0)
                            {
                                employee = result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        /// <summary>
        /// Add new or Update existing employee using API call
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrEdit(int? employeeId, EmployeeMater employeeData)
        {
            bool IsEmployeeExist = false;
            EmployeeMater? employeeModel = new();
            try
            {
                if (employeeId != null)
                {
                    IsEmployeeExist = true;
                    employeeModel.Employee_Id = employeeId.Value;
                }

                if (ModelState.IsValid)
                {
                    employeeModel.Emp_Name = employeeData.Emp_Name;
                    employeeModel.Designation = employeeData.Designation;
                    employeeModel.Address = employeeData.Address;
                    employeeModel.Salary = employeeData.Salary;
                    employeeModel.Joining_Date = employeeData.Joining_Date;
                    employeeModel.Department_Id = employeeData.Department_Id;
                    employeeModel.IsActive = true;

                    if (IsEmployeeExist)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(employeeModel), Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PutAsync($"{Common.BaseUrl}{Common.UpdateEmp}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                employeeModel = JsonConvert.DeserializeObject<EmployeeMater>(apiResponse);
                            }
                        }
                    }
                    else
                    {
                        using (var httpClient = new HttpClient())
                        {
                            string json = JsonConvert.SerializeObject(employeeModel);
                            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PostAsync($"{Common.BaseUrl}{Common.AddEmp}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                employeeModel = JsonConvert.DeserializeObject<EmployeeMater>(apiResponse);
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return View(employeeModel);
        }

        // POST: EmployeeController/Delete/5
        /// <summary>
        /// Delete employee by id using API call
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int? employeeId)
        {
            try
            {
                if (employeeId == null) { return NotFound(); }
                using (var httpClient = new HttpClient())
                {
                    using (var resp = await httpClient.DeleteAsync($"{Common.BaseUrl}{Common.DeleteEmpById}{employeeId}"))
                    {
                        string apiResp = await resp.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<bool>(apiResp);
                        if (res)
                        {

                        }
                    }
                    //using (var response = await httpClient.DeleteAsync($"{Common.BaseUrl}{Common.DeleteAdditionalInfoById}{employeeId}"))
                    //{
                    //    string apiResponse = await response.Content.ReadAsStringAsync();
                    //    var result = JsonConvert.DeserializeObject<bool>(apiResponse);
                    //    if (result)
                    //    {
                    //        using (var resp = await httpClient.DeleteAsync($"{Common.BaseUrl}{Common.DeleteEmpById}{employeeId}"))
                    //        {
                    //            string apiResp = await resp.Content.ReadAsStringAsync();
                    //            var res = JsonConvert.DeserializeObject<bool>(apiResp);
                    //            if (res)
                    //            {

                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get employees' all details using API call
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<ActionResult> EmployeeDetails(int? employeeId)
        {
            AdditionalInfoData? additionalInfoData = null;
            try
            {
                ViewBag.IsInfo = false;
                if (employeeId == null)
                {
                    return View();
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetAdditionalInfoById}{employeeId}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<AdditionalInfoData>(apiResponse);
                            if (result != null && result.Employee_Id > 0)
                            {
                                additionalInfoData = result;
                                ViewBag.IsInfo = result.AI_Id <= 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return View(additionalInfoData);
        }

        /// <summary>
        /// Get addtional info by id using API call while click on view
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="aiId"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddOrEditInfo(int? employeeId,int? aiId)
        {
            AdditionalInfo? additionalInfo = new();
            try
            {
                ViewBag.Pagename = aiId > 0 ? "Edit Info" : "Add Info";
                ViewBag.IsEdit = aiId > 0;
                if (aiId == null)
                {
                    return View();
                }
                else
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"{Common.BaseUrl}{Common.GetInfoById}{aiId}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<AdditionalInfo>(apiResponse);
                            if (result != null && result.Employee_Id > 0)
                            {
                                additionalInfo = result;
                            }
                            else
                            {
                                additionalInfo.Birth_Date = DateTime.Now.AddYears(-20);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            additionalInfo.Employee_Id = employeeId.Value;
            return View(additionalInfo);
        }

        /// <summary>
        /// Add new or Update existing additional information using API call
        /// </summary>
        /// <param name="aiId"></param>
        /// <param name="additionalInfoData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOrEditInfo(int? aiId, AdditionalInfo additionalInfoData)
        {
            bool IsEmployeeExist = false;
            AdditionalInfo? additionalInfo = new();
            try
            {
                if (aiId > 0)
                {
                    IsEmployeeExist = true;
                    additionalInfo.AI_Id = aiId.Value;
                }

                if (ModelState.IsValid)
                {
                    additionalInfo.Mobile = additionalInfoData.Mobile;
                    additionalInfo.Email = additionalInfoData.Email;
                    additionalInfo.Birth_Date = additionalInfoData.Birth_Date;
                    additionalInfo.Gender = additionalInfoData.Gender;
                    additionalInfo.Marital_Status = additionalInfoData.Marital_Status;
                    additionalInfo.Qualification = additionalInfoData.Qualification;
                    additionalInfo.Experience = additionalInfoData.Experience;
                    additionalInfo.Employee_Id = additionalInfoData.Employee_Id;
                    additionalInfo.IsActive = true;

                    if (IsEmployeeExist)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(additionalInfo), Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PutAsync($"{Common.BaseUrl}{Common.UpdateAdditionalInfo}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                additionalInfo = JsonConvert.DeserializeObject<AdditionalInfo>(apiResponse);
                            }
                        }
                    }
                    else
                    {
                        using (var httpClient = new HttpClient())
                        {
                            string json = JsonConvert.SerializeObject(additionalInfo);
                            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PostAsync($"{Common.BaseUrl}{Common.AddAdditionalInfo}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                additionalInfo = JsonConvert.DeserializeObject<AdditionalInfo>(apiResponse);
                            }
                        }
                    }
                    return RedirectToAction("EmployeeDetails", "EmployeeViews", new
                    {
                        employeeId = additionalInfo.Employee_Id
                    });
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return View(additionalInfo);
        }
    }
}