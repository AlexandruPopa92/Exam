using Microsoft.EntityFrameworkCore;
 
namespace Exam.Models
{
    public class ExamContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ExamContext(DbContextOptions<ExamContext> options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Post> Posts {get;set;}
        public DbSet<Like> Likes {get;set;}


    }
}