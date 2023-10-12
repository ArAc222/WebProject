using System;
using System.Collections.Generic;
using Example.Repository.Common;
using Npgsql;
using Example.Model;
using Example.Model.Common;
using System.Threading.Tasks;

namespace Example.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        public async Task<List<IWorkerModel>> GetWorkersAsync()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Worker\";", connection))
                    {
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<IWorkerModel> workersList = new List<IWorkerModel>();
                            while (await reader.ReadAsync())
                            {
                                WorkerModel worker = new WorkerModel
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetChar(3)
                                };

                                // Dodajte povezani posao za radnika
                                Guid jobId = reader.GetGuid(4);
                                IJobModel job = await GetJobAsync(jobId);
                                worker.Job = job;

                                workersList.Add((IWorkerModel)worker);
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

        public async Task<IWorkerModel> GetWorkerAsync(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Worker\" WHERE \"Id\" = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                IWorkerModel worker = new WorkerModel()
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetChar(3)
                                };

                                // Dodajte povezani posao za radnika
                                Guid jobId = reader.GetGuid(4);
                                IJobModel job = await GetJobAsync(jobId);
                                worker.Job = job;

                                return worker;
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

        public async Task<bool> PostAsync(IWorkerModel worker)
        {
            try
            {
                worker.Id = Guid.NewGuid();
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Worker\" (\"Id\", \"FirstName\", \"LastName\", \"Gender\", \"JobId\") VALUES (@WorkerId, @FirstName, @LastName, @Gender, @JobId)", connection))
                    {
                        command.Parameters.AddWithValue("@WorkerId", worker.Id);
                        command.Parameters.AddWithValue("@FirstName", worker.FirstName);
                        command.Parameters.AddWithValue("@LastName", worker.LastName);
                        command.Parameters.AddWithValue("@Gender", worker.Gender);
                        command.Parameters.AddWithValue("@JobId", worker.Job.Id);

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

        public async Task<bool> PutAsync(Guid id, IWorkerModel worker)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand getCommand = new NpgsqlCommand("SELECT * FROM \"Worker\" WHERE \"Id\" = @Id", connection))
                    {
                        getCommand.Parameters.AddWithValue("@Id", id);

                        using (NpgsqlDataReader reader = await getCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                worker.FirstName = reader.GetString(1);
                                worker.LastName = reader.GetString(2);
                                worker.Gender = reader.GetChar(3);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    using (NpgsqlCommand updateCommand = new NpgsqlCommand("UPDATE \"Worker\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Gender\" = @Gender, \"JobId\" = @JobId WHERE \"Id\" = @Id", connection))
                    {
                        updateCommand.Parameters.AddWithValue("@FirstName", worker.FirstName);
                        updateCommand.Parameters.AddWithValue("@LastName", worker.LastName);
                        updateCommand.Parameters.AddWithValue("@Gender", worker.Gender);
                        updateCommand.Parameters.AddWithValue("@JobId", worker.Job.Id);
                        updateCommand.Parameters.AddWithValue("@Id", id);
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

                    using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Worker\" WHERE \"Id\" = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

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

        public async Task<bool> SetJobAsync(Guid workerId, Guid jobId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand("UPDATE \"Worker\" SET \"JobId\" = @JobId WHERE \"Id\" = @WorkerId", connection))
                    {
                        command.Parameters.AddWithValue("@JobId", jobId);
                        command.Parameters.AddWithValue("@WorkerId", workerId);

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

        // Dodajte funkciju za dohvaćanje posla
        private async Task<IJobModel> GetJobAsync(Guid jobId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
            {
                await connection.OpenAsync();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Job\" WHERE \"Id\" = @JobId", connection))
                {
                    command.Parameters.AddWithValue("@JobId", jobId);

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            IJobModel job = new JobModel()
                            {
                                Id = reader.GetGuid(0),
                                Salary = reader.GetInt32(1),
                                Type = reader.GetString(2)
                            };
                            return job;
                        }
                    }
                }
            }
            return null;
        }
    }
}