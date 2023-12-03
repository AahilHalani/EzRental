import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import axios from "axios";
import { useDispatch } from "react-redux";
import { loginSuccess, userEdit } from "../Auth/LoginSlice";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";

export default function ProfilePage() {
  const user = useSelector(state => state.login.user)
  const userId = Cookies.get('userId')
  const [ email, setEmail ] = useState('')
  const [address, setAddress] = useState('')
  const [ firstName, setFirstName ] = useState('')
  const [ lastName, setLastName ] = useState('')
  const [phoneNumber, setPhoneNumber] = useState('')
  const dispatch = useDispatch()
  const navigate = useNavigate()


  useEffect(() => {
    const getAccess = async () => {
        const response = await axios.get('http://localhost:44486/session/verifysession',{
          withCredentials: true,
        })
        if(response.status === 200)
        {
          dispatch(loginSuccess(response.data.name))
          try
          {
            const response2 = await axios.get(`http://localhost:44486/users/${userId}`)
            setFirstName(response2.data.firstName)
            setLastName(response2.data.lastName)
            setEmail(response2.data.email)
            setAddress(response2.data.address)
            setPhoneNumber(response2.data.phoneNumber)
            console.log(response2)
          }
          catch(error)
          {
            console.log(error)
          }
        }
        else
        {
          navigate("/login")
        }
      }
    getAccess()
  }, [])



  const handleChanges = async(e) => {
    e.preventDefault()
    if(!firstName || !email || !lastName || !address || !phoneNumber)
    {
      console.log("Bad input")
      return
    }
    const updatedUser = {
      firstName: firstName,
      lastName: lastName,
      email: email,
      phoneNumber: phoneNumber,
      address: address,
      userId: userId
    }
    try
    {
      const response = await axios.put(`http://localhost:44486/users/${userId}`, updatedUser)
      alert("Changes successful")
    }
    catch(error)
    {
      console.log(error)
    }
  }


  return (
    <div className="bg-gray-200 min-h-screen flex flex-col items-center py-10">
      <div className="text-center mb-8">
        <h1 className="text-5xl font-semibold">Profile</h1>
      </div>
      <form 
        className="w-5/6 flex flex-col items-start"
        onSubmit={e => handleChanges(e)}>
        <div className="mb-4 w-full">
          <label className="text-gray-600 font-semibold mb-2 block">
            First Name
          </label>
          <input
            type="text"
            className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:border-indigo-500"
            placeholder= {firstName ? firstName : "First Name here"}
            value={firstName}
            onChange={e => setFirstName(e.target.value)}
          />
        </div>
        <div className="mb-4 w-full">
          <label className="text-gray-600 font-semibold mb-2 block">
            Last Name
          </label>
          <input
            type="text"
            className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:border-indigo-500"
            placeholder= {lastName ? lastName : "Last Name here"}
            value={lastName}
            onChange={e => setLastName(e.target.value)}
          />
        </div>
        <div className="mb-4 w-full">
          <label className="text-gray-600 font-semibold mb-2 block">
            Email
          </label>
          <input
            type="email"
            className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:border-indigo-500"
            placeholder={email}
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
        </div>
        <div className="mb-4 w-full">
          <label className="text-gray-600 font-semibold mb-2 block">
            Address
          </label>
          <input
            type="test"
            className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:border-indigo-500"
            placeholder={address}
            value={address}
            onChange={e => setAddress(e.target.value)}
          />
        </div>  
        <div className="mb-4 w-full">
          <label className="text-gray-600 font-semibold mb-2 block">
            Phone Number
          </label>
          <input
            type="text"
            className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:border-indigo-500"
            placeholder={phoneNumber}
            value={phoneNumber}
            onChange={e => setPhoneNumber(e.target.value)}
          />
        </div>
        {/* Edit Profile button */}
        <div className="text-center">
          <button
            className="bg-indigo-500 hover:bg-indigo-600 text-white py-2 px-6 rounded-lg font-semibold focus:outline-none"
            type="submit"
          >
            Edit Profile
          </button>
        </div>
      </form>
    </div>
  );
}