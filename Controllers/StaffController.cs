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

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<StaffDto>> UpdateStaff(int Id, StaffDto staff)
        {
            try
            {
                if (Id != staff.Id)
                {
                    ModelState.AddModelError("Id", "Staff Id mismatch"); return BadRequest(ModelState);
                }
                var staffToUpdate = await staffRepository.GetStaffByIDAsync(Id);

                if (staff == null) return StatusCode(StatusCodes.Status302Found, $"Staff with ID {Id} not found");
                await staffRepository.UpdateStaffAsync(staff);
                var updatedStaff = await staffRepository.GetStaffByIDAsync(Id);
                return Ok(updatedStaff);
            }
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error Updating informationn in the database. {ex.Message}");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteStaff(int Id)
        {
            try
            {
                var staffToDelete = await staffRepository.GetStaffByIDAsync(Id);
                if (staffToDelete == null)
                {
                    ModelState.AddModelError("Id", $"No staff exists with ID {Id}");
                    return BadRequest(ModelState);
                }
                await staffRepository.DeleteStaffAsync(Id);

                return Ok($"{staffToDelete.FirstName} {staffToDelete.LastName}'s profile has been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error Deleting data from database. {ex.Message}");
            }

        }
    }
}
