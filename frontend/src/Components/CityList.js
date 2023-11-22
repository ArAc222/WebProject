import React from 'react';

const CityList = ({ cities }) => {
  return (
    <div>
      <h3>City List</h3>
      <ul className="list-group">
        {cities.map(city => (
          <li key={city.cityId} className="list-group-item">
            {city.cityName}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CityList;
