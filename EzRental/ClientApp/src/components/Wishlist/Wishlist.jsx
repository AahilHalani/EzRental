import React, { useEffect, useState } from 'react'
import axios from 'axios';
import Cookies from 'js-cookie';
import Card from 'antd/es/card/Card';
import { useSelector } from 'react-redux';
import DetailedCard from '../Home/DetailedCard';

export default function WishlistView() {
    const [likedCards, setLikedCards] = useState([])
    const userId = Cookies.get('userId')
    const user = useSelector(state => state.login.user)
    const [selectedCard, setSelectedCard] = useState()

    const getWishList = async () => {
        const response = await axios.get(`http://localhost:44486/wishlist/${userId}`);
        console.log(response.data)
        setLikedCards(response.data)
      }
    useEffect(() => {
        getWishList()
    },[])


    const CardClickHandler = async (adId) => {
        try {
          const response = await axios.get(`http://localhost:44486/advertisement/${adId}`);
          setSelectedCard(response.data);
        } catch (error) {
          console.error('Error fetching card data:', error);
        }
      };

  return (
    <div className='p-4'>
        <h1 className="text-3xl font-bold mb-4">{user}'s Wishlist</h1>
        <div className="flex flex-wrap gap-12 mt-8">
            {likedCards.map((card, index) => (
            <div key={index} className="max-w-xs">
            <Card
                onClick={() => CardClickHandler(card.advertisement.adId)}
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
                <Card.Meta title={card.advertisement.area} description={`${card.advertisement.area}, ${card.advertisement.city}`} />
                <p>From {card.advertisement.price}</p>
            </Card>
            </div>
        ))}
        </div>
        {selectedCard && 
        <DetailedCard
          card={selectedCard}
          onClose={() => {setSelectedCard(null)
        getWishList()}}
          visible={!!selectedCard}
        />
      }
    </div>
  )
}
