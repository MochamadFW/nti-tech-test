using Microsoft.EntityFrameworkCore;
using NTI_Technical_Test.Data;
using NTI_Technical_Test.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTI_Technical_Test.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByNisnAsync(string nisn);
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }

    public class StudentService : IStudentService
    {
        private readonly NtiContext _context;

        public StudentService(NtiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            try
            {
                return await _context.Students.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving students data: {ex.Message}");
            }
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching student with ID : {ex.Message}");
            }
        }

        public async Task<Student> GetByNisnAsync(string nisn)
        {
            try
            {
                return await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Nisn == nisn);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching student with NISN : {ex.Message}");
            }
        }

        public async Task CreateAsync(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating a new student : {ex.Message}");
            }
        }

        public async Task UpdateAsync(Student student)
        {
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating student : {ex.Message}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting student : {ex.Message}");
            }
        }
    }
}
