using Microsoft.AspNetCore.Mvc;
using NTI_Technical_Test.Models;
using NTI_Technical_Test.Services;

namespace NTI_Technical_Test.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<SubjectController> _logger;

        public SubjectController(ISubjectService subjectService, ILogger<SubjectController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var subjects = await _subjectService.GetAllAsync();
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched list of subjects!",
                    data = subjects
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
                var checkSubject = await _subjectService.GetByIdAsync(id);
                if (checkSubject == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        message = "Subject not found"
                    });
                }
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched detail of a subject!",
                    data = checkSubject
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
        public async Task<IActionResult> Create([FromBody] Subject subject)
        {
            try
            {
                subject.StudentIds = null;
                await _subjectService.CreateAsync(subject);
                return CreatedAtAction(nameof(GetById), new { id = subject.Id }, new
                {
                    status = true, 
                    message = "Successfully created a new subject!",
                    data = subject
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
        public async Task<IActionResult> Update(int id, [FromBody] Subject subject)
        {
            try
            {
                subject.Id = id;
                subject.StudentIds = null;
                await _subjectService.UpdateAsync(subject);
                return Ok(new
                {
                    status = true,
                    message = "Successfully updated subject data!",
                    data = subject
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
                var existingSubject = await _subjectService.GetByIdAsync(id);
                if (existingSubject == null) {
                    return NotFound(new
                    {
                        status = false,
                        message = "Subject not found!"
                    });
                }

                await _subjectService.DeleteAsync(id);
                return Ok(new
                {
                    status = true,
                    message = "Successfully deleted subject data"
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
