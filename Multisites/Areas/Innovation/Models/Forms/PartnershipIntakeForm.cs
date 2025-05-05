using NM_MultiSites.Areas.Innovation.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.Innovation.Models.Forms
{
    public class PartnershipIntakeForm
    {
        /*Step 1 Contact Info*/
        [Required(ErrorMessage = "Enter your full name.")]
        [MaxLength(50, ErrorMessage = "Full Name length cannot exceed 50 characters.")]
        [RegularExpression(@"^[ A-Za-z./&+-/']*$", ErrorMessage = "Names can only contain letters, spaces ( ), hyphens (-) and apostrophes (‘).")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter a valid email address.")]
        [MaxLength(254, ErrorMessage = "Email length cannot exceed 254 characters.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter your email in the format: yourname@example.com.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter a phone number.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^(\+?[0-9]{1,3})?[-. ]?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(18, MinimumLength = 10, ErrorMessage = "Incorrect format")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your company name.")]
        [MaxLength(50, ErrorMessage = "Company Name length cannot exceed 50 characters.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Enter your title or affiliation.")]
        [MaxLength(50, ErrorMessage = "Title /Affiliation length cannot exceed 50 characters.")]
        [Display(Name = "Title /Affiliation")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter your company website.")]
        [RegularExpression(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)", ErrorMessage = "Enter a valid website.")]
        [Display(Name = "Company URL")]
        public string CompanyURL { get; set; }
        [Required(ErrorMessage = "Upload a file.")]
        [FileSize(15728640, ErrorMessage = "File size too big. Max size is 15MB.")]
        [FileType("PDF", ErrorMessage = "Upload files of pdf type.")]
        [Display(Name = "Please Upload Your Company Pitch Deck (PDF Only).")]
        public HttpPostedFileBase CompanyPitch { get; set; }
        /*Step 2 Problem*/
        [Display(Name = "In two to three sentences, briefly describe the problem your business idea will address.")]        
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string BusinessIdea { get; set; }

        public IEnumerable<SelectListItem> Priorities { get; set; }
        [Required(ErrorMessage = "Select a priority.")]
        [Display(Name= "What Northwestern Medicine priority does your solution align to? *")]
        public string Priority { get; set; }
        /*Step 3 Solution*/
        [Display(Name = "In two to three sentences, briefly describe your solution.")]        
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string SolutionMessage { get; set; }

        [Display(Name = "What benefits will the proposed solution bring to Northwestern Medicine (e.g. positive patient experience, increased efficiency, reduced risk, revenue capture)?")]       
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string BenefitsMessage { get; set; }

        [Display(Name = "How do you measure ROI, and what are your success metrics?")]        
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string ROIMessage { get; set; }

        [Display(Name = "In two to three sentences, tell us what differentiates you from your competitors.")]        
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string CompetitorsMessage { get; set; }

        [Display(Name = "List any other hospitals or health systems that currently use your solution.")]        
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string OthersInfoMessage { get; set; }
        /*Step 4 Partnership*/

        [Display(Name = "In one or two sentences, tell us what you are seeking in a partnership with Northwestern Medicine.")]
        [MaxLength(500, ErrorMessage = "Allowed character limit has been met.")]
        [Required(ErrorMessage = "Leave a response that is 500 characters or less.")]
        public string PartnershipMessage { get; set; }

        [Required(ErrorMessage = "Make a selection.")]
        [Display(Name = "Are you seeking investment?")]
        public string SelectedOption { get; set; }

        [Display(Name = "Are you seeking investment?")]
        public string Option { get; set; }

        public string ConfirmationMessage { get; set; }

        public string Robotest { get; set; }
        public PartnershipIntakeForm()
        {
            Priorities = GetPriorities();
        }

        #region Private Methods

        public List<SelectListItem> GetPriorities()
        {
            List<SelectListItem> priorityList = new List<SelectListItem>();
            priorityList.Add(new SelectListItem { Value = "", Text = "Choose an option" });
            priorityList.Add(new SelectListItem { Value = "Clinical Automation and Robotics", Text = "Clinical Automation and Robotics" });
            priorityList.Add(new SelectListItem { Value = "Augmented and Virtual Reality", Text = "Augmented and Virtual Reality" });
            priorityList.Add(new SelectListItem { Value = "Hospital Room of the Future", Text = "Hospital Room of the Future" });
            priorityList.Add(new SelectListItem { Value = "Personalized Medicine/Clinical CRM", Text = "Personalized Medicine/Clinical CRM" });
            priorityList.Add(new SelectListItem { Value = "Augmented Intelligence (AI)", Text = "Augmented Intelligence (AI)" });
            priorityList.Add(new SelectListItem { Value = "Hospital at Home", Text = "Hospital at Home" });
            priorityList.Add(new SelectListItem { Value = "Command Center", Text = "Command Center" });
            priorityList.Add(new SelectListItem { Value = "Patient Experience and Engagement", Text = "Patient Experience and Engagement" });

            return priorityList;
        }

        #endregion
    }
}