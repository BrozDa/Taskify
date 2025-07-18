import React from 'react'
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
function Dashboard() {

  const { setToken } = useAuth();
  const navigate = useNavigate();

  const handleLogOut = () => {
    setToken(null);
    navigate("/"); 
  }

  return (
    <div>
      <h1>Logged In</h1>
      <button onClick={handleLogOut}>Log out</button>
    </div>
    
  )
}

export default Dashboard