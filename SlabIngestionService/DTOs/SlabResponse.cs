using SlabIngestionService.Enums;

namespace SlabIngestionService.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class SlabResponse
    {
        /// <summary>
        /// Gets or sets the slab identifier.
        /// </summary>
        /// <value>
        /// The slab identifier.
        /// </value>
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
    }
}
