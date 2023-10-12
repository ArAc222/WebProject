using System;
using System.Collections.Generic;
using Example.Model;
using Example.Repository;
using System.Threading.Tasks;
using Example.Service.Common;
using Example.Model.Common;

namespace Example.Service
{
    public class WorkerService : IWorkerService
    {
        private WorkerRepository _workerRepository;

        public WorkerService(WorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<List<IWorkerModel>> GetWorkersAsync()
        {
            return await _workerRepository.GetWorkersAsync();
        }

        public async Task<IWorkerModel> GetWorkerAsync(Guid id)
        {
            return await _workerRepository.GetWorkerAsync(id);
        }

        public async Task<bool> AddWorkerAsync(WorkerModel worker)
        {
            return await _workerRepository.PostAsync(worker);
        }

        public async Task<bool> UpdateWorkerAsync(Guid id, WorkerModel worker)
        {
            return await _workerRepository.PutAsync(id, worker);
        }

        public async Task<bool> DeleteWorkerAsync(Guid id)
        {
            return await _workerRepository.DeleteAsync(id);
        }

        public async Task<bool> SetJobAsync(Guid workerId, Guid jobId)
        {
            return await _workerRepository.SetJobAsync(workerId, jobId);
        }

        public Task<List<IWorkerModel>> GetWorkers()
        {
            throw new NotImplementedException();
        }

        public Task<IWorkerModel> GetWorker(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddWorker(IWorkerModel worker)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWorker(Guid id, IWorkerModel worker)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWorker(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetJob(Guid workerId, Guid jobId)
        {
            throw new NotImplementedException();
        }
    }
}
