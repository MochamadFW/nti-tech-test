using Microsoft.EntityFrameworkCore;
using NTI_Technical_Test.Data;
using NTI_Technical_Test.Models;

namespace NTI_Technical_Test.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject> GetByIdAsync(int id);
        Task CreateAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        Task DeleteAsync(int id);
    }
    
    public class SubjectService : ISubjectService
    {
        private readonly NtiContext _context;

        public SubjectService(NtiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.AsNoTracking().ToListAsync();
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching detail subject data : {ex.Message}");
            }
        }

        public async Task CreateAsync(Subject subject)
        {
            try
            {
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating a new subject : {ex.Message}");
            }
        }

        public async Task UpdateAsync(Subject subject)
        {
            try
            {
                _context.Subjects.Update(subject);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating subject data : {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var subject = await _context.Subjects.FindAsync(id);
                if (subject != null)
                {
                    _context.Subjects.Remove(subject);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting subject : {ex.Message}");
            }
        }
    }
}
