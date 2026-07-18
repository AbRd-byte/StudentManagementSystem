using Application.DTOs.StudentDTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentMasterDTO>> GetAllAsync()
        {
            return await _context.StudentMaster
                .Select(s => new StudentMasterDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Contact = s.Contact,
                    Address = s.Address,
                    City = s.City,
                    State = s.State,
                    ZipCode = s.ZipCode,
                    Course = s.Course,
                    IsActive = s.IsActive,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.StudentMaster.FindAsync(id);

            if (student == null)
                return false;

            _context.StudentMaster.Remove(student);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<StudentMasterDTO?> GetByIdAsync(int id)
        {
            return await _context.StudentMaster
                .Where(s => s.Id == id)
                .Select(s => new StudentMasterDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Contact = s.Contact,
                    Address = s.Address,
                    City = s.City,
                    State = s.State,
                    ZipCode = s.ZipCode,
                    Course = s.Course,
                    IsActive = s.IsActive,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn
                })
                .FirstOrDefaultAsync();
        }

        public async Task<StudentMasterUpdateDTO?> UpdateAsync(StudentMasterUpdateDTO student)
        {
            var existingStudent = _context.StudentMaster.Find(student.Id);
            if (existingStudent == null)
                return await Task.FromResult<StudentMasterUpdateDTO?>(null);
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Contact = student.Contact;
            existingStudent.Address = student.Address;
            existingStudent.City = student.City;
            existingStudent.State = student.State;
            existingStudent.ZipCode = student.ZipCode;
            existingStudent.Course = student.Course;
            existingStudent.IsActive = student.IsActive;
            existingStudent.CreatedBy = student.CreatedBy;
            existingStudent.CreatedOn = new DateTime();
            await _context.SaveChangesAsync();
            return new StudentMasterUpdateDTO
            {
                Id = existingStudent.Id,
                FirstName = existingStudent.FirstName,
                LastName = existingStudent.LastName,
                Contact = existingStudent.Contact,
                Address = existingStudent.Address,
                City = existingStudent.City,
                State = existingStudent.State,
                ZipCode = existingStudent.ZipCode,
                Course = existingStudent.Course,
                IsActive = existingStudent.IsActive,
                CreatedBy = existingStudent.CreatedBy,
                CreatedOn = existingStudent.CreatedOn
            };
        }

        public async Task<StudentMasterCreateDTO> AddAsync(StudentMasterCreateDTO student)
        {
            var newStudent = new StudentMaster
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Contact = student.Contact,
                Address = student.Address,
                City = student.City,
                State = student.State,
                ZipCode = student.ZipCode,
                Course = student.Course,
                IsActive = student.IsActive,
                CreatedBy = student.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
            _context.StudentMaster.Add(newStudent);
            bool isInserted = Convert.ToBoolean(_context.SaveChanges());
            if (!isInserted)
            {
                return await Task.FromResult<StudentMasterCreateDTO>(null);
            }
            return await Task.FromResult(new StudentMasterCreateDTO
            {
                Id = newStudent.Id,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                Contact = newStudent.Contact,
                Address = newStudent.Address,
                City = newStudent.City,
                State = newStudent.State,
                ZipCode = newStudent.ZipCode,
                Course = newStudent.Course,
                IsActive = newStudent.IsActive,
                CreatedBy = newStudent.CreatedBy,
                CreatedOn = newStudent.CreatedOn
            });
        }
    }
}
