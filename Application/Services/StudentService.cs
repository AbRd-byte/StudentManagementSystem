using Application.DTOs;
using Application.DTOs.StudentDTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentMasterDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(s => new StudentMasterDTO
            {
                Id = s.Id,  
                FirstName = s.FirstName,
                LastName = s.LastName,
                Contact = s.Contact,
                Address = s.Address,
                ZipCode = s.ZipCode,
                City = s.City,
                State = s.State,
                Course = s.Course,
                IsActive = s.IsActive,
                CreatedBy = s.CreatedBy,
                CreatedOn = s.CreatedOn
            });
        }
        public async Task<StudentMasterDTO?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return null;
            return new StudentMasterDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Contact = student.Contact,
                Address = student.Address,
                ZipCode = student.ZipCode,
                City = student.City,
                State = student.State,
                Course = student.Course,
                IsActive = student.IsActive,
                CreatedBy = student.CreatedBy,
                CreatedOn = student.CreatedOn
            };
        }
        public async Task<StudentMasterDTO> CreateStudentAsync(StudentMasterCreateDTO studentDto)
        {
            var student = new StudentMasterCreateDTO
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Contact = studentDto.Contact,
                Address = studentDto.Address,
                ZipCode = studentDto.ZipCode,
                City = studentDto.City,
                State = studentDto.State,
                Course = studentDto.Course,
                IsActive = studentDto.IsActive,
                CreatedBy = studentDto.CreatedBy,
                CreatedOn = studentDto.CreatedOn
            };                
            var createdStudent = await _studentRepository.AddAsync(student);
            return new StudentMasterDTO
            {
                FirstName = createdStudent.FirstName,
                LastName = createdStudent.LastName,
                Contact = createdStudent.Contact,
                Address = createdStudent.Address,
                ZipCode = createdStudent.ZipCode,
                City = createdStudent.City,
                Course = createdStudent.Course,
                State = createdStudent.State,
                IsActive = true, 
                CreatedBy = createdStudent.CreatedBy,
                CreatedOn = new DateTime()
            };
        }
        public async Task<StudentMasterDTO?> UpdateStudentAsync(int id, StudentMasterUpdateDTO studentDto)
        {
            var student = new StudentMasterUpdateDTO
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Contact = studentDto.Contact,
                Address = studentDto.Address,
                ZipCode = studentDto.ZipCode,
                City = studentDto.City,
                State = studentDto.State,
                IsActive = studentDto.IsActive,
                Course = studentDto.Course,
                CreatedBy = "Abhishek",
                CreatedOn = studentDto.CreatedOn
            };
            var updatedStudent = await _studentRepository.UpdateAsync(student);
            if (updatedStudent == null)
                return null;
            return new StudentMasterDTO
            {
                Id = updatedStudent.Id,
                FirstName = updatedStudent.FirstName,
                LastName = updatedStudent.LastName,
                Contact = updatedStudent.Contact,
                Address = updatedStudent.Address,
                ZipCode = updatedStudent.ZipCode,
                City = updatedStudent.City,
                State = updatedStudent.State,
                Course = updatedStudent.Course,
                IsActive = updatedStudent.IsActive,
                CreatedBy = updatedStudent.CreatedBy,
                CreatedOn = updatedStudent.CreatedOn
            };
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _studentRepository.DeleteAsync(id);
        }
    }
}
