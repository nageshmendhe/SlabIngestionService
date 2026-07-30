using SlabIngestionService.DTOs;
using SlabIngestionService.Enums;

namespace SlabIngestionService.Services
{
    public interface ISlabService
    {
        Task<SlabResponse> IngestAsync(IngestSlabRequest request);

        Task<SlabResponse?> GetByIdAsync(string slabId);

        Task<IEnumerable<SlabResponse>> GetAsync(
            SlabStatus? status,
            DateTime? from,
            DateTime? to);
    }
}
