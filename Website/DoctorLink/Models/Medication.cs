using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorLink.Models
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }

        [Required]
        [DisplayName("Medication")]

        public string DrugName { get; set; }

        [Required]
        public string Dose { get; set; }

        [Required]
        [DisplayName("Description")]
        public string? MedicationDescription { get; set; }

        [Required]
        [DisplayName("Usage")]
        public string? UsageDescription { get; set; }

        public string? Notes { get; set; } = "None";

        [DisplayName("# QR scans")]
        public int? NumberOfScans { get; set; } = 0; // to track number of scans for this specific medication by patient

    }
}