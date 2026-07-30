using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlabIngestionService.DTOs;
using SlabIngestionService.Enums;
using SlabIngestionService.Services;

namespace SlabIngestionService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class SlabsController : ControllerBase
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly ISlabService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlabsController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public SlabsController(ISlabService service)
        {
            _service = service;
        }

        /// <summary>
        /// Ingests the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("ingest")]
        public async Task<IActionResult> Ingest([FromBody] IngestSlabRequest request)
        {
            try
            {
                var result = await _service.IngestAsync(request);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new
                {
                    message = "The slab was modified by another request. Please retry."
                });
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="slabId">The slab identifier.</param>
        /// <returns></returns>
        [HttpGet("{slabId}")]
        public async Task<IActionResult> GetById(string slabId)
        {
            var slab = await _service.GetByIdAsync(slabId);

            if (slab == null)
                return NotFound();

            return Ok(slab);
        }

        /// <summary>
        /// Gets the specified status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(
            SlabStatus? status,
            DateTime? from,
            DateTime? to)
        {
            var slabs = await _service.GetAsync(status, from, to);

            return Ok(slabs);
        }
    }
}
