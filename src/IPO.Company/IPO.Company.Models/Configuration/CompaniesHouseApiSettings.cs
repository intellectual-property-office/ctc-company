using System.ComponentModel.DataAnnotations;

namespace IPO.Company.Models.Configuration
{
    public class CompaniesHouseApiSettings
    {
        [Required]
        public string AuthenticationType { get; set; }
        [Required]
        [StringLength(36, MinimumLength = 32)]
        public string APIKey { get; set; }
        [Required]
        public Uri BaseAddress { get; set; }
        [Required]
        public string AcceptMediaType { get; set; }
    }
}
