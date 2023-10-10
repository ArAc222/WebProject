using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Model;
using Example.Repository;

namespace Example.WebApi.Controllers
{
    public class WorkerController : ApiController
    {
        private  WorkerRepository _workerRepository = new WorkerRepository();

        // GET api/worker
        public HttpResponseMessage GetWorkers()
        {
            List<WorkerModel> workersList = _workerRepository.GetWorkers();
            return Request.CreateResponse(HttpStatusCode.OK, workersList);
        }

        // GET api/worker/5
        public HttpResponseMessage GetWorker(Guid id)
        {
            WorkerModel worker = _workerRepository.GetWorker(id);
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
        public HttpResponseMessage Post([FromBody] WorkerModel worker)
        {
            if (_workerRepository.Post(worker))
            {
                return Request.CreateResponse(HttpStatusCode.OK, worker);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error inserting worker");
            }
        }

        // PUT api/worker/5
        public HttpResponseMessage Put(Guid id, [FromBody] WorkerModel worker)
        {
            if (_workerRepository.Put(id, worker))
            {
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Worker not found");
            }
        }

        // DELETE api/worker/5
        public HttpResponseMessage Delete(Guid id)
        {
            if (_workerRepository.Delete(id))
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
        public HttpResponseMessage SetJob(Guid workerId, [FromBody] Guid jobId)
        {
            if (_workerRepository.SetJob(workerId, jobId))
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
