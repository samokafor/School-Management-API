using Microsoft.AspNetCore.Mvc;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.Repositories.Interfaces;

namespace SchoolManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : Controller
    {
        private readonly IFacultyRepository facultyRepository;
        public FacultyController(IFacultyRepository facultyRepository)
        {
            this.facultyRepository = facultyRepository;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FacultyController>>> Search(string name)
        {
            try
            {
                var result = await facultyRepository.Search(name);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetFaculties()
        {
            try
            {
                var result = await facultyRepository.GetFaculties();

                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetFacultyById(int Id)
        {
            try
            {
                var result = await facultyRepository.GetFacultyByID(Id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateFaculty(Faculty faculty)
        {
            try
            {
                if (faculty == null)
                {
                    return BadRequest();
                }
                var existingFaculty = await facultyRepository.GetFacultyByName(faculty.Name);
                if (existingFaculty != null)
                {
                    ModelState.AddModelError("Name", "Faculty with that name is already in use");
                    return BadRequest(ModelState);
                }
                var newFaculty = await facultyRepository.AddFaculty(faculty);
                return CreatedAtAction(nameof(GetFacultyById), new { id = newFaculty.Id }, newFaculty);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data from database.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateFaculty(int Id, Faculty faculty)
        {

        }


    }
}
