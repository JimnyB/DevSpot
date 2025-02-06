using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using DevSpot.Models;
namespace DevSpot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<JobPosting> jobPostings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }
    }
}
