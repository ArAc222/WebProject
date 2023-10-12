using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Model;
using Example.Service;
using System.Threading.Tasks;
using Example.Service.Common;

namespace Example.WebApi.Controllers
{
    public class JobController : ApiController
    {
        private JobService _jobService;

        public JobController(JobService jobService)
        {
            _jobService = jobService;
        }

        // GET api/job
        public async Task<HttpResponseMessage> GetJobs()
        {
            var jobsList = await _jobService.GetJobsAsync();
            return Request.CreateResponse(HttpStatusCode.OK, jobsList);
        }

        // GET api/job/5
        public async Task<HttpResponseMessage> GetJob(Guid id)
        {
            var job = await _jobService.GetJobAsync(id);
            if (job != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, job);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
            }
        }

        // POST api/job
        public async Task<HttpResponseMessage> Post([FromBody] JobModel job)
        {
            if (await _jobService.AddJobAsync(job))
            {
                return Request.CreateResponse(HttpStatusCode.OK, job);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error inserting job");
            }
        }

        // PUT api/job/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] JobModel job)
        {
            if (await _jobService.UpdateJobAsync(id, job))
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
            }
        }

        // DELETE api/job/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            if (await _jobService.DeleteJobAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
            }
        }

        // GET api/job/workers/5
        [Route("api/job/workers/{jobId}")]
        public async Task<HttpResponseMessage> GetWorkersForJob(Guid jobId)
        {
            var workersList = await _jobService.GetWorkersForJobAsync(jobId);
            return Request.CreateResponse(HttpStatusCode.OK, workersList);
        }
    }
}
