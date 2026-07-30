using SlabIngestionService.Data;

namespace SlabIngestionService.Services
{
    public class SlabService : ISlabService
    {
        private readonly ApplicationDbContext _context;

        public SlabService(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
