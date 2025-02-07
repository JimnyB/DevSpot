using DevSpot.Data;
using DevSpot.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace DevSpot.Repositories
{
    public class JobPostingRepository : IRepository<JobPosting>
    {
        private readonly ApplicationDbContext _context;

        public JobPostingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(JobPosting entity)
        {
            await _context.jobPostings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var delete_JobPosting = await _context.jobPostings.FindAsync(id);

            if (delete_JobPosting == null)
            {
                throw new KeyNotFoundException();
            }
            _context.jobPostings.Remove(delete_JobPosting);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await _context.jobPostings.ToListAsync();
        }

        public async Task<JobPosting> GetByIdAsync(string id)
        {
            var Id_JobPosting = await _context.jobPostings.FindAsync(id);

            if (Id_JobPosting == null)
            {
                throw new KeyNotFoundException();
            }
            return Id_JobPosting;
        }

        public async Task UpdateAsync(JobPosting entity)
        {
            _context.jobPostings.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
