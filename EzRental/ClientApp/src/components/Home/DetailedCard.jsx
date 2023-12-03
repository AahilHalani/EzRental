import { Modal, Button } from 'antd';
import { useSelector } from 'react-redux/es/hooks/useSelector';
import { useDispatch } from 'react-redux';
import { useEffect, useState } from 'react';
import { addWishlistData } from './AdvertisementSlice';
import axios from 'axios';
import Cookies from 'js-cookie';

export default function DetailedCard({ card, onClose, visible }) {
  const [likedCards, setLikedCards] = useState([]);
  const dispatch = useDispatch()
  const userId = Cookies.get('userId')
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];
  };

  const getWishList = async () => {
    const response = await axios.get(`http://localhost:44486/wishlist/${userId}`);
    setLikedCards(response.data)
  }

  useEffect(() => {
    getWishList()
  },[])

  const toggleFavorite = async (cardIndex) => {
    const data = {
      "userId": userId,
      "advertisementId": card.advertisement.adId
  }
  const isAdIdIncluded = likedCards.some(card => card.advertisement.adId === cardIndex);
    if (isAdIdIncluded) {
      const response = await axios.delete(`http://localhost:44486/wishlist`, {
        headers: {
          'Content-Type': 'application/json', // Adjust content type if necessary
        },
        data: data, // Check if 'data' needs stringifying (if not already a string)
      });
      getWishList()
    } else {
      const response = await axios.post(`http://localhost:44486/wishlist`,data);
      getWishList()
    }
  };

  const handleInquire = async () => {
    console.log(card)
    const response = await axios.post(`http://localhost:44486/advertisement/Inquire/${card.advertisement.adId}/${card.advertisement.rent.renterId}`)
    console.log(response)
  }
    

  return (
    <Modal
      visible={visible}
      onCancel={onClose}
      footer={null}
      centered
      destroyOnClose
      width={'80%'}
    >
      <div className="p-4 flex">
        <div className="flex-none w-1/2 pr-4">
          <img
            alt="property"
            src="./images/default/default.jpeg"
            className="w-full h-full rounded"
          />
        </div>
        <div className="w-1/2">
          <h2 className="text-2xl font-bold mb-2">
            {`${card.advertisement.area}, ${card.advertisement.city}`}
          </h2>
          <h3 className='text-2x1 font-bold mb-2'>Description</h3>
          <p className="mb-2">{card.advertisement.description}</p>
          <p className="mb-2">Price: {card.advertisement.price}</p>
          <h3 className='text-2x1 font-bold mb-2'>What you will get</h3>
          <div className="flex flex-wrap">
            <p className="w-1/3 mb-2">{card.advertisement.rent.room.type} Room</p>
            <p className="w-1/3 mb-2">{card.advertisement.rent.room.numBeds} Bedrooms</p>
            <p className="w-1/3 mb-2">{card.advertisement.rent.room.sqArea} Square Area</p>
            <p className="w-1/2 mb-2"><span className='font-bold'>Start Date:</span> {formatDate(card.advertisement.startDate)}</p>
            <p className="w-1/2 mb-2"><span className='font-bold'>End Date:</span> {formatDate(card.advertisement.endDate)}</p>
          </div>
          <h3 className='text-2x1 font-bold mb-2'>Renters Information</h3>
          <div className='flex flex-wrap mb-10'>
            <p className="w-full mb-2"><span className='font-bold'>Name: </span>{card.advertisement.rent.renter.firstName} {card.advertisement.rent.renter.lastName}</p>
            <p className="w-1/2 mb-2"><span className='font-bold'>Email: </span> {card.advertisement.rent.renter.email}</p>
            <p className="w-1/2 mb-2"><span className='font-bold'>Phone Number: </span> {card.advertisement.rent.renter.phoneNumber}</p>
          </div>
          <div className="mt-auto flex justify-end">
            <div className='flex mt-20'>
              <p className="mb-2 mt-2 mr-2"><span className='font-bold'>Price: </span> {card.advertisement.price}</p>
              <Button type="primary" className='bg-customGreen hover:bg-green-700 text-white font-bold rounded' onClick={() => toggleFavorite(card.advertisement.adId)}>{likedCards.some(cards => cards.advertisement.adId === card.advertisement.adId) ? "Remove from wishlist" : "Add to wishlist"}</Button>
              <Button type="primary" className='bg-customGreen hover:bg-green-700 text-white font-bold rounded ml-4' onClick={handleInquire}>Inquire</Button>
            </div>
          </div>
        </div>
      </div>
    </Modal>
  );
}
