using Dapper;
using Employee.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace EmployeeAPI.EmployeeRepository
{
    public class AdditionalInfoRepository : IAdditionalInfoRepository
    {
        private readonly IOptions<DataConfig> _connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public AdditionalInfoRepository(IOptions<DataConfig> connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Delete Additional Information By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdditionalInfo(int id)
        {
            bool isDelete = false;
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "D");
                    param.Add("@Id", id);
                    var result = await con.QueryFirstOrDefaultAsync<int>("SP_CRUD_AdditionalInfo", param, commandType: CommandType.StoredProcedure);
                    if(result > 0) { isDelete = true; }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return isDelete;
        }

        /// <summary>
        /// Get Employee's All Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdditionalInfoData> GetAdditionalInfoById(int id)
        {
            AdditionalInfoData? additionalInfoData = null;
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    param.Add("@Mode", "GA");
                    var data = await con.QueryFirstOrDefaultAsync<AdditionalInfoData>("SP_CRUD_AdditionalInfo", param, commandType: CommandType.StoredProcedure);
                    if (data != null && data.Employee_Id > 0)
                    {
                        additionalInfoData = data;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return additionalInfoData;
        }

        /// <summary>
        /// Get Additional Imformation By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdditionalInfo> GetInfoById(int id)
        {
            AdditionalInfo? additionalInfo = null;
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    param.Add("@Mode", "GBI");
                    var data = await con.QueryFirstOrDefaultAsync<AdditionalInfo>("SP_CRUD_AdditionalInfo", param, commandType: CommandType.StoredProcedure);
                    if (data != null && data.Employee_Id > 0)
                    {
                        additionalInfo = data;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return additionalInfo;
        }

        /// <summary>
        /// Add New Additional Information Of Employee
        /// </summary>
        /// <param name="additional_Info"></param>
        /// <returns></returns>
        public async Task<AdditionalInfo> InsertAdditionalInfo(AdditionalInfo additional_Info)
        {
            AdditionalInfo additionalInfo = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "I");
                    param.Add("@Mobile", additional_Info.Mobile);
                    param.Add("@Email", additional_Info.Email);
                    param.Add("@BirthDate", additional_Info.Birth_Date);
                    param.Add("@Gender", additional_Info.Gender);
                    param.Add("@MaritalStatus", additional_Info.Marital_Status);
                    param.Add("@Qualification", additional_Info.Qualification);
                    param.Add("@Experience", additional_Info.Experience);
                    param.Add("@EmployeeId", additional_Info.Employee_Id);
                    param.Add("@IsActive", additional_Info.IsActive);
                    additionalInfo = await con.QueryFirstOrDefaultAsync<AdditionalInfo>("SP_CRUD_AdditionalInfo", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return additionalInfo;
        }

        /// <summary>
        /// Update Additional Information Of Employee
        /// </summary>
        /// <param name="additional_Info"></param>
        /// <returns></returns>
        public async Task<AdditionalInfo> UpdateAdditionalInfo(AdditionalInfo additional_Info)
        {
            AdditionalInfo additionalInfo = new();
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString.Value.DefaultConnection))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var param = new DynamicParameters();
                    param.Add("@Mode", "U");
                    param.Add("@Id", additional_Info.AI_Id);
                    param.Add("@Mobile", additional_Info.Mobile);
                    param.Add("@Email", additional_Info.Email);
                    param.Add("@BirthDate", additional_Info.Birth_Date);
                    param.Add("@Gender", additional_Info.Gender);
                    param.Add("@MaritalStatus", additional_Info.Marital_Status);
                    param.Add("@Qualification", additional_Info.Qualification);
                    param.Add("@Experience", additional_Info.Experience);
                    param.Add("@EmployeeId", additional_Info.Employee_Id);
                    additionalInfo = await con.QueryFirstOrDefaultAsync<AdditionalInfo>("SP_CRUD_AdditionalInfo", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return additionalInfo;
        }
    }
}
