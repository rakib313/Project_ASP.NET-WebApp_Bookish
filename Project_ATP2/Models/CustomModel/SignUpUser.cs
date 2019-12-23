using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_ATP2.Models.CustomModel
{
    public class SignUpUser
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Please enter only characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Password Required")]
        [Compare("Password", ErrorMessage = "passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth required")]
        public DateTime DOB { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number required")]
        [RegularExpression("\\d{1,11}", ErrorMessage = "Please enter valid phone number")]
        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Area Required")]
        public string Area { get; set; }

    }
}