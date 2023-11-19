import React, { useEffect, useState } from 'react';
import { Card } from 'antd';
import axios from 'axios';
import Cookies from 'js-cookie'
// const cardData = [
//   { title: 'Card 1', content: 'Content for Card 1' },
//   { title: 'Card 2', content: 'Content for Card 2' },
//   // Add more data as needed
// ];


export default function HomePage() {
  const [ cardData, setCardData] = useState([]);
  const accessToken = Cookies.get('userId')
  useEffect(() => {
    const fetchAdvertisements = async () => {
      const response =  await axios.get('http://localhost:44486/advertisement/')
      setCardData(response.data);
    }
    fetchAdvertisements()
  },[])
  return (
    <div style={{ display: 'flex', flexWrap: 'wrap', gap: '16px' }}>
      {cardData.map((card, index) => (
        <Card key={index} style={{ width: 300 }}>
          <img src="http://dummyimage.com/242x110.png/5fa2dd/ffffff" alt="nice" />
          <h1>{card.area}</h1>
          <p>{card.area}, {card.city}</p>
          <p>From {card.price}</p>
        </Card>
      ))}
    </div>
  );
};
