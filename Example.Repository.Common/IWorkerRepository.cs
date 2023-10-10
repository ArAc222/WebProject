using System;
using System.Collections.Generic;
using Example.Model.Common;

namespace Example.Repository.Common
{
    public interface IWorkerRepository
    {
        List<IWorkerModel> GetWorkers();
        IWorkerModel GetWorker(Guid id);
        bool Post(IWorkerModel worker);
        bool Put(Guid id, IWorkerModel worker);
        bool Delete(Guid id);
        bool SetJob(Guid workerId, Guid jobId);
    }
}

