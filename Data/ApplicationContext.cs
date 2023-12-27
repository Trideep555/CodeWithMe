using CodeWithMe.Models;
using CodeWithMe.Views.Admin;
using Microsoft.EntityFrameworkCore;

namespace CodeWithMe.Data
{
    public class ApplicationContext : DbContext
    {
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            
        }
        public DbSet<Models.Languages> Languages { get; set; }
        public DbSet<Models.Types> Types { get; set; }
        public DbSet<Models.Program> Program { get; set; }

    }
}
