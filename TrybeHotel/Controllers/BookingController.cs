using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]
  
    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert){
            var token = HttpContext.User.Identity as ClaimsIdentity;
            var email = token?.Claims.First(e => e.Type == ClaimTypes.Email)?.Value;
            try {
                if (email != null) {
                    return Created("", _repository.Add(bookingInsert, email));
                } else {
                    return BadRequest(new { message = "Email not found in claims" });
                }
            } catch {
                return BadRequest(new { message = "Guest quantity over room capacity" });
            }
        }


        [HttpGet("{Bookingid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid){
            var token = HttpContext.User.Identity as ClaimsIdentity;
            var email = token?.Claims.First(e => e.Type == ClaimTypes.Email)?.Value;
            try {
                if (email != null) {
                    var result = _repository.GetBooking(Bookingid, email);
                    return Ok(result);
                } else {
                    return Unauthorized();
                }
            } catch {
                return Unauthorized();
            }
        }
    }
}