using SlabIngestionService.Enums;

namespace SlabIngestionService.DTOs
{
    public class SlabResponse
    {
        public string SlabId { get; set; } = string.Empty;

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public SlabStatus Status { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
