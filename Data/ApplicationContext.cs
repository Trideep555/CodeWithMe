using CodeWithMe.Models;
using CodeWithMe.Views.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeWithMe.Data
{
    public class ApplicationContext :IdentityDbContext<IdentityUser>
    {
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            
        }
        public DbSet<Models.Languages> Languages { get; set; }
        public DbSet<Models.Types> Types { get; set; }
        public DbSet<Models.Program> Program { get; set; }

    }
}
