using System;
using System.Collections.Generic;
using Example.Model;
using Example.Repository;

namespace Example.Service
    {
        public class JobService
        {
            private  JobbRepository _jobRepository;

            public JobService(JobbRepository jobRepository)
            {
                _jobRepository = jobRepository;
            }

            public List<JobModel> GetJobs()
            {
                return _jobRepository.GetJobs();
            }

            public JobModel GetJob(Guid id)
            {
                return _jobRepository.GetJob(id);
            }

            public bool AddJob(JobModel job)
            {
                return _jobRepository.Post(job);
            }

            public bool UpdateJob(Guid id, JobModel job)
            {
                return _jobRepository.Put(id, job);
            }

            public bool DeleteJob(Guid id)
            {
                return _jobRepository.Delete(id);
            }

            public List<WorkerModel> GetWorkersForJob(Guid jobId)
            {
                return _jobRepository.GetWorkersForJob(jobId);
            }
        }
    }
