using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Model;
using Npgsql;

namespace Example.Repository
{
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=student1;Database=postgres;";


        public List<WeatherDataModel> GetAllWeatherDataByCityName(string cityName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM \"WeatherData\" WHERE \"CityId\" = (SELECT \"CityId\" FROM \"Cities\" WHERE \"CityName\" = @CityName)", connection))
                {
                    command.Parameters.AddWithValue("@CityName", cityName);

                    using (var reader = command.ExecuteReader())
                    {
                        List<WeatherDataModel> weatherDataList = new List<WeatherDataModel>();

                        while (reader.Read())
                        {
                            WeatherDataModel weatherData = new WeatherDataModel
                            {
                                DataId = reader.GetInt32(0),
                                CityId = reader.GetInt32(1),
                                Temperature = reader.GetInt32(2),
                                Description = reader.GetString(3),
                                Date = reader.GetDateTime(4)
                            };
                            weatherDataList.Add(weatherData);
                        }

                        return weatherDataList;
                    }
                }
            }
        }

        public void AddWeatherData(WeatherDataModel weatherData)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("INSERT INTO \"WeatherData\" (\"CityId\", \"Temperature\", \"Description\", \"Date\") VALUES (@CityId, @Temperature, @Description, @Date)", connection))
                {
                    command.Parameters.AddWithValue("@CityId", weatherData.CityId);
                    command.Parameters.AddWithValue("@Temperature", weatherData.Temperature);
                    command.Parameters.AddWithValue("@Description", weatherData.Description);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWeatherData(string cityName, WeatherDataModel updatedData)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("UPDATE \"WeatherData\" SET \"Temperature\" = @Temperature, \"Description\" = @Description, \"Date\" = @Date WHERE \"CityId\" = (SELECT \"CityId\" FROM \"Cities\" WHERE \"CityName\" = @CityName)", connection))
                {
                    command.Parameters.AddWithValue("@CityName", cityName);
                    command.Parameters.AddWithValue("@Temperature", updatedData.Temperature);
                    command.Parameters.AddWithValue("@Description", updatedData.Description);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCityAndWeatherData(string cityName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("DELETE FROM \"WeatherData\" WHERE \"CityId\" = (SELECT \"CityId\" FROM \"Cities\" WHERE \"CityName\" = @CityName); DELETE FROM \"Cities\" WHERE \"CityName\" = @CityName", connection))
                {
                    command.Parameters.AddWithValue("@CityName", cityName);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
