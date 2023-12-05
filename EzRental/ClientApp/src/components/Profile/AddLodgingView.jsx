import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie"

const AddLodgingView = () => {

    const navigate = useNavigate()
    const renterId = Cookies.get("userId")

    const [lodgingInfo, setLodgingInfo] = useState({
    type: '',
    numberOfBeds: 0,
    squareArea: '',
    renterId: '',
    price: '',
    description: '',
    startDate: '',
    endDate: '',
    area: '',
    city: '',
    country: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setLodgingInfo({ ...lodgingInfo, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

      const formData = {
              "advertisement": {
                  "rent": {
                      "room": {
                          "type": lodgingInfo.type,
                          "numBeds": lodgingInfo.numberOfBeds,
                          "sqArea": lodgingInfo.squareArea,
                          "picturePath": "http://dummyimage.com/242x110.png/5fa2dd/ffffff"
                      },
                      "renterId": renterId
                  },
                  "price": lodgingInfo.price,
                  "description": lodgingInfo.description,
                  "startDate": lodgingInfo.startDate,
                  "endDate": lodgingInfo.endDate,
                  "area": lodgingInfo.area,
                  "city": lodgingInfo.city,
                  "country": lodgingInfo.country
              },
              "facilties": [{
                "facilityId":1,
                "facilityName":"Temp"
              }]       
      }

     const response = await axios.post(`http://localhost:44486/advertisement`,formData)
     alert(response.data.message)
     navigate("/")
  };

  return (
    <div className="bg-gray-200 min-h-screen p-4">
      <h2 className="text-2xl font-bold mb-4">Add Lodging</h2>
      <form onSubmit={handleSubmit} className="flex space-x-4">
        <div className="flex-1 space-y-2">
          <h3 className="text-lg font-semibold">Room</h3>
          <label className="flex flex-col">
            Type:
            <input type="text" name="type" value={lodgingInfo.type} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            Number of Beds:
            <input type="number" name="numberOfBeds" value={lodgingInfo.numberOfBeds} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            Square Area:
            <input type="text" name="squareArea" value={lodgingInfo.squareArea} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
        </div>
        <div className="flex-1 space-y-2">
          <h3 className="text-lg font-semibold">Additional Details</h3>
          <label className="flex flex-col">
            Price:
            <input type="text" name="price" value={lodgingInfo.price} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            Description:
            <textarea name="description" value={lodgingInfo.description} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md"></textarea>
          </label>
          <label className="flex flex-col">
            Start Date:
            <input type="date" name="startDate" value={lodgingInfo.startDate} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            End Date:
            <input type="date" name="endDate" value={lodgingInfo.endDate} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            Area:
            <input type="text" name="area" value={lodgingInfo.area} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            City:
            <input type="text" name="city" value={lodgingInfo.city} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
          <label className="flex flex-col">
            Country:
            <input type="text" name="country" value={lodgingInfo.country} onChange={handleInputChange} className="border border-gray-300 p-2 rounded-md" />
          </label>
        </div>
        <div className="flex flex-col justify-end">
          <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600 transition duration-300 self-start">Submit</button>
        </div>
      </form>
    </div>
  );
};

export default AddLodgingView;
