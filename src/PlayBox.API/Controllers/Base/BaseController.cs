using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayBox.Application.Common.Models;

namespace PlayBox.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(ServiceResponse<T> response)
        {
            if (response == null)
                return BadRequest(new ServiceResponse<T>(false, "Bir hata oluştu"));

            if (!response.Success)
                return BadRequest(response);

            if (response.Data == null)
                return NoContent();

            return Ok(response);
        }

        protected IActionResult CreateResponse(object data = null, string message = null)
        {
            if (data == null && string.IsNullOrEmpty(message))
                return NoContent();

            var response = new ServiceResponse<object>(true, message, data);
            return Ok(response);
        }
    }
}
