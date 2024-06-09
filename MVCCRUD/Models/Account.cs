using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCRUD.Models
{
    public class Account
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name ="Enter Your First Name ")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Enter Your Last Name ")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [Display(Name = "Enter Your Mobile Number ")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "Enter Your Email Id ")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Enter Your Your Address ")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Enter Your Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Repeat Your Password ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginAccount
    {
        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "Enter Your Email Id ")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Enter Your Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UpdateAccount
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "Enter Your First Name ")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Enter Your Last Name ")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [Display(Name = "Enter Your Mobile Number ")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "Enter Your Email Id ")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Enter Your Your Address ")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
    }

    public class ChangePassword
    {
        [Required(ErrorMessage = "Old Password is required")]
        [Display(Name = "Enter Your Old Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [Display(Name = "Enter Your New Password ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [Display(Name = "Repear Your New Password ")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }

}