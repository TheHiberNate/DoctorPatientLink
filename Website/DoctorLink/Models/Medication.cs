using System.ComponentModel.DataAnnotations;

namespace DoctorLink.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DrugName { get; set; }

        [Required]
        public string Dose { get; set; }

        [Required]
        public string? MedicationDescription { get; set; }

        public string? UsageDescription { get; set; }

        public string? Notes { get; set; }

        public int? NumberOfScans { get; set; } // to track number of scans for this specific medication by patient

    }
}