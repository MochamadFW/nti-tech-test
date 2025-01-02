using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTI_Technical_Test.Services;

namespace NTI_Technical_Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILogger<EnrollmentController> _logger;

        public EnrollmentController(IEnrollmentService enrollmentService, ILogger<EnrollmentController> logger)
        {
            _enrollmentService = enrollmentService;
            _logger = logger;
        }

        [HttpPost("{subjectId}/enroll-students")]
        public async Task<IActionResult> EnrollStudents(int subjectId, [FromBody] List<int> studentIds)
        {
            try
            {
                await _enrollmentService.EnrollStudentAsync(studentIds, subjectId);
                return Ok(new {
                    status = true,
                    message = "Successfull enroll new students!" 
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

        [HttpPost("{subjectId}/unenroll-students")]
        public async Task<IActionResult> UnenrollStudents(int subjectId, [FromBody] List<int> studentIds)
        {
            try
            {
                await _enrollmentService.UnEnrollStudentAsync(studentIds, subjectId);
                return Ok(new { 
                    status = true,
                    message = "Successfully unenroll students!" 
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

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetSubjectsByStudent(int studentId)
        {
            try
            {
                var subjects = await _enrollmentService.GetSubjectByStudentIdAsync(studentId);
                return Ok(new
                {
                    status = true,
                    message = "Successfully fetched list of subject from a student!",
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
    }
}
