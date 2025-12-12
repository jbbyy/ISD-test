using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface ICustomerService
    {
        Task<CustomerDtoResult> GetCustomerRecords(string id);
        Task<CustomerResult> InsertCustomerResult(CustomerSubmission customer);
        Task<CustomerResult> DeleteCustomerResult(string id);

        
    }
}
