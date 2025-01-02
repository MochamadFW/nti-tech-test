using Microsoft.EntityFrameworkCore;
using NTI_Technical_Test.Data;
using NTI_Technical_Test.Models;

namespace NTI_Technical_Test.Services
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class> GetByIdAsync(int id);
        Task CreateAsync(Class classes);
        Task UpdateAsync(Class classes);
        Task DeleteAsync(int id);
    }

    public class ClassService : IClassService
    {
        private readonly NtiContext _context;

        public ClassService(NtiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _context.Classes.AsNoTracking().ToListAsync();
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Classes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching detail class data : {ex.Message}");
            }
        }

        public async Task CreateAsync(Class classes)
        {
            try
            {
                await _context.Classes.AddAsync(classes);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating a new class : {ex.Message}");
            }
        }

        public async Task UpdateAsync(Class classes)
        {
            try
            {
                _context.Classes.Update(classes);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating class data : {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var classes = await _context.Classes.FindAsync(id);
                if (classes != null)
                {
                    _context.Classes.Remove(classes);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting class : {ex.Message}");
            }
        }
    }
}
