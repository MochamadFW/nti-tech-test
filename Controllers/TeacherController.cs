using Microsoft.AspNetCore.Mvc;
using NTI_Technical_Test.Models;
using NTI_Technical_Test.Services;

namespace NTI_Technical_Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teachers = await _teacherService.GetAllAsync();
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched list of teachers!",
                    data = teachers
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

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var checkTeacher = await _teacherService.GetByIdAsync(id);
                if (checkTeacher == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Teacher not found!"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched teacher details!",
                    data = checkTeacher
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

        [HttpGet("nip/{nip}")]
        public async Task<IActionResult> GetByNip(string nip)
        {
            try
            {
                var checkTeacher = await _teacherService.GetByNipAsync(nip);
                if (checkTeacher == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Teacher not found!"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched teacher details by NIP!",
                    data = checkTeacher
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

        [HttpGet("nuptk/{nuptk}")]
        public async Task<IActionResult> GetByNuptk(string nuptk)
        {
            try
            {
                var checkTeacher = await _teacherService.GetByNuptkAsync(nuptk);
                if (checkTeacher == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Teacher not found!"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched teacher details by NUPTK!",
                    data = checkTeacher
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
        public async Task<IActionResult> Create([FromBody] Teacher teacher)
        {
            try
            {
                await _teacherService.CreateAsync(teacher);
                return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, new
                {
                    status = true,
                    message = "Successfully created a new teacher!",
                    data = teacher
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
        public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
        {
            try
            {
                teacher.Id = id;
                await _teacherService.UpdateAsync(teacher);
                return Ok(new
                {
                    status = true,
                    message = "Successfully updated teacher data!",
                    data = teacher
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var teacher = await _teacherService.GetByIdAsync(id);
                if (teacher == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Teacher not found!"
                    });
                }

                await _teacherService.DeleteAsync(id);
                return Ok(new
                {
                    status = true,
                    message = "Successfully deleted teacher data!"
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
