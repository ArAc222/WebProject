import React, { useState, useEffect } from 'react';
import CityList from './Components/CityList';
import WeatherDetails from './Components/WeatherDetails';
import axios from 'axios';
import './App.css';

function App() {
  const [searchTerm, setSearchTerm] = useState('');
  const [cities, setCities] = useState([]);
  const [selectedCity, setSelectedCity] = useState(null);
  const [allWeatherData, setAllWeatherData] = useState([]);

  useEffect(() => {
    // Fetch all cities on component mount
    axios.get('https://localhost:44357/api/city/all')
      .then(response => setCities(response.data))
      .catch(error => console.error('Error fetching cities:', error));
  }, []);

  const handleSearch = () => {
    if (searchTerm.trim() === '') {
      alert('Enter a city name.');
      return;
    }
  
    // Fetch selected city data
    axios.get(`https://localhost:44357/api/city/${searchTerm}`)
      .then(response => setSelectedCity(response.data))
      .catch(error => {
        console.error('Error fetching city details:', error);
        setSelectedCity(null);
      });
  
    // Fetch all weather data for the selected city
    axios.get(`https://localhost:44357/api/weatherdata/bycity/${searchTerm}`)
      .then(response => setAllWeatherData(response.data))
      .catch(error => console.error('Error fetching weather data:', error));
  };
  

  const handleShowAllCities = () => {
    console.log('Fetching all cities...');
    axios.get('https://localhost:44357/api/city/all')
      .then(response => {
        console.log('Cities fetched successfully:', response.data);
        setCities(response.data);
      })
      .catch(error => {
        console.error('Error fetching cities:', error);
      });
  
    // Reset selected city and weather data
    setSelectedCity(null);
    setAllWeatherData([]);
  };
  

  return (
    <div className="container mt-5">
      <h1 className="text-center mb-4">Weather Forecast</h1>

      {/* Search bar */}
      <div className="input-group mb-3">
        <input
          type="text"
          className="form-control"
          placeholder="Enter city name..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
        <div className="input-group-append">
          <button
            className="btn btn-primary"
            type="button"
            onClick={handleSearch}
          >
            Search
          </button>
        </div>
      </div>

      {/* Show all cities button */}
      <button
        className="btn btn-secondary mb-4"
        onClick={handleShowAllCities}
      >
        Show All Cities
      </button>

      {/* Render CityList and WeatherDetails components */}
      <div className="row">
        <div className="col-md-6">
          <CityList cities={cities} setSelectedCity={setSelectedCity} />
        </div>
        <div className="col-md-6">
          <WeatherDetails
            selectedCity={selectedCity}
            allWeatherData={allWeatherData}
          />
        </div>
      </div>
    </div>
  );
}

export default App;
