import React, { useEffect, useState } from 'react';
import { Card } from 'antd';
import axios from 'axios';
import { useSelector } from 'react-redux/es/hooks/useSelector';
import { addCardData, addCityData, addCountryData, addWishlistData } from './AdvertisementSlice';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import DetailedCard from './DetailedCard';

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
  const [likedCards, setLikedCards] = useState(useSelector(state => state.advertisement.wishlistData));
  const [selectedCard, setSelectedCard] = useState(null);

  useEffect(() => {
    if(!user)
    {
      navigate("/login")
    }
  })

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

  useEffect(() => {
    dispatch(addWishlistData(likedCards));
  }, [likedCards, dispatch]);
  

  
  const toggleFavorite = (cardIndex) => {
    if (likedCards.includes(cardIndex)) {
      setLikedCards(likedCards.filter((index) => index !== cardIndex));
      dispatch(addWishlistData(likedCards))
    } else {
      setLikedCards([...likedCards, cardIndex]);
      dispatch(addWishlistData(likedCards))
    }
  };

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
                          src="./images/default/default2.jpeg"
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
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className={`h-6 w-6 ${likedCards.includes(index) ? 'fill-current' : 'stroke-current'} absolute top-2 right-2 text-red-500 cursor-pointer`}
                  viewBox="0 0 24 24"
                  onClick={() => toggleFavorite(index)}
                >
                  {likedCards.includes(index) ? (
                    <path
                      fill="currentColor"
                      d="M12 21l-1 1-1-1C4 15.5 0 12.5 0 8c0-3.5 2.5-6 6-6 2 0 4 1.5 6 3 2-1.5 4-3 6-3 3.5 0 6 2.5 6 6 0 4.5-4 7.5-11 13z"
                    />
                  ) : (
                    <path
                    fill="none"
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M20.84 7.5H20c-.84 0-1.38.57-1.68 1.12L12 18l-6.32-9.38C5.39 8.07 4.85 7.5 4 7.5H3.16A2.25 2.25 0 0 0 1 9.75V20a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V9.75a2.25 2.25 0 0 0-2.16-2.25z"
                    />
                  )}
                </svg>

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
