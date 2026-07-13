using Application.DTOs;
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

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Age = s.Age,
                Email = s.Email,
                Course = s.Course
            });
        }
        public async Task<StudentDTO?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return null;
            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                Course = student.Course
            };
        }
        public async Task<StudentDTO> CreateStudentAsync(CreateStudentDTO studentDto)
        {
            var student = new Domain.Entities.Student
            {
                Name = studentDto.Name,
                Age = studentDto.Age,
                Email = studentDto.Email,
                Course = studentDto.Course,
                Address = studentDto.Address,
                IsActive = studentDto.IsActive,
                CreatedBy = studentDto.CreatedBy,
                CreatedOn = new DateTime()
            };
            var createdStudent = await _studentRepository.AddAsync(student);
            return new StudentDTO
            {
                Id = createdStudent.Id,
                Name = createdStudent.Name,
                Age = createdStudent.Age,
                Email = createdStudent.Email,
                Course = createdStudent.Course,
                Address = createdStudent.Address,
                IsActive = createdStudent.IsActive,
                CreatedBy = createdStudent.CreatedBy,
                CreatedOn = createdStudent.CreatedOn
            };
        }
        public async Task<StudentDTO?> UpdateStudentAsync(int id, UpdateStudentDTO studentDto)
        {
            var student = new Student
            {
                Id = id,
                Name = studentDto.Name,
                Age = studentDto.Age,
                Email = studentDto.Email,
                Course = studentDto.Course,
                Address = studentDto.Address,
                IsActive = studentDto.IsActive,
                CreatedBy = studentDto.CreatedBy,
                CreatedOn = studentDto.CreatedOn
            };
            var updatedStudent = await _studentRepository.UpdateAsync(student);
            if (updatedStudent == null)
                return null;
            return new StudentDTO
            {
                Id = updatedStudent.Id,
                Name = updatedStudent.Name,
                Age = updatedStudent.Age,
                Email = updatedStudent.Email,
                Course = updatedStudent.Course,
                Address = updatedStudent.Address,
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
