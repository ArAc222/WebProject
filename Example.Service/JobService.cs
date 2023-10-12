using System;
using System.Collections.Generic;
using Example.Model;
using Example.Repository;
using System.Threading.Tasks;

namespace Example.Service
{
    public class JobService
    {
        private JobbRepository _jobRepository;

        public JobService(JobbRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<JobModel>> GetJobsAsync()
        {
            return await _jobRepository.GetJobsAsync();
        }

        public async Task<JobModel> GetJobAsync(Guid id)
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

        public async Task<List<WorkerModel>> GetWorkersForJobAsync(Guid jobId)
        {
            return await _jobRepository.GetWorkersForJobAsync(jobId);
        }
    }
}
