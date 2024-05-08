using jobconnect.Models;

namespace jobconnect.Data
{
    public interface IDataRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int jobId, int jobSeekerId);
        Task<List<Proposal>> GetByJobIdAsync(int jobId);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> Save();

    }
}
