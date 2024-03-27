using Microsoft.AspNetCore.Mvc;
using SchoolManagementAPI.DTOs;
using SchoolManagementAPI.Repositories.Interfaces;

namespace SchoolManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            this.staffRepository = staffRepository;
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest();
            }
            var results = await staffRepository.SearchAsync(searchTerm);
            return Ok(results);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GeatAllStaff()
        {
            var staffList = await staffRepository.GetAllStaffAsync();
            return Ok(staffList);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult> GetStaffById(int Id)
        {
            try
            {
                var result = await staffRepository.GetStaffByIDAsync(Id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Staff with ID {Id} not found!");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateNewStaff(StaffDto staff)
        {
            try
            {
                if (staff == null)
                {
                    return BadRequest();
                }

                var existingStaff = await staffRepository.GetStaffByEmailAsync(staff.Email);
                if (existingStaff != null)
                {
                    ModelState.AddModelError("Email", $"Staff with name {existingStaff.Email} is already in use");
                    return BadRequest(ModelState);
                }

                var newStaff = await staffRepository.AddNewStaffAsync(staff);
                return CreatedAtAction(nameof(GetStaffById), new { id    = newStaff.Id }, newStaff);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. {ex.Message}");
            }
        }
    }
}
