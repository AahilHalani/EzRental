import React, { useEffect, useState } from 'react';
import { Button, Card } from 'antd';
import axios from 'axios';
import Cookies from 'js-cookie'
import { useSelector } from 'react-redux/es/hooks/useSelector';
import { addCardData, addCityData, addCountryData } from './AdvertisementSlice';
import { useDispatch } from 'react-redux';

export default function HomePage() {
  const [selectedCity, setSelectedCity] = useState('default_city');
  const [filteredCards, setFilteredCards] = useState([]);
  const { cardData, countryData, cityData } = useSelector(state => ({
    cardData: state.advertisement.cardData,
    countryData: state.advertisement.countryData,
    cityData: state.advertisement.cityData,
  }));
  const dispatch = useDispatch();

  useEffect(() => {
    const fetchAdvertisements = async () => {
      const response = await axios.get('http://localhost:44486/advertisement/');
      dispatch(addCardData(response.data));
    };
    fetchAdvertisements();
  }, []);

  useEffect(() => {
    if (cardData && cardData.length > 0) {
      const citiesSet = new Set();
      cardData.forEach(card => {
        citiesSet.add(card.city);
      });
      dispatch(addCityData(Array.from(citiesSet)));
    }
  }, [cardData]);

  useEffect(() => {
    if (selectedCity) {
      const filtered = cardData.filter(card => card.city === selectedCity);
      setFilteredCards(filtered);
    } else {
      setFilteredCards(cardData);
    }
  }, [selectedCity, cardData]);

  return (
    <div className="p-4">
      <h1 className="text-3xl font-bold mb-4">Popular Cities Across The Globe</h1>
      <div className="flex flex-wrap gap-4">
        {countryData.map((country, index) => (
          <button key={index} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
            {country}
          </button>
        ))}
      </div>

      <div className="flex flex-wrap gap-16 mt-8">
        {cityData.map((card, index) => (
          <div key={index} className="max-w-xs">
            <Card
              bodyStyle={{padding: "0"}}
              style={{ width: 300 }}
              cover={
                <div key={index} className="max-w-xs relative rounded overflow-hidden">
                  <img
                    alt="property"
                    src="http://dummyimage.com/242x110.png/5fa2dd/ffffff"
                    className="w-full h-48 object-cover"
                  />
                  <div className="absolute bottom-2 left-7 w-4/5 bg-black bg-opacity-50 text-white p-2 rounded">
                    <p className='text-center'>{card}</p>
                  </div>
                </div>
              }
            />
          </div>
        ))}
      </div>

      <h1 className="text-3xl font-bold mt-8">Popular Properties across the globe</h1>
      <div className="flex flex-wrap gap-4 mt-4">
        {cityData.map((city, index) => (
          <button
            key={index}
            onClick={() => setSelectedCity(city)}
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            {city}
          </button>
        ))}
      </div>

      <div className="flex flex-wrap gap-12 mt-8">
        {filteredCards.map((card, index) => (
          <div key={index} className="max-w-xs">
            <Card
              hoverable
              style={{ width: 300 }}
              cover={<img alt="property" src="http://dummyimage.com/242x110.png/5fa2dd/ffffff" />}
            >
              <Card.Meta title={card.area} description={`${card.area}, ${card.city}`} />
              <p>From {card.price}</p>
            </Card>
          </div>
        ))}
      </div>
    </div>
  );
}
