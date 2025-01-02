using Microsoft.AspNetCore.Mvc;
using NTI_Technical_Test.Models;
using NTI_Technical_Test.Services;

namespace NTI_Technical_Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ILogger<ClassController> _logger;

        public ClassController(IClassService classService, ILogger<ClassController> logger)
        {
            _classService = classService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var classes = await _classService.GetAllAsync();
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched list of classes!",
                    data = classes
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
                var checkClass = await _classService.GetByIdAsync(id);
                if (checkClass == null)
                {
                    return NotFound(new { 
                        status = false,
                        message = "Class not found" 
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched detail of a class!",
                    data = checkClass
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
        public async Task<IActionResult> Create([FromBody] Class classes)
        {
            try
            {
                await _classService.CreateAsync(classes);
                return CreatedAtAction(nameof(GetById), new { id = classes.Id }, new
                {
                    status = true,
                    message = "Successfully created a new class!",
                    data = classes
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
        public async Task<IActionResult> Update(int id, [FromBody] Class classes)
        {
            try
            {
                classes.Id = id;
                await _classService.UpdateAsync(classes);
                return Ok(new
                {
                    status = true,
                    message = "Successfully updated class data!",
                    data = classes
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
                var classData = await _classService.GetByIdAsync(id);
                if (classData == null)
                {
                    return NotFound(new { 
                        status = false,
                        message = "Class not found!" 
                    });
                }

                await _classService.DeleteAsync(id);
                return Ok(new
                {
                    status = true,
                    message = "Successfully deleted class data"
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
