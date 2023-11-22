import React from 'react';

const WeatherDetails = ({ selectedCity, allWeatherData }) => {
  return (
    <div>
      <h3>Weather Details</h3>
      {selectedCity && (
        <div>
          <h4>{selectedCity.cityName}</h4>
          {allWeatherData.length > 0 ? (
            <ul>
              {allWeatherData.map(weather => (
                <li key={weather.dataId}>
                  Temperature: {weather.temperature}, Description: {weather.description}, Date: {weather.date}
                </li>
              ))}
            </ul>
          ) : (
            <p>No weather data available for this city.</p>
          )}
        </div>
      )}
    </div>
  );
};

export default WeatherDetails;
