import React, { useEffect, useState } from 'react';
import { Card } from 'antd';
import axios from 'axios';
import { useSelector } from 'react-redux/es/hooks/useSelector';
import { addCardData, addCityData, addCountryData, addWishlistData } from './AdvertisementSlice';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import DetailedCard from './DetailedCard';
import Cookies from "js-cookie"
import { loginSuccess } from '../Auth/LoginSlice';


export default function HomePage() {
  const [selectedCity, setSelectedCity] = useState('default_city');
  const [filteredCards, setFilteredCards] = useState([]);
  const { cardData, countryData, cityData } = useSelector(state => ({
    cardData: state.advertisement.cardData,
    countryData: state.advertisement.countryData,
    cityData: state.advertisement.cityData,
  }));
  const user = useSelector(state => state.login.user)
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [selectedCard, setSelectedCard] = useState(null);

  useEffect(() => {
    const fetchAdvertisements = async () => {
      const response = await axios.get('http://localhost:44486/advertisement/');
      dispatch(addCardData(response.data));
    };
    const sessionCheck = async () => {
      try
      {
        const response = await axios.get('http://localhost:44486/session/verifysession',{
          withCredentials: true,
        })
        if(response.status === 200)
        {
          console.log(response.data)
          dispatch(loginSuccess(response.data.name))
        }
        else
        {
          navigate("/login")
        }
      }
      catch(error)
      {
        navigate('/login')
      }
    }
    sessionCheck()
    fetchAdvertisements()
  },[])

  useEffect(() => {
    if (cardData && cardData.length > 0) {
      const citiesSet = new Set();
      const countriesSet = new Set();
      cardData.forEach(card => {
        citiesSet.add(card.city);
        countriesSet.add(card.country)
      });
      dispatch(addCityData(Array.from(citiesSet)));
      dispatch(addCountryData(Array.from(countriesSet)))
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
  const CardClickHandler = async (adId) => {
    try {
      const response = await axios.get(`http://localhost:44486/advertisement/${adId}`);
      console.log(response.data)
      setSelectedCard(response.data);
    } catch (error) {
      console.error('Error fetching card data:', error);
    }
  };


  return (
    <div className="p-4">
      <h1 className="text-3xl font-bold mb-4">Popular Cities Across The Globe</h1>
      <div className="flex flex-wrap gap-4">
        {countryData.map((country, index) => (
          <button key={index} className="bg-customGreen hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
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
                          src="./images/default/karachi.jpg"
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
            className="bg-customGreen hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
          >
            {city}
          </button>
        ))}
      </div>

      <div className="flex flex-wrap gap-12 mt-8">
        {filteredCards.map((card, index) => (
          <div key={index} className="max-w-xs">
            <Card
              onClick={() => CardClickHandler(card.adId)}
              hoverable
              style={{ width: 300 }}
              cover={
              <div key={index} className="relative">
                <img
                  alt="property"
                  src="./images/default/default.jpeg"
                  className="w-full h-48 object-cover rounded-t"
                />
              </div>}
            >
              <Card.Meta title={card.area} description={`${card.area}, ${card.city}`} />
              <p>From {card.price}</p>
            </Card>
          </div>
        ))}
      </div>
      {selectedCard && 
        <DetailedCard
          card={selectedCard}
          onClose={() => setSelectedCard(null)}
          visible={!!selectedCard}
        />
      }
    </div>
  );
}
