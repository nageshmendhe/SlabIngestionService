using Microsoft.EntityFrameworkCore;
using SlabIngestionService.Data;
using SlabIngestionService.DTOs;
using SlabIngestionService.Enums;
using SlabIngestionService.Models;

namespace SlabIngestionService.Services
{
    public class SlabService : ISlabService
    {
        private readonly ApplicationDbContext _context;

        public SlabService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SlabResponse> IngestAsync(IngestSlabRequest request)
        {
            var slab = await _context.Slabs
                .FirstOrDefaultAsync(x => x.SlabId == request.SlabId);

            if (slab == null)
            {
                slab = new Slab
                {
                    SlabId = request.SlabId
                };

                _context.Slabs.Add(slab);
            }

            slab.Weight = request.Weight;
            slab.Length = request.Length;
            slab.Width = request.Width;
            slab.Status = request.Status;
            slab.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }

            return new SlabResponse
            {
                SlabId = slab.SlabId,
                Weight = slab.Weight,
                Length = slab.Length,
                Width = slab.Width,
                Status = slab.Status,
                UpdatedAt = slab.UpdatedAt
            };
        }

        public async Task<SlabResponse?> GetByIdAsync(string slabId)
        {
            return await _context.Slabs
                .Where(x => x.SlabId == slabId)
                .Select(x => new SlabResponse
                {
                    SlabId = x.SlabId,
                    Weight = x.Weight,
                    Length = x.Length,
                    Width = x.Width,
                    Status = x.Status,
                    UpdatedAt = x.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SlabResponse>> GetAsync(
            SlabStatus? status,
            DateTime? from,
            DateTime? to)
        {
            var query = _context.Slabs.AsQueryable();

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            if (from.HasValue)
                query = query.Where(x => x.UpdatedAt >= from);

            if (to.HasValue)
                query = query.Where(x => x.UpdatedAt <= to);

            return await query
                .OrderByDescending(x => x.UpdatedAt)
                .Select(x => new SlabResponse
                {
                    SlabId = x.SlabId,
                    Weight = x.Weight,
                    Length = x.Length,
                    Width = x.Width,
                    Status = x.Status,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();
        }

    }
}
