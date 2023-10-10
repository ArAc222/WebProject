using Npgsql;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.Model;

namespace Example.WebApi.Controllers
{
    public class JobController : ApiController
    {
        private NpgsqlConnection connection;

        public JobController()
        {

        }

        // GET api/job
        public HttpResponseMessage GetJobs()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"JobModel\";", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<JobModel> jobsList = new List<JobModel>();
                            while (reader.Read())
                            {
                                JobModel job = new JobModel
                                {
                                    Id = reader.GetGuid(0),
                                    Salary = reader.GetInt32(2),
                                    Type = reader.GetString(3)
                                };
                                jobsList.Add(job);
                            }
                            return Request.CreateResponse(HttpStatusCode.OK, jobsList);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/job/5
        public HttpResponseMessage GetJob(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"JobModel\" WHERE \"Id\" = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                JobModel job = new JobModel
                                {
                                    Id = reader.GetGuid(0),
                                    Salary = reader.GetInt32(2),
                                    Type = reader.GetString(3)
                                };
                                return Request.CreateResponse(HttpStatusCode.OK, job);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/job
        public HttpResponseMessage Post([FromBody] JobModel job)
        {
            try
            {
                job.Id = Guid.NewGuid();
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Job\" (\"Id\", \"Salary\", \"Type\") VALUES (@Id, @Name, @Salary, @Type)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", job.Id);
                        command.Parameters.AddWithValue("@Salary", job.Salary);
                        command.Parameters.AddWithValue("@Type", job.Type);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, job);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, "Error inserting job");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/job/5
        public HttpResponseMessage Put(Guid id, [FromBody] JobModel job)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand getCommand = new NpgsqlCommand("SELECT * FROM \"JobModel\" WHERE \"Id\" = @Id", connection))
                    {
                        getCommand.Parameters.AddWithValue("@Id", id);

                        using (NpgsqlDataReader reader = getCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Ažuriram promijenjena polja
                                job.Salary = reader.GetInt32(2);
                                job.Type = reader.GetString(3);
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                            }
                        }
                    }

                    using (NpgsqlCommand updateCommand = new NpgsqlCommand("UPDATE \"JobModel\" SET \"Name\" = @Name, \"Salary\" = @Salary, \"Type\" = @Type WHERE \"Id\" = @Id", connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Id", job.Id);
                        updateCommand.Parameters.AddWithValue("@Salary", job.Salary);
                        updateCommand.Parameters.AddWithValue("@Type", job.Type);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/job/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"JobModel\" WHERE \"Id\" = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, true);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Job not found");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/job/workers/5
        [Route("api/job/workers/{jobId}")]
        public HttpResponseMessage GetWorkersForJob(Guid jobId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"WorkerModel\" WHERE \"JobId\" = @JobId", connection))
                    {
                        command.Parameters.AddWithValue("@JobId", jobId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<WorkerModel> workersList = new List<WorkerModel>();
                            while (reader.Read())
                            {
                                WorkerModel worker = new WorkerModel
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetChar(3),
                                    JobId = reader.GetGuid(4)
                                };
                                workersList.Add(worker);
                            }
                            return Request.CreateResponse(HttpStatusCode.OK, workersList);
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}