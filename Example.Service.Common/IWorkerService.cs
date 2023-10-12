using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model;
using Example.Model.Common;

namespace Example.Service.Common
{
    public interface IWorkerService
    {
        Task<List<IWorkerModel>> GetWorkers();
        Task<IWorkerModel> GetWorker(Guid id);
        Task<bool> AddWorker(IWorkerModel worker);
        Task<bool> UpdateWorker(Guid id, IWorkerModel worker);
        Task<bool> DeleteWorker(Guid id);
        Task<bool> SetJob(Guid workerId, Guid jobId);
    }
}
