using Microsoft.EntityFrameworkCore;
using NTI_Technical_Test.Data;
using NTI_Technical_Test.Models;

namespace NTI_Technical_Test.Services
{
    public interface IEnrollmentService
    {
        Task<List<Subject>> GetSubjectByStudentIdAsync(int studentId);
        Task<bool> EnrollStudentAsync(List<int> studentId, int subjectId);
        Task<bool> UnEnrollStudentAsync(List<int> studentId, int subjectId);
    }

    public class EnrollmentService : IEnrollmentService
    {
        private readonly NtiContext _context;

        public EnrollmentService(NtiContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetSubjectByStudentIdAsync(int studentId)
        {
            if (studentId <= 0)
                throw new ArgumentException("Invalid student Id!");

            return await _context.Subjects
                .Where(subject => subject.StudentIds != null && subject.StudentIds.Contains(studentId))
                .ToListAsync();
        }

        public async Task<bool> EnrollStudentAsync(List<int> studentIds, int subjectId)
        {
            if (subjectId <= 0)
                throw new ArgumentException("Invalid subject ID!");

            if (studentIds == null || !studentIds.Any())
                throw new ArgumentException("Student Id cannot be null or empty!");

            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found!");

            var validStudentIds = await _context.Students
                .Where(s => studentIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToListAsync();

            if (!validStudentIds.Any())
                throw new ArgumentException("No valid students found to enroll!");

            subject.StudentIds ??= new List<int>();
            subject.StudentIds.AddRange(validStudentIds.Except(subject.StudentIds));
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnEnrollStudentAsync(List<int> studentIds, int subjectId)
        {
            if (subjectId <= 0)
                throw new ArgumentException("Invalid Subject Id!");

            if (studentIds == null || !studentIds.Any())
                throw new ArgumentException("Student Id cannot be null or empty!");

            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject == null)
                throw new KeyNotFoundException("Subject not found!");

            if (subject.StudentIds == null || !subject.StudentIds.Any())
                return false;

            subject.StudentIds = subject.StudentIds.Except(studentIds).ToList();
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
