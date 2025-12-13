using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase

    {
        private ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("customer-detail/{id}")]
        public async Task<ActionResult<CustomerDtoResult>> GetCustomerRecord(string id)
        {
            try
            {
                var result =  await _customerService.GetCustomerRecords(id);
                if (result.Customer is null)
                {
                    _logger.LogError(result.Message);
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }


        [HttpPost]
        [Route("Submit")]
        public async Task<ActionResult> SubmitCustomer ([FromBody] CustomerSubmission submitCustomerRecord)
        {
            try
            {
                var result = await _customerService.InsertCustomerResult(submitCustomerRecord);
                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            try
            {
                var result = await _customerService.DeleteCustomerResult(id);
                if (!result.IsSuccess)
                {
                    _logger.LogError(result.Message);
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }


    }
}
