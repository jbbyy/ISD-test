using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CustomerMemory
    {
        public List<CustomerDto> Customers { get; set; } = new();
    }
}
