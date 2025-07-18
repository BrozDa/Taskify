import React, { useEffect, useState } from 'react'
import { useAuth } from "../context/AuthContext";
import {userLogin} from "../services/api"
import { useNavigate } from "react-router-dom";

function Login() {

  const { setToken } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("admin");
  const [password, setPassword] = useState("admin");

  const handleSubmit = async() => {
    const token = await userLogin(username, password);
    setToken(token);
    navigate("/dashboard"); 
  }

  return (
    <div>
      <p>{username}</p>
      <p>{password}</p>
      <button onClick={handleSubmit}>Submit</button>
    </div>
  );
}

export default Login