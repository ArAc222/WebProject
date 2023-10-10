using System;
using System.Collections.Generic;
using Example.Model;

namespace Example.Service.Common
{
    public interface IWorkerService
    {
        List<WorkerModel> GetWorkers();
        WorkerModel GetWorker(Guid id);
        bool AddWorker(WorkerModel worker);
        bool UpdateWorker(Guid id, WorkerModel worker);
        bool DeleteWorker(Guid id);
        bool SetJob(Guid workerId, Guid jobId);
    }
}
