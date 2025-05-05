using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Forms
{
    public class ContactUsForm
    {
        [Required(ErrorMessage = "Enter your full name.")]
        [MaxLength(50, ErrorMessage = "Full Name length cannot exceed 50 characters.")]
        [RegularExpression("^[ A-Za-z./&+-/']*$", ErrorMessage = "Names can only contain letters, spaces ( ), hyphens (-) and apostrophes (‘).")]
        [Display(Name = "Full Name *")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter your email in the format: yourname@example.com.")]
        [MaxLength(254, ErrorMessage = "Email length cannot exceed 254 characters.")]
        [Display(Name = "Email *")]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Message *")]
        [RegularExpression("^((?!(https?:.*(?=))).)*$", ErrorMessage = "URLs are not allowed.")]
        [MaxLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string FormMessage { get; set; }

        public string ConfirmationMessage { get; set; }

        public string Robotest { get; set; }
    }
}