using Employee.Model;

namespace EmployeeAPI.EmployeeRepository
{
    public interface IAdditionalInfoRepository
    {
        Task<AdditionalInfoData> GetAdditionalInfoById(int id);
        Task<AdditionalInfo> GetInfoById(int id);
        Task<AdditionalInfo> InsertAdditionalInfo(AdditionalInfo additionalInfo);
        Task<AdditionalInfo> UpdateAdditionalInfo(AdditionalInfo additionalInfo);
        Task<bool> DeleteAdditionalInfo(int id);
    }
}
