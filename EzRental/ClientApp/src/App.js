import React, { Component } from 'react';
import './custom.css';
import SignupView from './components/Auth/SignupView';
import LoginView from './components/Auth/LoginView';
import { Routes, Route } from 'react-router-dom';
import HomePage from './components/Home/HomePage';
import Navbar from './Navbar';
import WishlistView from './components/Wishlist/Wishlist';
import ProfilePage from './components/Profile/ProfileView';


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
    <div>
      <Navbar />
      <Routes>
        <Route path='/' element={<HomePage />} />
        <Route path='login' element={<LoginView />} />
        <Route path='signup' element={<SignupView />} />
        <Route path='home' element={<HomePage />} />
        <Route path='wishlist' element={<WishlistView />} />
        <Route path='profile' element={<ProfilePage />}></Route>
      </Routes>
    </div>
    );
  }
}
