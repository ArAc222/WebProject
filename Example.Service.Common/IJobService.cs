using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;
using Example.Model.Common;

namespace Example.Service.Common
{
    public interface IJobService
    {
        Task<List<IJobModel>> GetJobsAsync();
        Task<IJobModel> GetJobAsync(Guid id);
        Task<bool> AddJobAsync(JobModel job);
        Task<bool> UpdateJobAsync(Guid id, JobModel job);
        Task<bool> DeleteJobAsync(Guid id);
        Task<List<IWorkerModel>> GetWorkersForJobAsync(Guid jobId);
    }
}

