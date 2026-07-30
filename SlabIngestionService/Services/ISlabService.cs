using SlabIngestionService.DTOs;
using SlabIngestionService.Enums;

namespace SlabIngestionService.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISlabService
    {
        /// <summary>
        /// Ingests the asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<SlabResponse> IngestAsync(IngestSlabRequest request);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="slabId">The slab identifier.</param>
        /// <returns></returns>
        Task<SlabResponse?> GetByIdAsync(string slabId);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        Task<IEnumerable<SlabResponse>> GetAsync(
            SlabStatus? status,
            DateTime? from,
            DateTime? to);
    }
}
