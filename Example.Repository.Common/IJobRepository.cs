using System;
using System.Collections.Generic;
using Example.Model.Common;

namespace Example.Repository.Common
{
    public interface IJobRepository
    {
        List<IJobModel> GetJobs();
        IJobModel GetJob(Guid id);
        bool Post(IJobModel job);
        bool Put(Guid id, IJobModel job);
        bool Delete(Guid id);
        List<IWorkerModel> GetWorkersForJob(Guid jobId);
    }

}
