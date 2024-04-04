using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Forms
{
    public class ContactUsForm
    {
        [Required(ErrorMessage = "Enter your full name.")]
        [MaxLength(50, ErrorMessage = "Full Name length cannot exceed 50 characters.")]
        [RegularExpression(@"^[ A-Za-z./&+-/']*$", ErrorMessage = "Names can only contain letters, spaces ( ), hyphens (-) and apostrophes (‘).")]
        [Display(Name = "Full Name *")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter your email in the format: yourname@example.com.")]
        [MaxLength(254, ErrorMessage = "Email length cannot exceed 254 characters.")]
        [Display(Name = "Email *")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Message *")]
        [RegularExpression(@"^((?!(https?:.*(?=))).)*$", ErrorMessage = "URLs are not allowed.")]
        [MaxLength(500, ErrorMessage = "Cannot exceed 500 characters.")]
        public string FormMessage { get; set; }

        [Required(ErrorMessage = "Make a selection.")]
        public string SelectedCurrentRole { get; set; }

        [Display(Name = "Which of the following best describes your current role? *")]
        public string CurrentRole { get; set; }

        [Required(ErrorMessage = "Make a selection.")]
        
        public string SelectedTopic { get; set; }
        [Display(Name = "Which topics would you like to receive notifications about? *")]
        public string Topic { get; set; }

        public string ConfirmationMessage { get; set; }

        public string Robotest { get; set; }
    }
}