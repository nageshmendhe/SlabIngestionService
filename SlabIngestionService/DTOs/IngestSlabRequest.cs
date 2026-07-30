using SlabIngestionService.Enums;
using System.ComponentModel.DataAnnotations;

namespace SlabIngestionService.DTOs
{
    public class IngestSlabRequest
    {
        [Required]
        public string SlabId { get; set; } = string.Empty;

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public SlabStatus Status { get; set; }
    }
}
