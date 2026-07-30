using Microsoft.EntityFrameworkCore;
using SlabIngestionService.Data;
using SlabIngestionService.DTOs;
using SlabIngestionService.Enums;
using SlabIngestionService.Models;

namespace SlabIngestionService.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SlabIngestionService.Services.ISlabService" />
    public class SlabService : ISlabService
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlabService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SlabService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ingests the asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="slabId">The slab identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
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
