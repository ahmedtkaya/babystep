using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using babystepV1.Data;
using babystepV1.Dtos;
using babystepV1.Models;
using Microsoft.EntityFrameworkCore;
using babystepV1.Interfaces;


namespace babystepV1.Repositories
{
    public class KidsRepository : IKidsRepository
    {
        private readonly AppDbContext _context;
        public KidsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Kids> AddKidsAsync(Kids kid)
        {
            try
            {
                await _context.Kids.AddAsync(kid);
                await _context.SaveChangesAsync();
                return kid;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging not shown in this example)
                throw new Exception("An error occurred while adding the kid.", ex);
            }
        }
        public async Task<Kids> AddKidsToAsync(Guid userId, CreateKidsDto createKidsDto)
        {
            var kid = new Kids
            {
                Name = createKidsDto.Name,
                BirthDate = createKidsDto.BirthDate,
                UserId = userId,
                Gender = createKidsDto.Gender // Eğer Kids modelinde Gender özelliği yoksa eklemelisin
            };

            try
            {

                await _context.Kids.AddAsync(kid);
                await _context.SaveChangesAsync();
                return kid;
            }
            catch (DbUpdateException ex)
            {
                // Log the exception (logging not shown in this example)
                throw new Exception("An error occurred while adding the kid.", ex);
            }
        }
        public async Task<Kids?> GetKidByIdAsync(Guid id)
        {
            return await _context.Kids.FindAsync(id);
        }
        public async Task<IEnumerable<Kids>> GetKidsByUserIdAsync(string userId)
        {
            return await _context.Kids.Where(k => k.UserId.ToString() == userId).ToListAsync();
        }

    }
}