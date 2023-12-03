import React from "react";
import { NavLink, useNavigate, useLocation } from 'react-router-dom';
import { useSelector } from "react-redux/es/hooks/useSelector";
import { loginFail } from "./components/Auth/LoginSlice";
import { useDispatch } from "react-redux";
import axios from "axios";
import { Menu, Dropdown } from "antd";
import { Space } from "antd";
import { DownOutlined } from "@ant-design/icons";

const items = [
  {
    label: (
      <NavLink
        to={"/profile"}
        className="text-black hover:text-gray-300"
      >
        Account Settings
      </NavLink>
    ),
    key: '1',
  },
  {
    label: (
      <NavLink
        to={"/add-lodging"}
        className="text-black hover:text-gray-300"
      >
        Add Lodging
      </NavLink>
    ),
    key: '2',
  },
];

export default function Navbar() {
  const isLoggedIn = useSelector(state => state.login.isLoggedIn)
  const user = useSelector(state => state.login.user)
  const dispatch = useDispatch()
  const navigate = useNavigate()
  const location = useLocation()

  const handleSignOut = async () => {
    const response = await axios.get('http://localhost:44486/session/Logout', user, {
        withCredentials: true,
      })
    dispatch(loginFail())
    navigate("/login")
  }


  return (
    <div className="bg-customGreen p-4 w-full flex justify-between items-center">
      <ul className="flex justify-center items-center">
        {/* <img src="logo/ezrental_logo.png" alt="Your Image" className="h-20 w-20" /> */}
        <li className="mr-4">
          <Dropdown
            menu={{
              items
            }}
          >
              <Space className="text-black hover:text-gray-300">
                Profile
                <DownOutlined />
              </Space>
          </Dropdown>
        </li>
        <li className="mr-4">
          <NavLink
            to={"/"}
            className={
              location.pathname === "/expenses"
                ? "text-yellow-400"
                : "text-black hover:text-gray-300"
            }
          >
            Home
          </NavLink>
        </li>
        <li className="mr-4">
          <NavLink
            to={"/wishlist"}
            className={
              location.pathname === "/expenses"
                ? "text-yellow-400"
                : "text-black hover:text-gray-300"
            }
          >
            Wishlist
          </NavLink>
        </li>
      </ul>
      <ul className="flex justify-end items-center">
        {isLoggedIn ? (
          <>
            <li className="mr-4 text-black">{user}</li>
            <li>
              <button
                className="text-black hover:text-gray-300 border border-black px-3 py-1 rounded"
                onClick={handleSignOut}>
                  Sign Out
                </button>
            </li>
          </>
        ) : (
          <>
            <li className="mr-4">
              <NavLink
                to={"login"}
                className={
                  location.pathname === "/login"
                    ? "text-yellow-400"
                    : "text-black hover:text-gray-300"
                }
              >
                Login
              </NavLink>
            </li>
            <li>
              <NavLink
                to={"signup"}
                className={
                  location.pathname === "/signup"
                    ? "text-yellow-400"
                    : "text-black hover:text-gray-300"
                }
              >
                Sign up
              </NavLink>
            </li>
          </>
        )}
      </ul>
    </div>
  );
}