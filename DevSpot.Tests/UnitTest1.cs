using Microsoft.EntityFrameworkCore;
using DevSpot.Data;
using DevSpot.Repositories;
using DevSpot.Models;



namespace DevSpot.Tests
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test User Id"
            };

            await repository.AddAsync(jobPosting);

            var result = db.jobPostings.SingleOrDefault(x => x.Title == "Test Title");

            Assert.NotNull(result);
            Assert.Equal("Test Description", result.Description);

        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);
            var jobPosting = new JobPosting
            {
                Title = "Test Title",
                Description = "Test Description",
                PostedDate = DateTime.Now,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test User Id"
            };

            await db.jobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();


            var result = await repository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Title", result.Title);


        }
    }
}
