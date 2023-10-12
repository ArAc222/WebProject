using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Repository.Common
{
    public interface IJobRepository
    {
        Task<List<IJobModel>> GetJobsAsync();
        Task<IJobModel> GetJobAsync(Guid id);
        Task<bool> PostAsync(IJobModel job);
        Task<bool> PutAsync(Guid id, IJobModel job);
        Task<bool> DeleteAsync(Guid id);
        Task<List<IWorkerModel>> GetWorkersForJobAsync(Guid jobId);
    }

}
