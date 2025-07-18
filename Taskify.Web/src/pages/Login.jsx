import React, { useEffect, useState } from 'react'
import { useAuth } from "../context/AuthContext";
import {userLogin} from "../services/api"
import { useNavigate, useLocation } from "react-router-dom";
import LoginInput from '../components/LoginInput';
import Button from '../components/Button';
function Login() {

  const { setToken } = useAuth();

  const navigate = useNavigate();
  const location = useLocation();

  const successMessage = location.state?.successMessage;

  const [username, setUsername] = useState("admin");
  const [password, setPassword] = useState("admin");
  const [error, setError] = useState(null);


  const handleSubmit = async(username, password, e) => {
    e.preventDefault();
    try {
      const token = await userLogin(username, password);
      setToken(token);
      navigate('/dashboard');

    } catch (err) {
      setError("Invalid username or password");
    }
  }

  return (
    <div className="flex items-center justify-center rounded-lg bg-gray-600 dark:bg-gray-900">
      <div className="w-full max-w-md p-8 bg-gray-800 rounded-lg shadow-md dark:bg-gray-800">

        {successMessage && (
        <h2 className="mb-6 text-2xl font-semibold text-center text-green-800 dark:text-white">
          {successMessage}
        </h2>
        )}
        <h2 className="mb-6 text-2xl font-semibold text-center text-blue-400 dark:text-white">
          Sign in to your account
        </h2>

        {error && <p className="text-red-500 mb-3">{error}</p>}
        <form className="space-y-4">
          <LoginInput
            label="Username"
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
          <LoginInput
            label="Password"
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <Button 
            type="submit"
            text="Sign In"
            action={(e) => handleSubmit(username, password,e)}/>
          <Button 
            type="submit"
            text="Create new account"
            action={() => navigate('/register')}/>
        </form>
      </div>
    </div>
    
  );
}

export default Login