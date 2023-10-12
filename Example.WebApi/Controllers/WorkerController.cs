using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Model;
using Example.Service;
using System.Threading.Tasks;

namespace Example.WebApi.Controllers
{
    public class WorkerController : ApiController
    {
        private WorkerService _workerService;

        public WorkerController(WorkerService workerService)
        {
            _workerService = workerService;
        }

        // GET api/worker
        public async Task<HttpResponseMessage> GetWorkers()
        {
            var workersList = await _workerService.GetWorkersAsync();
            return Request.CreateResponse(HttpStatusCode.OK, workersList);
        }

        // GET api/worker/5
        public async Task<HttpResponseMessage> GetWorker(Guid id)
        {
            var worker = await _workerService.GetWorkerAsync(id);
            if (worker != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, worker);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Worker not found");
            }
        }

        // POST api/worker
        public async Task<HttpResponseMessage> Post([FromBody] WorkerModel worker)
        {
            if (await _workerService.AddWorkerAsync(worker))
            {
                return Request.CreateResponse(HttpStatusCode.OK, worker);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error inserting worker");
            }
        }

        // PUT api/worker/5
        public async Task<HttpResponseMessage> Put(Guid id, [FromBody] WorkerModel worker)
        {
            if (await _workerService.UpdateWorkerAsync(id, worker))
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Worker not found");
            }
        }

        // DELETE api/worker/5
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            if (await _workerService.DeleteWorkerAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Worker not found");
            }
        }

        // PUT api/worker/setjob/5
        [Route("api/worker/setjob/{workerId}")]
        public async Task<HttpResponseMessage> SetJob(Guid workerId, [FromBody] Guid jobId)
        {
            if (await _workerService.SetJobAsync(workerId, jobId))
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Worker not found");
            }
        }
    }
}
