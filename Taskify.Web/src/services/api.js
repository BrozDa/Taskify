import axios from "axios";
import { useAuth } from "../context/AuthContext";


const baseUrl = "https://localhost:7024/api";

export const userLogin = async (username, password) => {
  const response = await axios
    .post(`${baseUrl}/Auth/login`, {
      username,
      password,
      }, 
      {
        headers: {
          'Accept': 'text/plain',
          'Content-Type': 'application/json'
        }
      })
  return response.data
};