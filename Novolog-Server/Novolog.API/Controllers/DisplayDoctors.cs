using Microsoft.AspNetCore.Mvc;
using Novolog.Application.Dtos;
using Novolog.Application.Services.DisplayDoctors;

namespace Novolog.API.Controllers
{
    [ApiController]
    [Route("DisplayDoctors")]
    public class DisplayDoctors : ControllerBase
    {
        private readonly ILogger<DisplayDoctors> _logger;
        private readonly IDisplayDoctorsService _displayDoctorsService;
        public DisplayDoctors(ILogger<DisplayDoctors> logger, IDisplayDoctorsService displayDoctorsService)
        {
            _logger = logger;
            _displayDoctorsService = displayDoctorsService;
        }
        [HttpGet("GetDoctors")]
        public IActionResult GetDoctors()
        {
            _logger.LogInformation("GetDoctors");
            return Ok(_displayDoctorsService.GetDoctorsList());
        }
        [HttpPost("ContactUs")]
        public IActionResult ContactUs(ContactUsDto contactUsDto)
        {
            _logger.LogInformation("ContactUs Controller");
            _displayDoctorsService.SaveContactData(contactUsDto);
            return Ok();
        }
    }
}
