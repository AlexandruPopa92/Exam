using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exam.Models
{
    public class Post
    {
        [Key]
        public int PostId {get;set;}
        
        [Required]
        public string Content {get;set;}
        
        public int UserId {get;set;}
        public User Creator {get;set;}
        
        public List<Like> Likes {get;set;}

    }
}