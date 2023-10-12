using System;
using System.Collections.Generic;
using Example.Model;
using System.Threading.Tasks;
using Example.Service.Common;
using Example.Repository.Common;
using Example.Model.Common;

namespace Example.Service
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<IJobModel>> GetJobsAsync()
        {
            return await _jobRepository.GetJobsAsync();
        }

        public async Task<IJobModel> GetJobAsync(Guid id)
        {
            return await _jobRepository.GetJobAsync(id);
        }

        public async Task<bool> AddJobAsync(JobModel job)
        {
            return await _jobRepository.PostAsync(job);
        }

        public async Task<bool> UpdateJobAsync(Guid id, JobModel job)
        {
            return await _jobRepository.PutAsync(id, job);
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            return await _jobRepository.DeleteAsync(id);
        }

        public async Task<List<IWorkerModel>> GetWorkersForJobAsync(Guid jobId)
        {
            return await _jobRepository.GetWorkersForJobAsync(jobId);
        }
    }
}
