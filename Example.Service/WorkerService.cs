using System;
using System.Collections.Generic;
using Example.Model;
using Example.Repository;
using Example.Model.Common;

namespace Example.Service
{
    public class WorkerService
    {
        private  WorkerRepository _workerRepository;

        public WorkerService(WorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public List<WorkerModel> GetWorkers()
        {
            return _workerRepository.GetWorkers();
        }

        public WorkerModel GetWorker(Guid id)
        {
            return _workerRepository.GetWorker(id);
        }

        public bool AddWorker(WorkerModel worker)
        {
            return _workerRepository.Post(worker);
        }

        public bool UpdateWorker(Guid id, WorkerModel worker)
        {
            return _workerRepository.Put(id, worker);
        }

        public bool DeleteWorker(Guid id)
        {
            return _workerRepository.Delete(id);
        }

        public bool SetJob(Guid workerId, Guid jobId)
        {
            return _workerRepository.SetJob(workerId, jobId);
        }
    }
}
