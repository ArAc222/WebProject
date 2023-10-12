using System;
using System.Collections.Generic;
using Example.Repository.Common;
using Npgsql;
using Example.Model;
using Example.Model.Common;
using System.Threading.Tasks;

namespace Example.Repository
{
    public class JobRepository : IJobRepository
    {
        public async Task<List<JobModel>> GetJobsAsync()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\";", connection))
                    {
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<JobModel> jobsList = new List<JobModel>();
                            while (await reader.ReadAsync())
                            {
                                JobModel job = new JobModel
                                {
                                    Id = reader.GetGuid(0),
                                    Salary = reader.GetInt32(1),
                                    Type = reader.GetString(2),
                                };
                                jobsList.Add(job);
                            }
                            return jobsList;
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

        public async Task<JobModel> GetJobAsync(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                JobModel job = new JobModel
                                {
                                    Id = reader.GetGuid(0),
                                    Salary = reader.GetInt32(1),
                                    Type = reader.GetString(2),
                                };
                                return job;
                            }
                            else
                            {
                                return null;
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

        public async Task<bool> PostAsync(JobModel job)
        {
            try
            {
                job.Id = Guid.NewGuid();
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Job\" (\"Id\", \"Salary\", \"Type\") VALUES (@Id, @Salary, @Type)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", job.Id);
                        command.Parameters.AddWithValue("@Salary", job.Salary);
                        command.Parameters.AddWithValue("@Type", job.Type);

                        int result = await command.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            return true;
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

        public async Task<bool> PutAsync(Guid id, JobModel job)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand getCommand = new NpgsqlCommand("SELECT * FROM \"Job\" WHERE \"Id\" = @id", connection))
                    {
                        getCommand.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = await getCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                job.Salary = reader.GetInt32(1);
                                job.Type = reader.GetString(2);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    using (NpgsqlCommand updateCommand = new NpgsqlCommand("UPDATE \"Job\" SET \"Salary\" = @Salary, \"Type\" = @Type WHERE \"Id\" = @id", connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Salary", job.Salary);
                        updateCommand.Parameters.AddWithValue("@Type", job.Type);
                        updateCommand.Parameters.AddWithValue("@id", id);

                        int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Job\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
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


        public async Task<List<WorkerModel>> GetWorkersForJobAsync(Guid jobId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Worker\" WHERE \"JobId\" = @JobId", connection))
                    {
                        command.Parameters.AddWithValue("@JobId", jobId);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<WorkerModel> workersList = new List<WorkerModel>();
                            while (await reader.ReadAsync())
                            {
                                WorkerModel worker = new WorkerModel
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetChar(3),
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
