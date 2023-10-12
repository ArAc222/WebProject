using System;
using System.Collections.Generic;
using Example.Model;

namespace Example.Service.Common
{
    public interface IJobService
    {
        List<JobModel> GetJobsAsync();
        JobModel GetJobAsync(Guid id);
        bool AddJobAsync(JobModel job);
        bool UpdateJobAsync(Guid id, JobModel job);
        bool DeleteJobAsync(Guid id);
        List<WorkerModel> GetWorkersForJobAsync(Guid jobId);
    }
}

