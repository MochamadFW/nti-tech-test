using Microsoft.AspNetCore.Mvc;
using NTI_Technical_Test.Models;
using NTI_Technical_Test.Services;

namespace NTI_Technical_Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            try
            {
                var students = await _studentService.GetAllAsync();
                if (students == null || !students.Any())
                {
                    return Ok(new
                    {
                        status = false,
                        message = "No students found",
                        data = new List<Student>()
                    });
                }

                return Ok(new
                {
                    status = true,
                    message = "Successfully retrieved student data",
                    data = students
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [HttpGet("detail/id/{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var checkStudent = await _studentService.GetByIdAsync(id);

                if (checkStudent == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Student not found!"
                    });
                }

                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched student data by ID",
                    data = checkStudent
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [HttpGet("detail/nisn/{nisn}")]
        public async Task<ActionResult<Student>> GetStudentByNisn(string nisn)
        {
            try
            {
                var checkNisn = await _studentService.GetByNisnAsync(nisn);

                if (checkNisn == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "NISN not found!"
                    });
                }

                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched student data by NISN",
                    data = checkNisn
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStudent([FromBody] Student student)
        {
            try
            {
                var checkStudent = await _studentService.GetByNisnAsync(student.Nisn);
                if (checkStudent != null)
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "A student with the same NISN already exists/registered!"
                    });
                }
                await _studentService.CreateAsync(student);
                return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, new
                {
                    status = true,
                    message = "Successfully created new student!",
                    data = student
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            try
            {
                // Check Student
                var checkStudent = await _studentService.GetByIdAsync(id);
                if (checkStudent != null)
                {
                    if (checkStudent.Nisn != student.Nisn)
                    {
                        // NISN Check
                        var checkNisn = await _studentService.GetByNisnAsync(student.Nisn);
                        if (checkNisn != null)
                        {
                            return BadRequest(new
                            {
                                status = false,
                                message = "A student with the same NISN already exists/registered!"
                            });
                        }
                    }
                }
                student.Id = id;

                await _studentService.UpdateAsync(student);
                return Ok(new
                {
                    status = true,
                    message = "Successfully updated student data!",
                    data = student
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);

                return Ok(new
                {
                    status = true,
                    message = "Successfully deleted student data"
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"ArgumentException: {ex}");
                return BadRequest(new
                {
                    status = false,
                    message = ex
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = false,
                    message = "An unexpected error occurred." + ex
                });
            }
        }
    }
}
