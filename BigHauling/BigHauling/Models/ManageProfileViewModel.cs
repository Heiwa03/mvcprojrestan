using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BigHauling.Models
{
    public class ManageProfileViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }
    }
} 