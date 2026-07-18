using Application.DTOs.StudentDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentMasterDTO>> GetAllAsync();

        Task<StudentMasterDTO?> GetByIdAsync(int id);

        Task<StudentMasterCreateDTO> AddAsync(StudentMasterCreateDTO student);

        Task<StudentMasterUpdateDTO?> UpdateAsync(StudentMasterUpdateDTO student);

        Task<bool> DeleteAsync(int id);
    }
}
