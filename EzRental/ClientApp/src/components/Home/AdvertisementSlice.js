import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  cardData: [],
  cityData: [],
  countryData: [],
  wishlistData: []
}

const advertisementSlice = createSlice({
  name: 'advertisement',
  initialState,
  reducers: {
    addCardData: (state, action) => {
      state.cardData = action.payload
    },
    addCityData: (state, action) => {
      state.cityData = action.payload
    },
    addCountryData: (state, action) => {
      state.countryData = action.payload
    },
    addWishlistData: (state, action) => {
      state.wishlistData = action.payload
    }
  }
})


export const { addCardData, addCityData, addCountryData, addWishlistData } = advertisementSlice.actions;
export default advertisementSlice.reducer;