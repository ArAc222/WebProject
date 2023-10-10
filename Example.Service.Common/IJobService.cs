using System;
using System.Collections.Generic;
using Example.Model;

namespace Example.Service.Common
{
    public interface IJobService
    {
        List<JobModel> GetJobs();
        JobModel GetJob(Guid id);
        bool AddJob(JobModel job);
        bool UpdateJob(Guid id, JobModel job);
        bool DeleteJob(Guid id);
        List<WorkerModel> GetWorkersForJob(Guid jobId);
    }
}

