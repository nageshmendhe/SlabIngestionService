using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlabIngestionService.DTOs;
using SlabIngestionService.Enums;
using SlabIngestionService.Services;

namespace SlabIngestionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlabsController : ControllerBase
    {
        private readonly ISlabService _service;

        public SlabsController(ISlabService service)
        {
            _service = service;
        }

        [HttpPost("ingest")]
        public async Task<IActionResult> Ingest([FromBody] IngestSlabRequest request)
        {
            var result = await _service.IngestAsync(request);

            return Ok(result);
        }

        [HttpGet("{slabId}")]
        public async Task<IActionResult> GetById(string slabId)
        {
            var slab = await _service.GetByIdAsync(slabId);

            if (slab == null)
                return NotFound();

            return Ok(slab);
        }

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
