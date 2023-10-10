using System;
using System.Collections.Generic;
using Example.Repository.Common;
using Npgsql;
using Example.Model;

namespace Example.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        public List<WorkerModel> GetWorkers()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Worker\";", connection))
                    {
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
                                    Gender = reader.GetChar(3)
                                };
                                workersList.Add(worker);
                            }
                            return  workersList;
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

        // GET api/worker/5
        public WorkerModel GetWorker(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Worker\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                WorkerModel worker = new WorkerModel
                                {
                                    Id = reader.GetGuid(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Gender = reader.GetChar(3)
                                };
                                return  worker;
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

        // POST api/worker
        public bool Post( WorkerModel worker)
        {
            try
            {
                worker.Id = Guid.NewGuid();
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO \"Worker\" (\"Id\", \"FirstName\", \"LastName\", \"Gender\") VALUES (@WorkerId, @FirstName, @LastName, @Gender)", connection))
                    {
                        command.Parameters.AddWithValue("@WorkerId", worker.Id);
                        command.Parameters.AddWithValue("@FirstName", worker.FirstName);
                        command.Parameters.AddWithValue("@LastName", worker.LastName);
                        command.Parameters.AddWithValue("@Gender", worker.Gender);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
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

        // PUT api/worker/5
        public bool Put(Guid id, WorkerModel worker)
        {
            try
            {
                // Instanciram connection objekt unutar metode
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand getCommand = new NpgsqlCommand("SELECT * FROM \"Worker\" WHERE \"Id\" = @id", connection))
                    {
                        getCommand.Parameters.AddWithValue("@id", id);

                        using (NpgsqlDataReader reader = getCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Ažuriram promijenjena polja
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

                    using (NpgsqlCommand updateCommand = new NpgsqlCommand("UPDATE \"Worker\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"Gender\" = @Gender WHERE \"Id\" = @id", connection))
                    {
                        updateCommand.Parameters.AddWithValue("@FirstName", worker.FirstName);
                        updateCommand.Parameters.AddWithValue("@LastName", worker.LastName);
                        updateCommand.Parameters.AddWithValue("@Gender", worker.Gender);
                        updateCommand.Parameters.AddWithValue("@id", id);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

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

        // DELETE api/worker/5
        public bool Delete(Guid id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM \"Worker\" WHERE \"Id\" = @id", connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

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

        // PUT api/worker/setjob/5
        public bool SetJob(Guid workerId, Guid jobId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=student1;Database=workerDB"))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("UPDATE \"Worker\" SET \"JobId\" = @JobId WHERE \"Id\" = @WorkerId", connection))
                    {
                        command.Parameters.AddWithValue("@JobId", jobId);
                        command.Parameters.AddWithValue("@WorkerId", workerId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
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


    }
}
