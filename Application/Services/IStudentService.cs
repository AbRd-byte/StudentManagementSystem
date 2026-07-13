using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();

        Task<StudentDTO?> GetStudentByIdAsync(int id);

        Task<StudentDTO> CreateStudentAsync(CreateStudentDTO studentDto);

        Task<StudentDTO?> UpdateStudentAsync(int id, UpdateStudentDTO studentDto);

        Task<bool> DeleteStudentAsync(int id);

    }
}
