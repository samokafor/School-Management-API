using Microsoft.AspNetCore.Mvc;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;
using SchoolManagementAPI.Repositories;
using SchoolManagementAPI.Repositories.Interfaces;

namespace SchoolManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Department>>> Search(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return BadRequest();
                }

                var result = await departmentRepository.SearchAsync(searchTerm);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. { ex.Message}");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllDepartments()
        {
            try 
            {
                var departments = await departmentRepository.GetAllDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. {ex.Message}");
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult> GetDepartmentById(int Id)
        {
            try
            {
                var result = await departmentRepository.GetDepartmentByIdAsync(Id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Department with ID {Id} not found!");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> CreateNewDepartment(DepartmentDto departmentDto)
        {
            try
            {
                if(departmentDto == null)
                {
                    return BadRequest();
                }

                var existingDepartment = await departmentRepository.GetDepartmentByNameAsync(departmentDto.Name);
                if (existingDepartment != null)
                {
                    ModelState.AddModelError("Name", $"Department with name {existingDepartment.Name} is already in use");
                    return BadRequest(ModelState);
                }

                var newDepartment = await departmentRepository.AddNewDepartmentAsync(departmentDto);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.Id }, newDepartment);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. {ex.Message}");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> UpdateDepartment(int Id, DepartmentDto department)
        {
            try
            {
                if (Id != department.Id)
                {
                    ModelState.AddModelError("Id", "Department Id mismatch!");
                    return BadRequest(ModelState);
                }

                var result = await departmentRepository.GetDepartmentByIdAsync(Id);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Department with ID {Id} not found!");
                }
                await departmentRepository.UpdateDepartment(department);
                return Ok(department);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating Department information.");
            }
        }
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteDepartment(int Id)
        {
            try
            {
                var result = await departmentRepository.GetDepartmentByIdAsync(Id);
                if (result == null)
                {
                    ModelState.AddModelError("Id", $"No department exists with the ID {Id}");
                    return BadRequest(ModelState);
                }
                var name = result.Name;
                await departmentRepository.DeleteDepartmentAsync(Id);
                return Ok($"The {name} Department has been deleted successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting faculty record. {ex.Message}");
            }
        }
    }
}
