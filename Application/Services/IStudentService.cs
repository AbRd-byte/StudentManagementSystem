using Application.DTOs;
using Application.DTOs.StudentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentMasterDTO>> GetAllStudentsAsync();

        Task<StudentMasterDTO?> GetStudentByIdAsync(int id);

        Task<StudentMasterDTO> CreateStudentAsync(StudentMasterCreateDTO studentDto);

        Task<StudentMasterDTO?> UpdateStudentAsync(int id, StudentMasterUpdateDTO studentDto);

        Task<bool> DeleteStudentAsync(int id);

    }
}
