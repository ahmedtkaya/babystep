using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Dtos;
using babystepV1.Models;

namespace babystepV1.Interfaces
{
    public interface IKidsRepository
    {
        Task<Kids> AddKidsAsync(Kids kid);
        Task<Kids> AddKidsToAsync(Guid userId, CreateKidsDto createKidsDto);
        Task<Kids?> GetKidByIdAsync(Guid id);
        Task<IEnumerable<Kids>> GetKidsByUserIdAsync(string userId);
        Task<Kids?> UpdateKidAsync(Guid id, UpdateKidDto updateKidDto);
        Task<Kids?> DeleteKidAsync(Guid id);

    }
}