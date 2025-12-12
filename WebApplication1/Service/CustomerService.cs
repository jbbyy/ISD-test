using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerMemory _customerMemory;

        public CustomerService(
            CustomerMemory customerMemory
            )
        {
            _customerMemory = customerMemory;
        }

        public Task<CustomerResult> DeleteCustomerResult(string id)
        {
            var message = "";
            bool isSuccess = true;
                var customer = _customerMemory.Customers.SingleOrDefault(c=> c.Id == id && c.IsActive);
            // check to ensure active customer exist before deletion
            if (customer == null)
            {
                message = $"Unable to find customer {id} for deletion";
                isSuccess = false;
            }
            else
            {
                //soft delete by setting IsActive to false
                customer.IsActive = false;
            }

            return Task.FromResult(new CustomerResult()
            {
                IsSuccess = isSuccess,
                Id = id,
                Message = message
            });

        }

        public Task<CustomerDtoResult> GetCustomerRecords(string id)
        {
            var message = "";
            var customer = _customerMemory.Customers.SingleOrDefault(c => c.Id == id && c.IsActive);
            if (customer == null)
            {
                message = $"Unable to find customer {id}";
            }

            return Task.FromResult(new CustomerDtoResult()
            {
                Customer = customer,
                Message = message
            });
        }

        public Task<CustomerResult> InsertCustomerResult(CustomerSubmission customer)

        {
            CustomerDto newCustomer = new CustomerDto()
            {
                Firstname = customer.Firstname,
                LastName = customer.LastName,
                Gender = customer.Gender,
                Id = Guid.NewGuid().ToString(),
                IsActive = true
            };
            try
            {
                _customerMemory.Customers.Add(newCustomer);
                return Task.FromResult(new CustomerResult()
                {
                    Id = newCustomer.Id,
                    IsSuccess = true,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CustomerResult()
                {
                    Id = "",
                    IsSuccess = false,
                    Message = ex.Message
                });
            }

        }
    }
}
