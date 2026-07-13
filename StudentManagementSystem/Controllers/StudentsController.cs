using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return Ok(students);
        }

        [HttpGet("{id:int}/student")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }

        [HttpPost("insertrecords")]
        public async Task<IActionResult> InsertStudent([FromBody] CreateStudentDTO studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is null.");
            }
            var createdStudent = await _studentService.CreateStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetAllStudents), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPost("{id:int}/updaterecords")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDTO studentDto, int id)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is null.");
            }
            var updatedStudent = await _studentService.UpdateStudentAsync(id, studentDto);
            if (updatedStudent == null)
            {
                return NotFound($"Student with ID {studentDto.Id} not found.");
            }
            return Ok(updatedStudent);
        }

        [HttpDelete("deleterecords/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var isDeleted = await _studentService.DeleteStudentAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
