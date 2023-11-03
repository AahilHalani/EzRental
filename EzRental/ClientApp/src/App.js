import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import './custom.css';
import SignupView from './components/Auth/SignupView';
import LoginView from './components/Auth/LoginView';


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
    <LoginView / >
    );
  }
}
