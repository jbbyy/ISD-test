using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CustomerDto
    {
        [Key]
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }

    }

    public class CustomerDtoResult
    {
        public  CustomerDto? Customer { get; set; }
        public string Message { get; set; }

    }



    public class CustomerResult
    {
        public bool IsSuccess { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class CustomerSubmission
    {
        //task 3: added validation to ensure first name and last name cannot be null during creation of customer
        [Required(ErrorMessage = "First name is mandatory")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last name is mandatory")]
        public string LastName { get; set; }
        public string Gender { get; set; }

    }
}
