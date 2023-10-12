using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Model.Common;

namespace Example.Repository.Common
{
    public interface IWorkerRepository
    {
        Task<List<IWorkerModel>> GetWorkersAsync();
        Task<IWorkerModel> GetWorkerAsync(Guid id);
        Task<bool> PostAsync(IWorkerModel worker);
        Task<bool> PutAsync(Guid id, IWorkerModel worker);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> SetJobAsync(Guid workerId, Guid jobId);
    }
}

