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

        private ApplicationDbContext CreateDbContext()
        {
            var context = new ApplicationDbContext(_options);
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();
            return context;

        }

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
        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);


           await  Assert.ThrowsAsync<KeyNotFoundException>(
               () => repository.GetByIdAsync("999")
               );

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPostings()
        {
            var db = CreateDbContext();
            var repository = new JobPostingRepository(db);

            var jobPosting1 = new JobPosting
            {
                Title = "Test Title 1",
                Description = "Test Description 1",
                PostedDate = DateTime.Now,
                Company = "Test Company 1",
                Location = "Test Location 1",
                UserId = "Test User Id 1"
            };
            var jobPosting2 = new JobPosting
            {
                Title = "Test Title 2",
                Description = "Test Description 2",
                PostedDate = DateTime.Now,
                Company = "Test Company 2",
                Location = "Test Location 2",
                UserId = "Test User Id 2"
            };

            await db.jobPostings.AddRangeAsync(jobPosting1, jobPosting2);
            await db.SaveChangesAsync();

            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPosting()
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

            jobPosting.Description = "Updated Description";

            await repository.UpdateAsync(jobPosting);

            var result = db.jobPostings.Find(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal("Updated Description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletejobPosting()
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

            await repository.DeleteAsync(jobPosting.Id);

            var result = db.jobPostings.Find(jobPosting.Id);
            Assert.Null(result);

        }

    }
}
