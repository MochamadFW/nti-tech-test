using Microsoft.EntityFrameworkCore;
using NTI_Technical_Test.Data;
using NTI_Technical_Test.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTI_Technical_Test.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task<Teacher> GetByNipAsync(string nip);
        Task<Teacher> GetByNuptkAsync(string nuptk);
        Task CreateAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(int id);
    }

    public class TeacherService : ITeacherService
    {
        private readonly NtiContext _context;

        public TeacherService(NtiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.AsNoTracking().ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching teacher with ID : {ex.Message}");
            }
        }

        public async Task<Teacher> GetByNipAsync(string nip)
        {
            try
            {
                return await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Nip == nip);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching teacher with NIP : {ex.Message}");
            }
        }

        public async Task<Teacher> GetByNuptkAsync(string nuptk)
        {
            try
            {
                return await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Nuptk == nuptk);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching teacher with NUPTK : {ex.Message}");
            }
        }

        public async Task CreateAsync(Teacher teacher)
        {
            try
            {
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating a new teacher : {ex.Message}");
            }
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            try
            {
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating teacher : {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);
                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting teacher : {ex.Message}");
            }
        }
    }
}
