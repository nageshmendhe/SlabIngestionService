using SlabIngestionService.Enums;
using System.ComponentModel.DataAnnotations;

namespace SlabIngestionService.Models
{
    public class Slab
    {
        [Key]
        public string SlabId { get; set; } = string.Empty;
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public SlabStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
