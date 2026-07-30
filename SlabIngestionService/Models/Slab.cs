using SlabIngestionService.Enums;
using System.ComponentModel.DataAnnotations;

namespace SlabIngestionService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Slab
    {
        /// <summary>
        /// Gets or sets the slab identifier.
        /// </summary>
        /// <value>
        /// The slab identifier.
        /// </value>
        [Key]
        public string SlabId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public decimal Weight { get; set; }
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public decimal Length { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public decimal Width { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public SlabStatus Status { get; set; }
        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>
        /// The updated at.
        /// </value>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the row version.
        /// </summary>
        /// <value>
        /// The row version.
        /// </value>
        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();


    }
}
