using System.Collections.Generic;
using System.Linq;
using Example.Model;
using Example.Model.Common;
using Example.Repository.Common;
using Npgsql;

namespace Example.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=student1;Database=postgres;";

        public List<CityModel> GetAllCities()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM \"Cities\"", connection))
                {
                    var cities = new List<CityModel>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CityModel city = new CityModel
                            {
                                CityId = reader.GetInt32(0),
                                CityName = reader.GetString(1)
                            };

                            cities.Add(city);
                        }
                    }

                    return cities;
                }
            }
        }
    }
}
