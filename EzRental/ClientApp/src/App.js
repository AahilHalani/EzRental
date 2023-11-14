import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import './custom.css';
import SignupView from './components/Auth/SignupView';


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
     <SignupView / >
    );
  }
}
