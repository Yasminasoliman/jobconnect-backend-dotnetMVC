using jobconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace jobconnect.Data
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly DataContext _db;
        private readonly DbSet<T> table;

        public DataRepository(DataContext db)
        {
            _db = db;
            table = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }
        public async Task<List<Proposal>> GetByJobIdAsync(int jobId)
        {
            // retrieve data from job table S
            return await _db.Proposal.Where(p => p.JobId == jobId).ToListAsync();
        }
        //  GetByIdAsync : JobId and JobSeekerId
        public async Task<T> GetByIdAsync(int jobId, int jobSeekerId)
        {
            
            if (typeof(T) == typeof(Proposal))
            {
                
                var proposals = table as DbSet<Proposal>;
                return await proposals.FirstOrDefaultAsync(x => x.JobId == jobId && x.JobSeekerId == jobSeekerId) as T;
            }

            return null;
        }
        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            table.Remove(entity);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }




/*        public async Task<bool> AcceptJobPost(int jobId)
        {
            var jobPost = await _db.JobPosts.FindAsync(jobId);
            if (jobPost != null)
            {
                jobPost.Status = JobPostStatus.Accepted;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectJobPost(int jobId)
        {
            var jobPost = await _db.JobPosts.FindAsync(jobId);
            if (jobPost != null)
            {
                jobPost.Status = JobPostStatus.Rejected;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }*/
    }
}
