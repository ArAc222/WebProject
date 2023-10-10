using System;
using System.Collections.Generic;
using Example.Repository.Common;
using Npgsql;
using Example.Model;

namespace Example.Repository
{
    public class JobbRepository : IJobRepository
    {
        public List<JobModel> GetJobs()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\";", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<JobModel> jobsList = new List<JobModel>();
                            while (reader.Read())
                            {
                                JobModel job = new JobModel
                                {
                                    Id = reader.GetGuid(0),
                                    Salary = Convert.ToInt32(reader["Salary"]),
                                    Type = reader.GetString(1),
                                };
                                jobsList.Add(job);
                            }
                            return  null;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return null;
            }
        }

        // GET api/job/5
        public JobModel GetJob(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                JobModel job = new JobModel
                                {
                                    Id = reader.GetGuid(0),
                                    Salary = Convert.ToInt32(reader["Salary"]),
                                    Type = reader.GetString(1),
                                };
                                return  job;
                            }
                            else
                            {
                                return  null;
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                var errorMessage = "Error";

                var customException = new Exception(errorMessage, ex);

                throw customException;
            }
        }

        // POST api/job
        public bool Post(JobModel job)
        {
            try
            {
                job.Id = Guid.NewGuid();
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Job\" (\"Id\", \"Name\", \"Salary\", \"Type\") VALUES (@Id, @Name, @Salary, @Type)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", job.Id);
                        command.Parameters.AddWithValue("@Salary", job.Salary);
                        command.Parameters.AddWithValue("@Type", job.Type);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return  true;
                        }
                        else
                        {
                            return  false;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                var errorMessage = "Error";

                var customException = new Exception(errorMessage, ex);

                throw customException;
            }
        }

        // PUT api/job/5
        public bool Put(Guid id, JobModel job)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand getCommand = new NpgsqlCommand("SELECT * FROM \"Job\" WHERE \"Id\" = @id", connection))
                    {
                        getCommand.Parameters.AddWithValue("@id", id);

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
                                return  true;
                            }
                        }
                    }

                    using (NpgsqlCommand updateCommand = new NpgsqlCommand("UPDATE \"Job\" SET \"Name\" = @Name, \"Salary\" = @Salary, \"Type\" = @Type WHERE \"Id\" = @id", connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Salary", job.Salary);
                        updateCommand.Parameters.AddWithValue("@Type", job.Type);
                        updateCommand.Parameters.AddWithValue("@id", id);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return  true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                var errorMessage = "Error";

                var customException = new Exception(errorMessage, ex);

                throw customException;
            }
        }

        // DELETE api/job/5
        public bool Delete(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Job\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return  false;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                var errorMessage = "Error";

                var customException = new Exception(errorMessage, ex);

                throw customException;
            }

        }

        // GET api/job/workers/5
        public List<WorkerModel> GetWorkersForJob(Guid jobId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Worker\" WHERE \"JobId\" = @JobId", connection))
                    {
                        command.Parameters.AddWithValue("@JobId", jobId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<WorkerModel> workersList = new List<WorkerModel>();
                            while (reader.Read())
                            {
                                Worker worker = new Worker
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetChar(3),
                                    JobId = reader.GetGuid(4) // Dodajte JobId za povezivanje s poslom
                                };
                                workersList.Add(worker);
                            }
                            return workersList;
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                var errorMessage = "Error";

                var customException = new Exception(errorMessage, ex);

                throw customException;
            }


        }
    }
}
