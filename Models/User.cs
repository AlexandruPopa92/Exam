using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exam.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}


        [Required]
        [Display(Name = "First Name")]  
        [MinLength(2, ErrorMessage="First Name must be 2 dcharacters or longer!")]
        public string FirstName {get;set;}
        
        
        [Required]
        [Display(Name = "Last Name")]  
        [MinLength(2,ErrorMessage="Last Name must be 2 characters or longer!")]
        public string LastName {get;set;}
        
        
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        
        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}
        
        public DateTime CreatedAt {get;set;}= DateTime.Now;
        public DateTime UpdatedAt {get;set;}= DateTime.Now;
        
        
        [NotMapped]
        [Required]
        [Display(Name = "Password Confirmation")]  
        [Compare("Password",ErrorMessage="Password did not match!")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        
        public List<Post> Posted {get;set;}
        public List<Like> Liked {get;set;}

    }
}